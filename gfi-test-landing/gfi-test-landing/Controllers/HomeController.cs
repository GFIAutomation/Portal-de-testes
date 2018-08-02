using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace gfi_test_landing.Controllers
{
    public class HomeController : Controller
    {
        private void changeLanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }

        [Authorize]
        public ActionResult Dashboard(string language)
        {
            changeLanguage(language);
            return View();
        }
        [Authorize]
        public ActionResult Index(string language)
        {
            changeLanguage(language);
            return View();
        }
        
        [Authorize]
        public ActionResult Button()
        {
            return View();
        }
        [Authorize]
        public ActionResult TestCreate()
        {
            return View();
        }
        [Authorize]
        public ActionResult TestList()
        {
            return View();
        }
       
    }
}