﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UKSHAApi.Models;
using UKSHAApi.Repository.Unit;

namespace UKSHAApi.Controllers
{
    [RoutePrefix("api/Unit")]
    public class UnitController : ApiController
    {
        private Lab repositoryLab = new Lab();

        [HttpPost]
        [Route("getMemberInfo")]
        public HttpResponseMessage getMemberInfo([FromBody] string pmrssm_id)
        {
            dataSet ds = repositoryLab.getMemberInfo(pmrssm_id);
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
    }
}
