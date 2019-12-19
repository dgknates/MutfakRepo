using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class AdminMutfakYorum
    {
    }

    public partial class db_StokKontrolEntities
    {
        public List<AdminMutfakYorum> GetAllAdminMutfakYorum()
        {
            return AdminMutfakYorum.Where(x=> x.Deleted ==false).OrderByDescending(x=> x.Tarih).ToList();
        }

        public AdminMutfakYorum AddAdminMutfakYorum(string _adminMutfakYorum, int kisiId)
        {
            AdminMutfakYorum adminMutfakYorum = new AdminMutfakYorum
            {
                AdminYorumu = _adminMutfakYorum,
                KisiId = kisiId,
                Tarih = DateTime.Now,
                Deleted = false,

            };
            AdminMutfakYorum.Add(adminMutfakYorum);
            SaveChanges();

            return adminMutfakYorum;
        }

        public List<AdminMutfakYorum> GetAdminMutfakYorumWithPageNumber(int pageNo)
        {
            return AdminMutfakYorum.Where(x => x.Deleted == false).OrderByDescending(x => x.Tarih).Skip((pageNo - 1) * 5).Take(5).ToList();
        }
    }
}