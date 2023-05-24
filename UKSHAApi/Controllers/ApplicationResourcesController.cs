using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UKSHAApi.Models;
using UKSHAApi.Repository;
using UKSHAApi.Repository.ApplicationResources;

namespace UKSHAApi.Controllers
{
    [RoutePrefix("api/ApplicationResources")]
    public class ApplicationResourcesController : ApiController
    {
        private SMSDataLayer smsDataLayer = new SMSDataLayer();
        private Admin repositoryAadmin = new Admin();

        [HttpPost]
        [Route("MasterQueries")]
        public HttpResponseMessage MasterQueries([FromBody] ipAdmin objBO)
        {
            dataSet ds = repositoryAadmin.MasterQueries(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("InsertUpdatemaster")]
        public HttpResponseMessage InsertUpdatemaster([FromBody] MasterInfo objBO)
        {
            string Result = repositoryAadmin.InsertUpdatemaster(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
        [Route("Authenticate_LoginInfo")]
        public HttpResponseMessage Authenticate_LoginInfo([FromBody] Authenticate objBO)
        {
            dataSet ds = repositoryAadmin.Authenticate_LoginInfo(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }
        [HttpPost]
        [Route("GetOtp")]
        public HttpResponseMessage GetOtp()
        {
            string otpvalue = OTPGenerator.GenerateRandomOTP(6);
            if (!string.IsNullOrEmpty(otpvalue))
            {
                return Request.CreateResponse(HttpStatusCode.OK, otpvalue);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Otp not found");
            }
        }
        [HttpPost]
        [Route("AuthenticateUserLoginOTP")]
        public HttpResponseMessage AuthenticateUserLoginOTP([FromBody] ipSMS objBO)
        {
            string smsresponse = smsDataLayer.AuthenticateUserLoginOTP(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, smsresponse);
        }
        [HttpPost]
        [Route("PasswordChangeSMS")]
        public HttpResponseMessage PasswordChangeSMS([FromBody] ipSMS objBO)
        {
            string smsresponse = smsDataLayer.PasswordChangeSMS(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, smsresponse);
        }
    }
}
