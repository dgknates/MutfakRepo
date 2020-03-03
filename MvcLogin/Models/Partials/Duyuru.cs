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
        public Duyuru AddAnnouncement(int kisiId, DateTime? startDate, DateTime? endDate)
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
            Duyuru duyuru = Duyuru.OrderByDescending(x => x.ObjectId).FirstOrDefault(x => x.Deleted == false);
            if (duyuru != null)
            {
                return duyuru.ObjectId;
            }
            else
            {
                return 0;
            }
        }

        public Duyuru GetLastAnnouncement()
        {
            Duyuru lastAnnouncement = Duyuru.OrderByDescending(x => x.ObjectId).FirstOrDefault(x => x.Deleted == false);
            if (lastAnnouncement != null && lastAnnouncement != default(Duyuru))
            {
                return lastAnnouncement;
            }
            else
            {
                return default(Duyuru);
            }
        }

        public bool UpdateDuyuruStartDate(int duyuruId, DateTime? startDate)
        {
            List<DuyuruBilgi> duyuruBilgiList = GetLastDuyuruBilgiToList(duyuruId);
            foreach (var item in duyuruBilgiList)
            {
                item.Duyuru.StartDate = startDate;
            }
            SaveChanges();
            return true;
        }

        public bool UpdateDuyuruEndDate(int duyuruId, DateTime? endDate)
        {
            List<DuyuruBilgi> duyuruBilgiList = GetLastDuyuruBilgiToList(duyuruId);
            foreach (var item in duyuruBilgiList)
            {
                item.Duyuru.EndDate = endDate;
            }
            SaveChanges();
            return true;
        }



    }
}