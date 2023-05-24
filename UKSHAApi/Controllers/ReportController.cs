using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UKSHAApi.Models;
using UKSHAApi.Repository.Report;

namespace UKSHAApi.Controllers
{
    [RoutePrefix("api/Report")]
    public class ReportController : ApiController
    {
        private Report repositoryReport = new Report();
        [HttpPost]
        [Route("MIS_ReportQueries")]
        public HttpResponseMessage MIS_ReportQueries([FromBody] ipReport objBO)
        {
            dataSet ds = repositoryReport.MIS_ReportQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("ChandanMIS_ReportQueries")]
        public HttpResponseMessage ChandanMIS_ReportQueries([FromBody] ipReport objBO)
        {
            if (objBO.ReportType == "XL")
            {
                dataSet ds = repositoryReport.ChandanMIS_ReportQueries(objBO);
                UKSHAApi.Repository.ExcelGenerator obj = new Repository.ExcelGenerator();
                return obj.GetExcelFile(ds.ResultSet);
            }
            else
            {
                dataSet ds = repositoryReport.ChandanMIS_ReportQueries(objBO);
                return Request.CreateResponse(HttpStatusCode.OK, ds);
            }
        }
    }
}
