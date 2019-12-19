using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class Duyuru
    {
    }
    public partial class db_StokKontrolEntities
    {
        public Duyuru AddAnnouncement(int kisiId, DateTime startDate, DateTime endDate)
        {
            Duyuru duyuru = new Duyuru
            {
                PersonId = kisiId,
                StartDate = startDate,
                EndDate = endDate,
                Deleted = false
            };
            Duyuru.Add(duyuru);
            SaveChanges();
            return duyuru;
        }

        public int FindLastAnnouncement()
        {
            return Duyuru.OrderByDescending(x => x.ObjectId).FirstOrDefault(x => x.Deleted == false).ObjectId;
        }


       

    }
}