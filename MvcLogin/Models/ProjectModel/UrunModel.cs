using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class UrunModel
    {
        public UrunModel()
        {

        }
        public UrunModel(Urun urun)
        {
            ObjectId = urun.ObjectId;
            UrunAdi = urun.UrunAdi;
            StokMiktari = urun.StokMiktari;
            DolapAdi = urun.Dolap.DolapAdi;
            UretimMiktari = 0;
            Selected = false;
            AllUretim = 0;
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
        }

        public UrunModel(Urun urun,int uretimMiktari)
        {
            ObjectId = urun.ObjectId;
            UrunAdi = urun.UrunAdi;
            StokMiktari = urun.StokMiktari;
            DolapAdi = urun.Dolap.DolapAdi;
            UretimMiktari = uretimMiktari;
            Selected = true;
            AllUretim = 0;
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
        }

        public UrunModel(Urun urun, List<UrunTipi> urunTipleri)
        {
            ObjectId = urun.ObjectId;
            UrunAdi = urun.UrunAdi;
            StokMiktari = urun.StokMiktari;
            DolapAdi = urun.Dolap.DolapAdi;
            UretimMiktari = 0;
            Selected = false;
            AllUretim = 0;
            UrunTipi = urunTipleri;
            UrunTipiString = "Belirtilmedi.";
        }
        public UrunModel(Urun urun, List<UrunTipi> urunTipleri, string urunTipiString)
        {
            ObjectId = urun.ObjectId;
            UrunAdi = urun.UrunAdi;
            StokMiktari = urun.StokMiktari;
            DolapAdi = urun.Dolap.DolapAdi;
            UretimMiktari = 0;
            Selected = false;
            AllUretim = 0;
            UrunTipi = urunTipleri;
            if (urunTipiString!= null)
            {
                UrunTipiString = urunTipiString;
            }
            else
            {
                UrunTipiString = "Belirtilmedi.";
            }
        }





        public int ObjectId { get; set; }
        public string UrunAdi { get; set; }
        public int? StokMiktari { get; set; }
        public string DolapAdi { get; set; }
        public int UretimMiktari { get; set; }
        public bool Selected { get; set; }
        public int AllUretim { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
    ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
    ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public List<UrunTipi> UrunTipi { get; set; }
        public string UrunTipiString { get; set; }
    }
}