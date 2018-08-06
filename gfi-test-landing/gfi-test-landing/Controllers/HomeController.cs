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
        private testLandingEntities db = new testLandingEntities();

        private void changeLanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }
        [Authorize]
        public ActionResult Project(string language)
        {
            changeLanguage(language);
            return View();
        }
        [Authorize]
        public ActionResult Dashboard(string language)
        {
            changeLanguage(language);

            List<String> broswer = new List<String>();
            int chrome = 0;
            int firefox = 0;
            foreach (var item in db.Test)
            {
                broswer.Add(item.broswer);
                if (item.broswer == "Chrome")
                {
                    chrome++;
                    ViewBag.brw1 = "Chrome";
                }
                    
                if (item.broswer == "Firefox")
                {
                    firefox++;
                    ViewBag.brw2 = "Firefox";
                }
                    
            }
            ViewBag.brwChrome = chrome;
            ViewBag.Firefox = firefox;
            ViewBag.broswer = broswer;
            return View();

            //https://www.youtube.com/watch?v=20L-h1rKyvM
            //https://www.youtube.com/watch?v=AqayTPADGsg
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