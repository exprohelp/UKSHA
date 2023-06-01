using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UKSHAApi.Models;
using UKSHAApi.Repository.MobileApp;
using UKSHAApi.Repository.Unit;

namespace UKSHAApi.Controllers
{
    [RoutePrefix("api/Unit")]
    public class UnitController : ApiController
    {
        private Lab repositoryLab = new Lab();
        private LISDBLayer repositoryLisDBLayer = new LISDBLayer();

        [HttpPost]
        [Route("getMemberInfo")]
        public HttpResponseMessage getMemberInfo([FromBody] string pmrssm_id)
        {
            dataSet ds = repositoryLab.getMemberInfo(pmrssm_id);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("GetSGHSEmpContributionData")]
        public HttpResponseMessage GetSGHSEmpContributionData([FromBody] string emp_code)
        {
            dataSet2 ds = repositoryLab.GetSGHSEmpContributionData(emp_code);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("LabQueries")]
        public HttpResponseMessage LabQueries([FromBody] ipUnit objBO)
        {
            dataSet ds = repositoryLab.LabQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Unit_VerificationQueries")]
        public HttpResponseMessage Unit_VerificationQueries([FromBody] ipUnit objBO)
        {
            dataSet ds = repositoryLab.Unit_VerificationQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }        
        [HttpPost]
        [Route("Register_Patient")]
        public HttpResponseMessage Register_Patient([FromBody] PatientInfo objBO)
        {
            string ds = repositoryLab.Register_Patient(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("Unit_InsertUpdateVerification")]
        public HttpResponseMessage Unit_InsertUpdateVerification([FromBody] List<PatientDetails> objBO)
        {
            string result = repositoryLab.Unit_InsertUpdateVerification(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        [Route("ITDoseChandanBulkSync")]
        public HttpResponseMessage ITDoseChandanBulkSync([FromBody] ipUnit objBO)
        {
            dataSet ds = repositoryLisDBLayer.ITDoseChandanBulkSync(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("MarkITDoseSynced")]
        public HttpResponseMessage MarkITDoseSynced([FromBody] MasterInfo objBO)
        {
            string result = repositoryLisDBLayer.MarkITDoseSynced(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
