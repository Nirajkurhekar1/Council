using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouncilAccounting.Models
{
    public class LoginSecurity
    {
        public static bool Login(string username, string password)
        {
            //AccountingDBEntities db = new AccountingDBEntities();
             waroraEntities db = new waroraEntities();
            // municipalcouncilEntities db = new municipalcouncilEntities();
            //ParseoniDBEntities db = new ParseoniDBEntities();
            //  wanadongriEntities db = new wanadongriEntities();
            //  parseoniEntities db = new parseoniEntities();
            // parseoniEntities1 db = new parseoniEntities1();
            // parseoniDBEntities db = new parseoniDBEntities();
           // localhostCAEntities db = new localhostCAEntities();
            return (db.USER_MSTR.Any(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && a.Password == password));

        }
    }
}