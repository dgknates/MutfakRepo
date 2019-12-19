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

        public List<Kullanıcılar> getAllUserList()
        {
            return Kullanıcılar.Where(x => x.Deleted == false).ToList();
        }



        public Kullanıcılar getPersonByUserNameAndPasword(string userName, string password)
        {
            return Kullanıcılar.FirstOrDefault(x => x.KullanıcıAdı == userName && x.Parola == password);
        }

        public Kullanıcılar getPersonByUserId(int? userId)
        {
            return Kullanıcılar.FirstOrDefault(x => x.ObjectId == userId);
        }

        public int KullanıcılarSaveChanges(Kullanıcılar kullanıcı)
        {
            int sonuc = SaveChanges();
            return sonuc;
        }

        public Kullanıcılar editUser(Kullanıcılar model, int? kisiid)
        {

            Kullanıcılar kullanıcı = getPersonByUserId(kisiid);
            //kullanıcı.KullanıcıAdı = model.KullanıcıAdı;
            //kullanıcı.Parola = model.Parola;
            kullanıcı.GuncelBorc = model.GuncelBorc;

            return kullanıcı;

        }

        public Kullanıcılar removeUser(int? kisiid)
        {
            Kullanıcılar kullanıcı = getPersonByUserId(kisiid);
            kullanıcı.Deleted = true;
            //Kullanıcılar.Remove(kullanıcı);
            SaveChanges();
            return null;
        }

    }
}