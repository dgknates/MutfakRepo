using MvcLogin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcLogin.Models;
using System.IO;
using System.Web.Http;

namespace MvcLogin.Controllers
{
    [AuthFilter]
    public class HomeController : BaseController
    {
        public ActionResult Homepage()
        {
            List<AdminMutfakYorum> adminMutfakYorum = StokKontrolEntitiesProvider.GetAllAdminMutfakYorum();
            List<KullaniciYorum> kullaniciYorum = StokKontrolEntitiesProvider.GetAllKullaniciYorum();
            List<DuyuruBilgi> duyuruBilgi = null;
            List<string> UrunTipiString = null;
            int lastAnnouncement = StokKontrolEntitiesProvider.FindLastAnnouncement();
            if (lastAnnouncement != 0)
            {
                duyuruBilgi = StokKontrolEntitiesProvider.GetLastDuyuruBilgiToList(lastAnnouncement);
                UrunTipiString = new List<string>();

                foreach (var item in duyuruBilgi)
                {
                    Urun urun = StokKontrolEntitiesProvider.GetProductNameByObjectId(item.Urun.ObjectId);
                    if (urun.UrunTipi != null)
                    {
                        UrunTipiString.Add(urun.UrunTipi.UrunTipi1);
                    }
                    else
                    {
                        UrunTipiString.Add("Birim");
                    }

                }
            }
            HomepageModel model = new HomepageModel(kullaniciYorum, duyuruBilgi, adminMutfakYorum, UrunTipiString);

            return View(model);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult AddKullaniciYorum(HomepageModel gelenler)
        {
            KullaniciYorum sonuc = new KullaniciYorum();
            if (gelenler.KullaniciYorumEkle != null)
            {
                sonuc = StokKontrolEntitiesProvider.AddKullaniciYorum(gelenler.KullaniciYorumEkle, Convert.ToInt32(Session["UserObjectId"]));

            }
            if (sonuc.KullaniciYorumu != null)
            {
                return RedirectToAction("Homepage", "Home");
            }
            else
            {
                return RedirectToAction("Homepage", "Home");
            }
        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult AddAdminMutfakYorum(HomepageModel gelenler)
        {
            if (Session["grup"].Equals(3))
            {
                AdminMutfakYorum adminMutfakYorum = new AdminMutfakYorum();
                if (gelenler.AdminYorumEkle != null)
                {
                    adminMutfakYorum = StokKontrolEntitiesProvider.AddAdminMutfakYorum(gelenler.AdminYorumEkle, Convert.ToInt32(Session["UserObjectId"]));
                }
                if (adminMutfakYorum.AdminYorumu != null)
                {
                    return RedirectToAction("Homepage", "Home");
                }
                else
                {
                    return RedirectToAction("Homepage", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult EditMutfakDuyuru(int? duyuruId)
        {
            if (duyuruId != null)
            {
                List<DuyuruBilgi> duyuruBilgi = StokKontrolEntitiesProvider.GetLastDuyuruBilgiToList(Convert.ToInt32(duyuruId));
                List<UrunModel> urunList = new List<UrunModel>();
                AnnouncementModel model = new AnnouncementModel();

                foreach (var item in duyuruBilgi)
                {
                    urunList.Add(new UrunModel(item.Urun, Convert.ToInt32(item.Adet)));
                }
                model.urunList = urunList;
                return Announcement(model);
            }
            return View();
        }

        public ActionResult GetKullaniciYorum(int sayfaNo)
        {
            HomepageModel model = new HomepageModel();
            model.kullaniciYorum = StokKontrolEntitiesProvider.GetKullaniciYorumWithPageNumber(sayfaNo);
            return PartialView("_PartialHomepageKullaniciYorum", model);
        }

        public ActionResult GetAdminYorum(int sayfaNo)
        {
            HomepageModel model = new HomepageModel();
            model.adminMutfakYorum = StokKontrolEntitiesProvider.GetAdminMutfakYorumWithPageNumber(sayfaNo);
            return PartialView("_PartialHomepageAdminMutfakYorum", model);
        }
        public ActionResult Products()
        {
            if (Session["grup"].Equals(3))
            {
                List<Urun> urunler = StokKontrolEntitiesProvider.GetAllProducts();
                List<UrunTipi> urunTipleri = StokKontrolEntitiesProvider.GetAllUrunTipi();
                List<UrunModel> model = new List<UrunModel>();
                
                foreach (Urun urun in urunler)
                {
                    if (urun.UrunTipi != null)
                    {
                        model.Add(new UrunModel(urun, urunTipleri, urun.UrunTipi.UrunTipi1));
                    }
                    else
                    {
                        model.Add(new UrunModel(urun, urunTipleri));
                    }
                }
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult Products(List<UrunModel> gelenler)
        {
            if (Session["grup"].Equals(3))
            {
                List<Urun> urunler = StokKontrolEntitiesProvider.GetAllProducts();
                List<UrunTipi> urunTipleri = StokKontrolEntitiesProvider.GetAllUrunTipi();
                List<UrunModel> model = new List<UrunModel>();

                for (int i = 0; i < urunler.Count(); i++)
                {
                    if (urunler[i].StokMiktari == null)
                    {
                        urunler[i].StokMiktari = 0;
                    }
                    for (int j = 0; j < gelenler.Count(); j++)
                    {
                        if (urunler[i].ObjectId == gelenler[j].ObjectId && gelenler[j].Selected == true)
                        {
                            urunler[i].StokMiktari += gelenler[j].UretimMiktari;
                        }
                    }
                }
                StokKontrolEntitiesProvider.SaveChanges();

                foreach (Urun urun in urunler)
                {
                    if (urun.UrunTipi != null)
                    {
                        model.Add(new UrunModel(urun, urunTipleri, urun.UrunTipi.UrunTipi1));
                    }
                    else
                    {
                        model.Add(new UrunModel(urun, urunTipleri));
                    }
                }
                ModelState.Clear();
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Announcement(int? duyuruId)
        {
            if (duyuruId == null)
            {
                if (Session["grup"].Equals(3))
                {
                    List<Urun> urunler = StokKontrolEntitiesProvider.GetAllProducts();
                    List<UrunTipi> urunTipleri = StokKontrolEntitiesProvider.GetAllUrunTipi();
                    AnnouncementModel model = new AnnouncementModel(1);
                    List<UrunModel> urunList = new List<UrunModel>();

                    foreach (Urun urun in urunler)
                    {
                        if (urun.UrunTipi != null)
                        {
                            urunList.Add(new UrunModel(urun, urunTipleri, urun.UrunTipi.UrunTipi1));
                        }
                        else
                        {
                            urunList.Add(new UrunModel(urun, urunTipleri));
                        }
                    }
                    model.urunList = urunList;
                    return View(model);
                }
            }
            else
            {
                List<DuyuruBilgi> duyuruBilgi = StokKontrolEntitiesProvider.GetLastDuyuruBilgiToList(Convert.ToInt32(duyuruId));
                List<UrunModel> urunList = new List<UrunModel>();
                AnnouncementModel model = new AnnouncementModel(1);

                List<Urun> urunler = StokKontrolEntitiesProvider.GetAllProducts();

                foreach (var item in duyuruBilgi)
                {
                    urunList.Add(new UrunModel(item.Urun, Convert.ToInt32(item.Adet)));
                }
                foreach (Urun urun in urunler)
                {
                    if (urunList.Where(x => x.UrunAdi == urun.UrunAdi).FirstOrDefault() == null)
                    {
                        urunList.Add(new UrunModel(urun));
                    }
                }
                model.urunList = urunList;
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Announcement(AnnouncementModel gelenler)
        {
            if (Session["grup"].Equals(3))
            {
                int lastAnnouncement = StokKontrolEntitiesProvider.FindLastAnnouncement();
                foreach (var item in gelenler.urunList)
                {
                    if (item.UretimMiktari != 0 && item.Selected == true)
                    {
                        StokKontrolEntitiesProvider.AddDuyuruBilgi(item.ObjectId, item.UretimMiktari, lastAnnouncement);
                        StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, item.UretimMiktari);
                    }
                }
                ModelState.Clear();
                return RedirectToAction("Announcement", "Home");
            }
            return RedirectToAction("Index", "Login");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult PostAddAnnouncement(AnnouncementModel gelenler)
        {
            if (Session["grup"].Equals(3))
            {
                foreach (var item in gelenler.urunList)
                {
                    StokKontrolEntitiesProvider.AddDuyuruBilgi(item.ObjectId, item.AllUretim, StokKontrolEntitiesProvider.FindLastAnnouncement());
                }
                ModelState.Clear();
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddAnnouncement(AnnouncementModel gelenler)
        {
            StokKontrolEntitiesProvider.AddAnnouncement(Convert.ToInt32(Session["UserObjectId"]), gelenler.StartDate, gelenler.EndDate);
            foreach (var item in gelenler.urunList)
            {
                if (item.Selected == true)
                {
                    Urun urun = StokKontrolEntitiesProvider.GetProductNameByObjectId(item.ObjectId);
                    if (urun.UrunTipi != null)
                    {
                        item.UrunTipiString = urun.UrunTipi.UrunTipi1;
                    }
                    else
                    {
                        item.UrunTipiString = "Birim";
                    }
                    item.UrunAdi = urun.UrunAdi;
                }
            }
            return PartialView(gelenler);
        }

        public ActionResult EditAnnouncement(int? duyuruId)
        {
            if (duyuruId == null)
            {
                if (Session["grup"].Equals(3))
                {
                    AnnouncementModel model = new AnnouncementModel(1);
                    List<Urun> urunler = StokKontrolEntitiesProvider.GetAllProducts();
                    List<UrunModel> urunList = new List<UrunModel>();

                    foreach (Urun urun in urunler)
                    {
                        urunList.Add(new UrunModel(urun));
                    }
                    model.urunList = urunList;
                    return View(model);
                }
            }
            else
            {
                AnnouncementModel model = new AnnouncementModel(1);
                List<DuyuruBilgi> duyuruBilgi = StokKontrolEntitiesProvider.GetLastDuyuruBilgiToList(Convert.ToInt32(duyuruId));
                List<UrunModel> urunList = new List<UrunModel>();
                List<Urun> urunler = StokKontrolEntitiesProvider.GetAllProducts();

                model.StartDate = duyuruBilgi[0].Duyuru.StartDate;
                model.EndDate = duyuruBilgi[0].Duyuru.EndDate;

                foreach (var item in duyuruBilgi)
                {
                    urunList.Add(new UrunModel(item.Urun, Convert.ToInt32(item.Adet)));
                }
                foreach (Urun urun in urunler)
                {
                    if (urunList.Where(x => x.UrunAdi == urun.UrunAdi).FirstOrDefault() == null)
                    {
                        urunList.Add(new UrunModel(urun));
                    }
                }
                model.urunList = urunList;
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditAnnouncement(AnnouncementModel gelenler)
        {
            if (Session["grup"].Equals(3))
            {
                int lastAnnouncement = StokKontrolEntitiesProvider.FindLastAnnouncement();
                List<DuyuruBilgi> duyuruBilgiList = StokKontrolEntitiesProvider.GetLastDuyuruBilgiToList(lastAnnouncement);
                foreach (var item in gelenler.urunList)
                {
                    DuyuruBilgi duyuruBilgiItem = duyuruBilgiList.Where(x => x.UrunId == item.ObjectId).FirstOrDefault();
                    if (item.UretimMiktari != 0 && item.Selected == true)
                    {
                        if (duyuruBilgiItem != null)
                        {
                            if (item.UretimMiktari != duyuruBilgiItem.Adet)
                            {
                                if (duyuruBilgiItem.Adet == null)
                                {
                                    StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, item.UretimMiktari);
                                }
                                else if (item.UretimMiktari < duyuruBilgiItem.Adet)
                                {
                                    int? guncellenecekMiktar = item.UretimMiktari - duyuruBilgiItem.Adet;
                                    StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, guncellenecekMiktar);
                                    StokKontrolEntitiesProvider.UpdateDuyuruBilgi(item.ObjectId, lastAnnouncement, item.UretimMiktari);
                                }
                                else if (item.UretimMiktari > duyuruBilgiItem.Adet)
                                {
                                    int? guncellenecekMiktar = item.UretimMiktari - duyuruBilgiItem.Adet;
                                    StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, guncellenecekMiktar);
                                    StokKontrolEntitiesProvider.UpdateDuyuruBilgi(item.ObjectId, lastAnnouncement, item.UretimMiktari);
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            StokKontrolEntitiesProvider.AddDuyuruBilgi(item.ObjectId, item.UretimMiktari, lastAnnouncement);
                            StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, item.UretimMiktari);
                        }
                    }
                    else if (item.Selected == false && item.UretimMiktari == 0)
                    {

                    }
                    else if (item.UretimMiktari == 0 && item.Selected == true)
                    {
                        if (duyuruBilgiItem.Adet != null)
                        {
                            int? guncellenecekMiktar = item.UretimMiktari - duyuruBilgiItem.Adet;
                            StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, guncellenecekMiktar);
                            StokKontrolEntitiesProvider.UpdateDuyuruBilgi(item.ObjectId, lastAnnouncement, item.UretimMiktari);
                            StokKontrolEntitiesProvider.RemoveDuyuruBilgi(item.ObjectId, lastAnnouncement);
                        }
                    }
                    else if (duyuruBilgiItem.Adet != null)
                    {
                        int? guncellenecekMiktar = -(duyuruBilgiItem.Adet);
                        StokKontrolEntitiesProvider.UpdateStokMiktari(item.ObjectId, guncellenecekMiktar);
                        StokKontrolEntitiesProvider.RemoveDuyuruBilgi(item.ObjectId, lastAnnouncement);
                    }
                }
                if (gelenler.StartDate != duyuruBilgiList[0].Duyuru.StartDate)
                {
                    StokKontrolEntitiesProvider.UpdateDuyuruStartDate(lastAnnouncement,gelenler.StartDate);
                }
                if (gelenler.EndDate != duyuruBilgiList[0].Duyuru.EndDate)
                {
                    StokKontrolEntitiesProvider.UpdateDuyuruEndDate(lastAnnouncement, gelenler.EndDate);
                }

                ModelState.Clear();
                return RedirectToAction("Announcement", "Home");
            }
            return RedirectToAction("Index", "Login");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditAddAnnouncement(AnnouncementModel gelenler)
        {
            foreach (var item in gelenler.urunList)
            {
                if (item.Selected == true)
                {
                    Urun urun = StokKontrolEntitiesProvider.GetProductNameByObjectId(item.ObjectId);

                    if (urun != null && urun != default(Urun))
                    {
                        if (urun.UrunTipi != null)
                        {
                            item.UrunTipiString = urun.UrunTipi.UrunTipi1;
                        }
                        else
                        {
                            item.UrunTipiString = "Birim";
                        }
                        item.UrunAdi = urun.UrunAdi;
                    }
                }
            }
            return PartialView(gelenler);
        }


        public ActionResult Shopping()
        {
            System.DateTime _alisverisTarihi = DateTime.Now.Date;//set Default Value here



            return View();
        }
        [System.Web.Mvc.HttpPost, ValidateInput(false)]
        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Shopping(AlisverisModel alisverisModel)
        {
            if (ModelState.IsValid)
            {
                Alisveris alisveris = StokKontrolEntitiesProvider.AddShopping(alisverisModel);

                string fileName3 = StokKontrolEntitiesProvider.GetObjectIdByAlisverisTarihi(alisverisModel).ToString();

                string fileName = alisveris.ObjectId.ToString();
                string fileName2 = System.IO.Path.GetFileName(fileName);
                string filePath = "~/Uploads/" + fileName2 + ".jpg";
                alisverisModel.Files.SaveAs(Server.MapPath(filePath));
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult CurrentDebts()
        {
            if (Session["login"] != null)
            {
                List<Kullanıcılar> kullanıcılar = StokKontrolEntitiesProvider.GetAllUserList();
                PersonalDebtsModel model2 = new PersonalDebtsModel();
                List<Aylar> Aylar2 = StokKontrolEntitiesProvider.GetAllAylarList();
                foreach (var ay in Aylar2)
                {
                    model2.Ay.Add(ay);
                }

                foreach (Kullanıcılar kullanıcı in kullanıcılar)
                {
                    model2.Kullanici.Add(kullanıcı);
                }
                for (int index = 0; index < model2.Kullanici.Count(); index++)
                {
                    model2.GuncelBorc.Add(StokKontrolEntitiesProvider.GetToplamBorcByObjectId(model2.Kullanici[index].ObjectId));
                }

                return View(model2);
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult GetCurrentDebts(int SecilenAy)
        {
            if (SecilenAy != 0)
            {
                List<Kullanıcılar> kullanıcılar = StokKontrolEntitiesProvider.GetAllUserList();
                PersonalDebtsModel model = new PersonalDebtsModel();
                List<Aylar> Aylar2 = StokKontrolEntitiesProvider.GetAllAylarList();
                foreach (var ay in Aylar2)
                {
                    model.Ay.Add(ay);
                }

                int index = 0;
                foreach (Kullanıcılar kullanıcı in kullanıcılar)
                {
                    if (StokKontrolEntitiesProvider.GetToplamBorcByDateAndObjectId2(SecilenAy, kullanıcı.ObjectId) != 0)
                    {
                        model.GuncelBorc.Add(StokKontrolEntitiesProvider.GetToplamBorcByDateAndObjectId2(SecilenAy, kullanıcı.ObjectId));
                        model.Kullanici.Add(kullanıcı);
                        model.SecilenAy = SecilenAy;
                    }
                    else
                    {

                    }
                    index++;
                }
                // model nesnesi oluştur sonra içini secilen aya göre doldur sonra modeli yolla
                // var p = Parti....
                return PartialView("_PartialCurrentDebts", model);
            }
            else
            {
                List<Kullanıcılar> kullanıcılar = StokKontrolEntitiesProvider.GetAllUserList();
                PersonalDebtsModel model = new PersonalDebtsModel();
                int index = 0;
                foreach (Kullanıcılar kullanıcı in kullanıcılar)
                {
                    if (StokKontrolEntitiesProvider.GetToplamBorcByDateAndObjectId2(SecilenAy, kullanıcı.ObjectId) != 0)
                    {
                        model.GuncelBorc.Add(StokKontrolEntitiesProvider.GetToplamBorcByDateAndObjectId2(SecilenAy, kullanıcı.ObjectId));
                        model.Kullanici.Add(kullanıcı);
                    }
                    index++;
                }
                return PartialView("_PartialCurrentDebts", model);

            }
        }

        public ActionResult PersonalDebst()
        {
            if (Session["grup"].Equals(3))
            {
                List<Kullanıcılar> kullanıcılar = StokKontrolEntitiesProvider.GetAllUserList();
                PersonalDebtsModel model2 = new PersonalDebtsModel();
                List<Aylar> Aylar2 = StokKontrolEntitiesProvider.GetAllAylarList();
                foreach (var ay in Aylar2)
                {
                    model2.Ay.Add(ay);
                }
                foreach (Kullanıcılar kullanıcı in kullanıcılar)
                {
                    if (kullanıcı.GrupId != 3)
                    {
                        model2.Kullanici.Add(kullanıcı);
                    }
                }
                for (int index = 0; index < model2.Kullanici.Count(); index++)
                {
                    model2.GuncelBorc.Add(StokKontrolEntitiesProvider.GetToplamBorcByObjectId(model2.Kullanici[index].ObjectId));
                }
                return View(model2);
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult GetPersonalDebst(int SecilenAy)
        {
            if (SecilenAy != 0)
            {
                List<Kullanıcılar> kullanıcılar = StokKontrolEntitiesProvider.GetAllUserList();
                PersonalDebtsModel model = new PersonalDebtsModel();
                int index = 0;
                foreach (Kullanıcılar kullanıcı in kullanıcılar)
                {
                    if (StokKontrolEntitiesProvider.GetToplamBorcByDateAndObjectId2(SecilenAy, kullanıcı.ObjectId) == 0)
                    {

                    }
                    else
                    {
                        model.GuncelBorc.Add(StokKontrolEntitiesProvider.GetToplamBorcByDateAndObjectId2(SecilenAy, kullanıcı.ObjectId));
                        model.Kullanici.Add(kullanıcı);

                    }

                    index++;
                }
                // model nesnesi oluştur sonra içini secilen aya göre doldur sonra modeli yolla
                // var p = Parti....
                return PartialView("_PartialBorclandirmaList", model);
            }
            else
            {
                return RedirectToAction("PersonalDebst", "Home");

            }
        }





        [System.Web.Mvc.HttpPost]
        public ActionResult PersonalDebst(PersonalDebtsModel gelenler)
        {
            if (Session["grup"].Equals(3) && gelenler.SecilenAy != 0)
            {
                List<Kullanıcılar> kullanıcılar = StokKontrolEntitiesProvider.GetAllUserList();
                List<KullanıcılarModel> model = new List<KullanıcılarModel>();
                Borclandirma borclandirma = new Borclandirma();
                PersonalDebtsModel model2 = new PersonalDebtsModel();

                List<Aylar> Aylar2 = StokKontrolEntitiesProvider.GetAllAylarList();
                foreach (var ay in Aylar2)
                {
                    model2.Ay.Add(ay);
                }

                foreach (Kullanıcılar kullanıcı in kullanıcılar)
                {
                    if (kullanıcı.GrupId != 3)
                    {
                        model2.Kullanici.Add(kullanıcı);

                    }

                }
                ModelState.Clear();
                for (int i = 0; i < model2.Kullanici.Count(); i++)
                {
                    if (model2.Kullanici[i].GuncelBorc == null)
                    {
                        model2.Kullanici[i].GuncelBorc = 0;
                    }
                    for (int j = 0; j < gelenler.Kullanici.Count(); j++)
                    {
                        if (model2.Kullanici[i].ObjectId == gelenler.Kullanici[j].ObjectId && gelenler.Selected[j] == true)
                        {
                            if (gelenler.SecilenAy == 0)
                            {
                                ModelState.Clear();
                            }
                            else
                            {

                                StokKontrolEntitiesProvider.Borclandirma.Add(new Borclandirma
                                {
                                    KisiId = model2.Kullanici[i].ObjectId,
                                    BorcMiktari = gelenler.Borclandirma[j],
                                    BorlandirmaTarihiId = gelenler.SecilenAy
                                });

                                StokKontrolEntitiesProvider.SaveChanges();



                            }
                        }
                    }
                }
                for (int index = 0; index < model2.Kullanici.Count(); index++)
                {
                    model2.GuncelBorc.Add(StokKontrolEntitiesProvider.GetToplamBorcByObjectId(model2.Kullanici[index].ObjectId));
                }
                ViewBag.Aylar2 = StokKontrolEntitiesProvider.GetAllAylarList();
                StokKontrolEntitiesProvider.SaveChanges();
                return View(model2);
            }
            return RedirectToAction("Index", "Login");

        }

        public ActionResult ShoppingList()
        {
            if (Session["grup"].Equals(3))
            {

                List<Alisveris> alisveris = StokKontrolEntitiesProvider.GetAllAlisverisList();





                return View(alisveris);
            }

            return View();
        }


        public ActionResult AddProduct()
        {
            return View();
        }

        //string kullaniciad ve sifre bizim formumuzda bulunan inputların namedir.
        [System.Web.Mvc.HttpPost]
        public ActionResult AddProduct(string urunAdi, int? stokMiktari, FormCollection urunTipi, FormCollection formCollection)
        {
            string a = formCollection["Dolaplar"];
            string b = urunTipi["[0].UrunTipi"];
            StokKontrolEntitiesProvider.AddProduct(urunAdi, stokMiktari, Convert.ToInt32(a), Convert.ToInt32(b));
            return RedirectToAction("Products", "Home");
        }


        public ActionResult Logout()
        {
            Session["login"] = null;
            return RedirectToAction("Index", "Login");
        }



    }
}

