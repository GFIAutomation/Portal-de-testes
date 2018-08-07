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

            //var chrome = db.Test.Where(x=> x.broswer=="Chrome").Count();
            //var firefox = db.Test.Where(x => x.broswer == "Firefox").Count();
            //var ie = db.Test.Where(x => x.broswer == "IE").Count();
            //var edge = db.Test.Where(x => x.broswer == "Edge").Count();
            //var opera = db.Test.Where(x => x.broswer == "Opera").Count();

            //Brand obj = new Brand();
            //obj.Chrome = chrome.ToString();
            //obj.Firefox = firefox.ToString();
            //obj.IE = ie.ToString();
            //obj.Edge = edge.ToString();
            //obj.Opera = opera.ToString();



            //return Json(obj,JsonRequestBehavior.AllowGet);
            return View();
            //https://www.youtube.com/watch?v=20L-h1rKyvM
            //https://www.youtube.com/watch?v=AqayTPADGsg
        }

        public class Brand
        {
            public string Chrome { get; set; }
            public string Firefox { get; set; }
            public string IE { get; set; }
            public string Edge { get; set; }
            public string Opera { get; set; }
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