using System;
using System.Data;
using System.Text;
using System.Web.Mvc;
using UKSHA.App_Start;
using UKSHA.Repository;
using UKSHAApi.Models;
using UKSHAApi.Repository.Utility;

namespace UKSHA.Areas.MIS.Controllers
{
    public class PrintController : Controller
    {
        public FileResult PrintBillByKey(string visitNo)
        {
            string decriptedVisitNo = EncryptionHelper.Decrypt(visitNo);
            return PrintBill(decriptedVisitNo);
        }
        public FileResult PrintBill(string visitNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipReport obj = new ipReport();
            obj.VisitNo = visitNo;
            obj.Logic = "PrintBill";
            dataSet dsResult = APIProxy.CallWebApiMethod("Report/ChandanMIS_ReportQueries", obj);

            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            decimal discount = 0;
            decimal NetAmount = 0;
            decimal Total = 0;
            string loginName = "";
            string CenterName = "";
            string Address = "";
            string Phone = "";
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Total = Convert.ToDecimal(dr["Total"]);
                    discount = Convert.ToDecimal(dr["discount"]);
                    NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                    CenterName = dr["CenterName"].ToString();
                    Address = dr["Address"].ToString();
                    Phone = dr["Phone"].ToString();

                    b.Append("<div style='width:100%;float:left;margin-top:-12px;padding:8px'>");
                    string headerImageFile = HttpContext.Server.MapPath(@"~/Content/img/logo_nhm_old.png");
                    b.Append("<div style='text-align:left;width:30%;float:left'>");
                    b.Append("<img src=" + headerImageFile + " style='width:70px;margin-top:18px;' />");
                    b.Append("</div>");
                    b.Append("<div style='text-align:left;width:auto;float:left;'>");
                    b.Append("<h3 style='font-weight:bold;margin:0'>" + CenterName + "</h3>");
                    b.Append("<span style='text-align:left;'>Add:" + Address + "</span><br/>");
                    b.Append("<span style='text-align:left;'>Ph. : " + Phone + "</span><br/>");
                    b.Append("</div>");
                    b.Append("</div>");
                    b.Append("<hr/>");

                    b.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
                    b.Append("<tr>");
                    b.Append("<td>Name</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["PatientName"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Bill No.</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["billNo"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Age/Gender</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["Age"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Visit/Reg Date</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["visitDate"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Contact No.</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["MobileNo"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Refered By</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["ref_name"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<td>SHA ID</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["SHAId"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>-</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>-</td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                    loginName = dr["loginName"].ToString();
                }
            }
            b.Append("<table  style='width:100%;font-size:18px;text-align:left;border:0px solid #dcdcdc;margin-top:20px;' border='0' cellspacing='0'>");
            b.Append("<tr>");
            b.Append("<th colspan='9'><hr style='margin-bottom:-2px;border:1px solid #000'></th>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<th style='width:1%;padding:3px;'>S.N.</th>");
            b.Append("<th style='width:58%;padding:3px;'>Test Name</th>");
            b.Append("<th style='width:13%;padding:3px;;text-align:right'>Rate(₹)</th>");
            b.Append("<th style='width:13%;padding:3px;;text-align:right'>Discount(₹)</th>");
            b.Append("<th style='width:15%;padding:3px;;text-align:right'>Net Amt(₹)</th>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<th colspan='9'><hr style='margin-bottom:-2px;border:1px solid #000'></th>");
            b.Append("</tr>");
           
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                var count = 0;
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    count++;
                    b.Append("<tr>");
                    b.Append("<td style='width:1%;font-size:15px !important;padding:0;margin:2px'>" + count + "</td>");
                    b.Append("<td style='width:58%;font-size:15px !important;padding:0;margin:2px'>" + dr["test_name"].ToString() + "</td>");
                    b.Append("<td style='width:13%;text-align:right;padding:0;margin:2px'>" + dr["rate"].ToString() + "</td>");
                    b.Append("<td style='width:13%;text-align:right;padding:0;margin:2px'>" + dr["discount"].ToString() + "</td>");
                    b.Append("<td style='width:15%;text-align:right;padding:0;margin:2px'>" + dr["netAmount"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td colspan='9'><hr style='margin:-1px 0;'></td>");
                    b.Append("</tr>");
                }
            } 
            b.Append("</table>");

            b.Append("<div style='width:100%;float:left;margin-top:5px'>");
            b.Append("<div style='width:60%;float:left'>");

            b.Append("</div>");
            b.Append("<div style='width:40%;float:right'>");
            b.Append("<table style='font-size:14px;float:right' border='0' cellspacing='0'>");
            b.Append("<tr style='font-size:16px'>");
            b.Append("<td colspan='3' style='width:80%;text-align:right'><b>Total Amount : </b></td>");
            b.Append("<td style='width:15%;text-align:right;white-space: nowrap;'><b>" + Total + "</b></td>");
            b.Append("</tr>");
            if (discount > 0)
            {
                b.Append("<tr style='font-size:16px'>");
                b.Append("<td colspan='3' style='width:80%;text-align:right'><b>Discount : </b></td>");
                b.Append("<td style='width:15%;text-align:right;white-space: nowrap;'><b>" + discount + "</b></td>");
                b.Append("</tr>");
            }
            b.Append("<tr style='font-size:16px'>");
            b.Append("<td colspan='3' style='width:80%;text-align:right'><b>Net Amount : </b></td>");
            b.Append("<td style='width:15%;text-align:right;white-space: nowrap;'><b>" + NetAmount + "</b></td>");
            b.Append("</tr>");
            b.Append("<tr style='font-size:16px'>");
            b.Append("<td colspan='3' style='width:80%;text-align:right'><b>Payable Amount : </b></td>");
            b.Append("<td style='width:15%;text-align:right;white-space: nowrap;'><b>" + NetAmount + "</b></td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");
            b.Append("</div>");        
            b.Append("<div style='text-align:left;width:100%;float:left;border-top:1px solid #000;padding:10px 0;margin:10px 0'>");
            b.Append("<span style='text-align:right;width:30%;float:right;'><b>Entry By : " + loginName + "</b></span>");
            b.Append("</div>");
   				
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A5";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "ServiceReceipt.pdf");
        }
    }
}