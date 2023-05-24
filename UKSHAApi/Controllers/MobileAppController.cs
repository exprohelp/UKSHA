using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UKSHAApi.Models;
using UKSHAApi.Repository.MobileApp;

namespace UKSHAApi.Controllers
{
    [RoutePrefix("api/MobileApp")]
    public class MobileAppController : ApiController
    {
        private App repositoryApp = new App();
        private DownloadLogic repositoryDownload = new DownloadLogic();

        [HttpPost]
        [Route("DiagnosticBookingReportByVisitNo")]
        public HttpResponseMessage DiagnosticBookingReportByVisitNo([FromBody] PatientInfo objBO)
        {
            string result = repositoryApp.DiagnosticBookingReportByVisitNo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("DownloadLISReport")]
        public HttpResponseMessage DownloadLISReport(PatientInfo ipProfile)
        {
            string result = repositoryDownload.DownloadLISReport(ipProfile);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
