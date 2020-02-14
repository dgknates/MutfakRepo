using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class Kullanıcılar
    {
    }

    public partial class db_StokKontrolEntities
    {

        public List<Kullanıcılar> GetAllUserList()
        {
            return Kullanıcılar.Where(x => x.Deleted == false && x.GrupId !=3).ToList();
        }

        public Kullanıcılar GetPersonByUserNameAndPasword(string userName, string password)
        {
            return Kullanıcılar.FirstOrDefault(x => x.KullanıcıAdı == userName && x.Parola == password);
        }

        public Kullanıcılar GetPersonByUserId(int? userId)
        {
            return Kullanıcılar.FirstOrDefault(x => x.ObjectId == userId);
        }

        public int KullanıcılarSaveChanges(Kullanıcılar kullanıcı)
        {
            int sonuc = SaveChanges();
            return sonuc;
        }

        

        public Kullanıcılar RemoveUser(int? kisiid)
        {
            Kullanıcılar kullanıcı = GetPersonByUserId(kisiid);
            kullanıcı.Deleted = true;
            //Kullanıcılar.Remove(kullanıcı);
            SaveChanges();
            return null;
        }

    }
}