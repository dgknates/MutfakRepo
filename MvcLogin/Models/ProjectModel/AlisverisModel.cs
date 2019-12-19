using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class AlisverisModel
    {
        public AlisverisModel()
        {
            
        }
        public AlisverisModel(Alisveris alisveris)
        {
            ObjectId = alisveris.ObjectId;
            MarketAdi = alisveris.MarketAdi;
            AlisverisTarihi = alisveris.AlisverisTarihi;
            FisTutari = alisveris.FisTutari;
            
        }
        
        public int ObjectId { get; set; }
        public string MarketAdi { get; set; }
        public System.DateTime? AlisverisTarihi { get; set; }
        public int? FisTutari { get; set; }

        public HttpPostedFileBase Files { get; set; }


        
    }
}