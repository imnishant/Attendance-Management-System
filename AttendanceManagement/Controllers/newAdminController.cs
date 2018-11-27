using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using AttendanceManagement.Models;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;

namespace AttendanceManagement.Controllers
{
    public class newAdminController : Controller
    {

        //private NewAdminViewModel nvm = new NewAdminViewModel();

        // GET: newAdmin
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NewAdminViewModel nvm)
        {
            AttendanceManagementDBEntities1 db = new AttendanceManagementDBEntities1();
            db.CreateAdmin(nvm.AdminUserName);
            return View();
        }
    }
}