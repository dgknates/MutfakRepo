using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class DuyuruBilgi
    {
    }
    public partial class db_StokKontrolEntities
    {
        public DuyuruBilgi AddDuyuruBilgi(int urunId, int adet, int duyuruId)
        {
            DuyuruBilgi duyuruBilgi = new DuyuruBilgi
            {
                UrunId = urunId,
                Adet = adet,
                DuyuruId = duyuruId
            };
            DuyuruBilgi.Add(duyuruBilgi);
            SaveChanges();
            return duyuruBilgi;
        }

        public List<DuyuruBilgi> GetLastDuyuruBilgiToList(int duyuruId)
        {
            List<DuyuruBilgi> duyuruBilgiList = DuyuruBilgi.Where(x => x.DuyuruId == duyuruId).ToList();
            if (duyuruBilgiList.Count() == 0)
            {
                Duyuru duyuru = Duyuru.OrderByDescending(x => x.ObjectId).FirstOrDefault(x => x.Deleted == false);
                duyuru.Deleted = true;
                SaveChanges();
                return GetLastDuyuruBilgiToList(duyuru.ObjectId);
            }
            return DuyuruBilgi.Where(x => x.DuyuruId == duyuruId && x.Deleted == false).ToList();
        }
        public bool UpdateDuyuruBilgi(int urunId, int duyuruId, int? stokMiktariYeni)
        {
            DuyuruBilgi duyuruBilgi = DuyuruBilgi.Where(x => x.UrunId == urunId && x.DuyuruId == duyuruId && x.Deleted == false).FirstOrDefault();
            duyuruBilgi.Adet = stokMiktariYeni;
            SaveChanges();
            return true;
        }

        public bool RemoveDuyuruBilgi(int urunId, int lastAnnouncement)
        {
            DuyuruBilgi duyuruBilgi = DuyuruBilgi.Where(x => x.UrunId == urunId && x.DuyuruId == lastAnnouncement && x.Deleted == false).FirstOrDefault();
            duyuruBilgi.Deleted = true;
            SaveChanges();
            return true;
        }


    }
}