using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class KullanıcılarModel
    {
        public KullanıcılarModel()
        {

        }

        public KullanıcılarModel(Kullanıcılar kullanıcılar)
        {
            ObjectId = kullanıcılar.ObjectId;
            Isim = kullanıcılar.Isim;
            Soyisim = kullanıcılar.Soyisim;
            GuncelBorc = kullanıcılar.GuncelBorc;
            Borclandirma = 0;
            Selected = false;
            AllBorclandirma = 0;
            GrupId = kullanıcılar.GrupId;
           
        }

      

        public int ObjectId { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public int? GuncelBorc { get; set; }
        public bool Selected { get; set; }
        public int AllBorclandirma { get; set; }
        public int Borclandirma { get; set; }
        public int GrupId { get; set; }
        public int BorclandirilanAy { get; set; }
        public int BorcMiktari { get; set; }
        //public string[] Aylar { get; set; }
        public List<Aylar> Aylar2 { get ; set; }
        public int? GuncelBorc2 { get; set; }


        

    }

    



}