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
           
            string[] buffer = _kullaniciYorum.Split(' ');
            int length;
            string largestword = buffer[0];

            for (int i = 0; i < buffer.Length; i++)
            {
                string temp = buffer[i];
                length = temp.Length;

                if (largestword.Length < buffer[i].Length)
                {
                    largestword = buffer[i];
                }
            }

            var largestwords = from words in buffer
                               let x = largestword.Length
                               where words.Length == x
                               select words;
            
            if (largestword.Length<= 20)
            {
                KullaniciYorum kullaniciYorum = new KullaniciYorum
                {
                    KullaniciYorumu = _kullaniciYorum,
                    KisiId = kisiId,
                    Tarih = DateTime.Now,
                    Deleted = false,

                };
                KullaniciYorum.Add(kullaniciYorum);
                SaveChanges();
                return kullaniciYorum;
            }
            else
            {
                KullaniciYorum kullaniciYorum = new KullaniciYorum();
                
                return kullaniciYorum;
            }
        }

    }
}