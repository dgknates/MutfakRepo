using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class KullaniciYorum
    {
    }

    public partial class db_StokKontrolEntities
    {
        public List<KullaniciYorum> GetAllKullaniciYorum()
        {
            return KullaniciYorum.Where(x => x.Deleted == false).OrderByDescending(x => x.Tarih).ToList();
        }

        public List<KullaniciYorum> GetKullaniciYorumWithPageNumber(int pageNo)
        {
            return KullaniciYorum.Where(x => x.Deleted == false).OrderByDescending(x => x.Tarih).Skip((pageNo - 1) * 5).Take(5).ToList();
        }

        public KullaniciYorum AddKullaniciYorum(string _kullaniciYorum, int kisiId)
        {
            KullaniciYorum kullaniciYorum = new KullaniciYorum {
                KullaniciYorumu = _kullaniciYorum,
                KisiId = kisiId,
                Tarih = DateTime.Now,
                Deleted = false,
                
            };
            KullaniciYorum.Add(kullaniciYorum);
            SaveChanges();

            return kullaniciYorum;
        }

    }
}