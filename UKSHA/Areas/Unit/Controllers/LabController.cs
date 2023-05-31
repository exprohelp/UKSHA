using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UKSHA.Areas.Unit.Controllers
{
    public class LabController : Controller
    {
        // GET: Unit/Lab
        public ActionResult PatientRegistration()
        {
            return View();
        }
        public ActionResult PendingUpload()
        {
            return View();
        }
        public ActionResult CancelTest()
        {
            return View();
        }
        public ActionResult ITDoseBulkSync()
        {
            return View();
        }
    }
}