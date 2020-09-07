
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CouncilAccounting.ViewModel;
using CouncilAccounting.Models;
using System.Text;
using System.Web.Http.Cors;
using System.Web;
using System.Timers;

namespace CouncilAccounting.Controllers
{
    // [EnableCors(origins: "https://councilaccounting.com", headers: "*", methods: "*")]
    //[EnableCors(origins: "https://councilaccounting.in", headers: "*", methods: "*")]
   // [EnableCors(origins: "https://parseoni.councilaccounting.in", headers: "*", methods: "*")]
    //[EnableCors(origins: "https://wanadongri.councilaccounting.in", headers: "*", methods: "*")]
     [EnableCors(origins: "https://warora.councilaccounting.in", headers: "*", methods: "*")]
    //[EnableCors(origins: "https://parseoni1.councilaccounting.in", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
       // localhostCAEntities db = new localhostCAEntities();

        //  AccountingDBEntities db = new AccountingDBEntities();
        // municipalcouncilEntities db = new municipalcouncilEntities();
        // wanadongriEntities db = new wanadongriEntities();
       // parseoniEntities db = new parseoniEntities();
          waroraEntities db = new waroraEntities();
        //ParseoniDBEntities db = new ParseoniDBEntities();
        // parseoniDBEntities db = new parseoniDBEntities();
        public string USERNAME = "";
        [System.Web.Http.HttpPost]
        public object Login(string username,string Password,int Status)
        {
            try
            {
                USERNAME = "";
                USERNAME = username;
                 var usermstr = db.USER_MSTR.SingleOrDefault(a => a.Username == username && a.Password == Password);
                
                if (usermstr == null)
                {
                    return "Invalid Username";
                }
                else
                {
                    if(usermstr.Username != username)
                    {
                        return "Invalid Username";
                    }
                    if (usermstr.Password != Password)
                    {
                        return "Invalid Password";
                    }
                    if (usermstr.Loginstatus == 0 )
                    {
                        usermstr.Loginstatus = Status;
                        db.SaveChanges();
                        var Encode = username + ":" + Password;
                        byte[] bytes = Encoding.UTF8.GetBytes(Encode);
                        var EncodeData = Convert.ToBase64String(bytes);

                        var model = new UserMstr
                        {
                            User_Id = usermstr.User_Id,
                            Address = usermstr.Address,
                            Name = usermstr.Name,
                            Email = usermstr.Email,
                            State = usermstr.State,
                            City = usermstr.City,
                            ContactNo = usermstr.ContactNo,
                            Username = usermstr.Username,
                            Password = usermstr.Password,
                            RoleAssign = usermstr.RoleAssign,
                            Status = usermstr.Status,
                            Image = usermstr.Image,
                            Token = EncodeData,
                        };
                        return model;
                    }
                    else
                    {
                        return "Alredy Login";
                    }                   
                    
                }
                // return "Login Succsesfully";
            }
            catch (Exception ex)
            {
                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        private readonly System.Timers.Timer _checkTimer = new System.Timers.Timer();
        public readonly int CheckTimerInterval = 90 * 100 * 100;

        public LoginController()
        {
            _checkTimer.Elapsed += CheckTimerElapsed;
            _checkTimer.Interval = this.CheckTimerInterval;
            _checkTimer.Enabled = true;
        }

        private void CheckTimerElapsed(object source, ElapsedEventArgs e)
        {
            //Do the processing
            var usermstr = db.USER_MSTR.FirstOrDefault(a => a.Username == USERNAME);
            if (usermstr != null && usermstr.Loginstatus == 1)
            {
                usermstr.Loginstatus = 0;
                db.SaveChanges();

            }
        }


    }
}
