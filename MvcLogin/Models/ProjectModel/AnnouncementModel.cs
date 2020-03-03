using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class AnnouncementModel
    {
        public AnnouncementModel()
        {

        }
        public AnnouncementModel(int a)
        {
            StartDate = System.DateTime.Now;
            EndDate = System.DateTime.Now;
        }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
    ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
    ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public List<UrunModel> urunList { get; set; }
    }
}