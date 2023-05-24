using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UKSHA.Areas.MIS.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Dashboard()
        {
            TempData["IsActive"] = true;
            return View();
        }
        public ActionResult TestAnalysis()
        {           
            return View();
        }
        public ActionResult PatientRegister()
        {
            return View();
        }
        public ActionResult BeneficiaryAudit()
        {
            return View();
        }
        public ActionResult TatReport()
        {
            return View();
        }
        public ActionResult ProcessSummary()
        {
            return View();
        }
        public ActionResult RawData()
        {
            return View();
        }
    }
}