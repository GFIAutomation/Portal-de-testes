using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace gfi_test_landing.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult chartColumn()
        {
            //var db = new testLandingEntities();
            //ArrayList xValue = new ArrayList();
            //ArrayList yValue = new ArrayList();

            //var results = (from p in db.Test select p);
            //results.ToList().ForEach(rs => xValue.Add(rs.broswer));
            //results.ToList().ForEach(rs => yValue.Add(rs.broswer.Count()));

            //new Chart(width: 600, height: 400, theme: ChartTheme.Green)
            //    .AddTitle("Example")
            //    .AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue).Write("bmp");
            //return null;
        }

    }
}