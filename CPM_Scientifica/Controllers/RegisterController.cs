using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App = CPM_Scientifica.Context.AppContext;

namespace CPM_Scientifica.Controllers
{
    public class RegisterController : Controller
    {
        App _db = new App();
        // GET: Register
        public ActionResult Index()
        {
            return View(_db.Registers.ToList());
        }
    }
}
