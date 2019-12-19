using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLogin.Models;

namespace MvcLogin.Controllers
{
    public class BaseController: Controller
    {

        private db_StokKontrolEntities fieldStokKontrolEntities;

        public db_StokKontrolEntities StokKontrolEntitiesProvider
        {
            get {
                if (fieldStokKontrolEntities == default(db_StokKontrolEntities))
                {
                    fieldStokKontrolEntities = new db_StokKontrolEntities();
                }
                return fieldStokKontrolEntities;
            }
        }

        

    }
}