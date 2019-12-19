using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class HomepageModel
    {
        public HomepageModel()
        {

        }
        public HomepageModel(List<KullaniciYorum> _kullaniciYorum, List<DuyuruBilgi> _duyuruBilgi, List<AdminMutfakYorum> _adminMutfakYorum, List<string> _urunTipiString)
        {
            List<KullaniciYorum> kullaniciYorumNew = new List<KullaniciYorum>();
            kullaniciYorum = kullaniciYorumNew;
            List<DuyuruBilgi> duyuruBilgiNew = new List<DuyuruBilgi>();
            duyuruBilgi = duyuruBilgiNew;
            List<AdminMutfakYorum> adminMutfakYorumNew = new List<AdminMutfakYorum>();
            adminMutfakYorum = adminMutfakYorumNew;
            List<string> urunTipiStringNew = new List<string>();
            UrunTipiString = urunTipiStringNew;

            foreach (var item in _kullaniciYorum)
            {
                kullaniciYorum.Add(item);
            }

            foreach (var item in _duyuruBilgi)
            {
                duyuruBilgi.Add(item);

                //if (StokKontrolEntitiesProvider.getProductNameByObjectId(item.ObjectId).UrunTipi != null)
                //{
                //    item.UrunTipiString = StokKontrolEntitiesProvider.getProductNameByObjectId(item.ObjectId).UrunTipi.UrunTipi1;

                //}
                //else
                //{
                //    item.UrunTipiString = "Birim";
                //}
                //item.UrunAdi = StokKontrolEntitiesProvider.getProductNameByObjectId(item.ObjectId).UrunAdi;
            }
            foreach (var item in _adminMutfakYorum)
            {
                adminMutfakYorum.Add(item);
            }

            foreach (var item in _urunTipiString)
            {
                UrunTipiString.Add(item);
            }

            

        }



        public List<KullaniciYorum> kullaniciYorum { get; set; }
        public List<DuyuruBilgi> duyuruBilgi { get; set; }
        public List<AdminMutfakYorum> adminMutfakYorum { get; set; }
        public List<string> UrunTipiString { get; set; }
        public string KullaniciYorumEkle { get; set; }
        public string AdminYorumEkle { get; set; }

    }
}