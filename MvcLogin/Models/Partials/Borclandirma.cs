using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public partial class Borclandirma
    {

    }
    public partial class db_StokKontrolEntities
    {
        public int GetToplamBorcByObjectId( int objectId)
        {
            if (Borclandirma.Where(x => x.KisiId == objectId).Count() ==0)
            {
                return 0;
            }
            return Borclandirma.Where(x => x.KisiId == objectId).Sum(x => x.BorcMiktari);
        }
        public int GetToplamBorcByDateAndObjectId2(int? ay, int objectId)
        {
            if (Borclandirma.Where(x => x.KisiId == objectId && x.BorlandirmaTarihiId==ay).Count() == 0)
            {
                return 0;
            }
            return Borclandirma.Where(x => x.KisiId == objectId && x.BorlandirmaTarihiId == ay).Sum(x=> x.BorcMiktari);
        }
    }
}