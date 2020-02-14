using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class UrunTipi
    {
    }

    public partial class db_StokKontrolEntities
    {
        public List<UrunTipi> GetAllUrunTipi()
        {
            return UrunTipi.Where(x => x.Deleted == false).ToList();
        }
    }
}