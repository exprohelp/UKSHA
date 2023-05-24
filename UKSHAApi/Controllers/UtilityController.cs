using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UKSHAApi.Models;
using UKSHAApi.Repository.Unit;
using UKSHAApi.Repository.Utility;

namespace UKSHAApi.Controllers
{
    [RoutePrefix("api/Utility")]
    public class UtilityController : ApiController
    {
        UploadClass repository = new UploadClass();
        private Lab repositoryLab = new Lab();

        [HttpPost]
        [Route("Insert_ScanedDocument")]
        public HttpResponseMessage Insert_ScanedDocument([FromBody] PatientDetail objBO)
        {
            string ds = repositoryLab.Insert_ScanedDocument(objBO);
            return Request.CreateResponse(HttpStatusCode.OK, ds);
        }

        [HttpPost]
        [Route("UploadPrescription")]
        public async Task<HttpResponseMessage> UploadPurchaseDocument()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            PatientDetail objBO = new PatientDetail();
            SubmitStatus ss = new SubmitStatus();
            if (!Request.Content.IsMimeMultipartContent())
            {
                ss.Status = 0;
                ss.Message = "This is not multipart content";
                response = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "This is not multipart content");
            }
            try
            {
                string outFileName = string.Empty;
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();
                //Json String of object  ipUploadDocument to be send at first or 0 index parameter   
                var json = await filesReadToProvider.Contents[0].ReadAsStringAsync();
                ipDocumentInfo obj = JsonConvert.DeserializeObject<ipDocumentInfo>(json);
                //Image to be send at second or 1 index parameter  
                byte[] fileBytes = await filesReadToProvider.Contents[1].ReadAsByteArrayAsync();
                ss.Message = UploadClass.UploadPrescription(out outFileName, obj.CentreId, obj.VisitNo, fileBytes, obj.ImageName);
                ss.doc_location = outFileName;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
                if (ss.Message.Contains("Success"))
                {
                    objBO.VisitNo = obj.VisitNo;
                    objBO.CentreId = obj.CentreId;
                    objBO.barcodeNo = obj.barcodeNo;
                    objBO.doc_name = obj.doc_name;
                    objBO.doc_location = outFileName;
                    objBO.fSize = fileBytes.Length;
                    objBO.login_id = obj.login_id;
                    objBO.ServerUrl = obj.ServerUrl;
                    objBO.Logic = obj.Logic;
                    ss.Message = repositoryLab.Insert_ScanedDocument(objBO);
                }
            }
            catch (Exception ex)
            {
                ss.Status = 0;
                ss.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.OK, ss);
            }
            return response;
            //string doc_location = string.Empty;
            //string result = UploadClass.UploadPrescription(out doc_location, obj.imageByte, obj.ImageName);
            //return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
