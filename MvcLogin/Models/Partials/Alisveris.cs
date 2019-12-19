using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class Alisveris
    {
        
    }

    public partial class db_StokKontrolEntities
    {
        public Alisveris AddShopping(AlisverisModel alisverisModel)
        {
            Alisveris alisveris = new Alisveris
            {
                AlisverisTarihi = alisverisModel.AlisverisTarihi,
                FisTutari = alisverisModel.FisTutari,
                MarketAdi = alisverisModel.MarketAdi
            };
            Alisveris.Add(alisveris);
            SaveChanges();
            return alisveris;
        }

        public List<Alisveris> getAllAlisverisList()
        {
            return Alisveris.Where(x => x.Deleted == false).OrderByDescending(x=> x.AlisverisTarihi).ToList();
        }

        public int GetObjectIdByAlisverisTarihi(AlisverisModel alisverisModel)
        {
            return Alisveris.FirstOrDefault(x=> x.AlisverisTarihi == alisverisModel.AlisverisTarihi).ObjectId;
        }
    }
}