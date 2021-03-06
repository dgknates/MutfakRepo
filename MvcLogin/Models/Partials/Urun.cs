﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class Urun
    {
    }

    public partial class db_StokKontrolEntities
    {
        public List<Urun> GetAllProducts()
        {
            return Urun.Where(x => x.Deleted == false).ToList();
        }

        public Urun AddProduct(string urunAdi, int? stokMiktari, int dolapId, int urunTipi)
        {
            Urun urun = new Urun
            {
                UrunAdi = urunAdi,
                StokMiktari = stokMiktari,
                DolapId = dolapId,
                UrunTipId = urunTipi
            };
            Urun.Add(urun);
            SaveChanges();
            return urun;
        }

        public Urun GetProductNameByObjectId(int a)
        {
            return Urun.Where(x => x.ObjectId == a && x.Deleted == false).FirstOrDefault();
        }

        public bool UpdateStokMiktari(int urunId, int? guncelleme)
        {
            Urun urun = Urun.Where(x => x.ObjectId == urunId && x.Deleted == false).FirstOrDefault();
            urun.StokMiktari = urun.StokMiktari - guncelleme;
            SaveChanges();
            return urun.StokMiktari == urun.StokMiktari - guncelleme;
        }

       
    }
}