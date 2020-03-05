using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class PersonalDebtsModel
    {
        public PersonalDebtsModel()
        {
            List<Kullanıcılar> Kullanici2 = new List<Kullanıcılar>();
            Kullanici = Kullanici2;
            List<Aylar> Ay2 = new List<Aylar>();
            Ay = Ay2;
            List<int?> GuncelBorc2 = new List<int?>();
            GuncelBorc = GuncelBorc2;
        }
        public PersonalDebtsModel(Kullanıcılar kullanıcılar)
        {
            Kullanici.Add(kullanıcılar);
        }
        public int personalDebts { get; set; }
        public List<Kullanıcılar> Kullanici { get; set; }
        public List<Aylar> Ay { get; set; }
        public List<bool> Selected { get; set; }
        public List<int> Borclandirma { get; set; }
        public int AllBorclandirma { get; set; }
        public int SecilenAy { get; set; }
        public List<int?> GuncelBorc { get; set; }
    }
}