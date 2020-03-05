using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class Aylar
    {
    }
    public partial class db_StokKontrolEntities
    {
        public List<Aylar> GetAllAylarList()
        {
            return Aylar.Where(x => x.Deleted == false).ToList();
        }
    }
}