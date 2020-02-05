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
            return AdminMutfakYorum.Where(x => x.Deleted == false).OrderByDescending(x => x.Tarih).ToList();
        }

        public AdminMutfakYorum AddAdminMutfakYorum(string _adminMutfakYorum, int kisiId)
        {
            string[] buffer = _adminMutfakYorum.Split(' ');
            int length;
            string largestword = buffer[0];

            for (int i = 0; i < buffer.Length; i++)
            {
                string temp = buffer[i];
                length = temp.Length;

                if (largestword.Length < buffer[i].Length)
                {
                    largestword = buffer[i];
                }
            }

            var largestwords = from words in buffer
                               let x = largestword.Length
                               where words.Length == x
                               select words;

            if (largestword.Length <= 20)
            {

                AdminMutfakYorum adminMutfakYorum = new AdminMutfakYorum
                {
                    AdminYorumu = _adminMutfakYorum,
                    KisiId = kisiId,
                    Tarih = DateTime.Now,
                    Deleted = false

                };
                AdminMutfakYorum.Add(adminMutfakYorum);
                SaveChanges();

                return adminMutfakYorum;
            }
            else
            {
                AdminMutfakYorum adminMutfakYorum = new AdminMutfakYorum();
                return adminMutfakYorum;
            }
        }

        public List<AdminMutfakYorum> GetAdminMutfakYorumWithPageNumber(int pageNo)
        {
            return AdminMutfakYorum.Where(x => x.Deleted == false).OrderByDescending(x => x.Tarih).Skip((pageNo - 1) * 5).Take(5).ToList();
        }
    }
}