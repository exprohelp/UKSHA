using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UKSHA.App_Start;
using UKSHA.Repository;
using UKSHAApi.Models;

namespace UKSHA.Areas.MIS.Controllers
{
    public class PrintController : Controller
    {
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
            decimal GrossAmount = 0;
            decimal discount = 0;
            decimal NetAmount = 0;
            decimal Received = 0;
            decimal Balance = 0;
            string CancelAgainstNo = "";
            string loginName = "";
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
                    discount = Convert.ToDecimal(dr["discount"]);
                    NetAmount = Convert.ToDecimal(dr["NetAmount"]);
                    Received = Convert.ToDecimal(dr["Received"]);
                    Balance = Convert.ToDecimal(dr["Balance"]);
                    b.Append("<div style='width:100%;float:left;margin-top:-12px;padding:8px'>");
                    string headerImageFile = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    b.Append("<div style='text-align:left;width:30%;float:left'>");
                    b.Append("<img src=" + headerImageFile + " style='width:170px;margin-top:18px;' />");
                    b.Append("</div>");
                    b.Append("<div style='text-align:left;width:auto;float:left;'>");
                    b.Append("<h4 style='font-weight:bold;margin:0;text-decoration: underline;'>BILL OF SUPPLY(Duplicate)</h4>");
                    b.Append("<h3 style='font-weight:bold;margin:0'>CHANDAN DIAGNOSTIC CENTRE</h3>");
                    b.Append("<span style='text-align:left;'>(Unit Of Chandan Healthcare Ltd.)</span><br/>");
                    b.Append("<span style='text-align:left;'>Add:Biotech Park, Kursi Road, Lucknow, Pin - 226021</span><br/>");
                    b.Append("<span style='text-align:left;'>Ph. : (0522) 2354834, 2351112, 2351151</span><br/>");
                    b.Append("<span style='text-align:left;'><b>Email: care@chandandiagnostic.com</b></span>");
                    b.Append("</div>");
                    b.Append("</div>");
                    b.Append("<div style='text-align:left;width:100%;float:left;'>");
                    b.Append("<span style='text-align:left;width:33%;float:left;'><b>CIN : " + dr["CIN"].ToString() + "</b></span>");
                    b.Append("<span style='text-align:right;width:33%;float:left;'><b>GSTIN : " + dr["GSTIN"].ToString() + "</b></span>");
                    b.Append("<span style='text-align:right;width:33%;float:right;'><b>HSN : " + dr["HSN"].ToString() + "</b></span>");
                    b.Append("</div>");
                    b.Append("<hr/>");

                    b.Append("<table style='width:100%;font-size:17px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
                    b.Append("<tr>");
                    b.Append("<td>Name</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["patient_name"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Bill No.</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["bill_no"].ToString() + "</td>");
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
                    b.Append("<td>" + dr["mobile_no"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Refered By</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["ref_name"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Address</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["address"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Panel</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td style='text-transform:uppercase'>" + dr["PanelName"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>UHID</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["UHID"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Reporting Time</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["ReportingTime"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Visit No</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["visitNo"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td>Consultant</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["DoctorName"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Home Colection</td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td colspan='5'>NO</td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                    CancelAgainstNo = dr["visitType"].ToString();
                    loginName = dr["loginName"].ToString();
                }
            }
            b.Append("<table  style='width:100%;font-size:18px;text-align:left;border:0px solid #dcdcdc;margin-top:10px;' border='0' cellspacing='0'>");
            b.Append("<tr>");
            b.Append("<th colspan='9'><hr style='margin-bottom:-2px;border:1px solid #000'></th>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<th style='width:1%;padding:3px;'>S.N.</th>");
            b.Append("<th style='width:70%;padding:3px;'>Test Name</th>");
            b.Append("<th style='width:10%;padding:3px;;text-align:right'>Rate(₹)</th>");
            b.Append("<th style='width:10%;padding:3px;;text-align:right'>P.Disc(₹)</th>");
            b.Append("<th style='width:10%;padding:3px;;text-align:right'>Adl.Disc(₹)</th>");
            b.Append("<th style='width:10%;padding:3px;;text-align:right'>NetAmt(₹)</th>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<th colspan='9'><hr style='margin-top:-4px;margin-bottom:-2px;border:1px solid #000'></th>");
            b.Append("</tr>");
            //Body
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                var count = 0;
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    count++;
                    b.Append("<tr>");
                    b.Append("<td style='width:1%;font-size:15px !important;padding:0;margin:2px'>" + count + "</td>");
                    b.Append("<td style='width:70%;font-size:15px !important;padding:0;margin:2px'>" + dr["ItemName"].ToString() + "</td>");
                    b.Append("<td style='width:10%;text-align:right;padding:0;margin:2px'>" + dr["panel_rate"].ToString() + "</td>");
                    b.Append("<td style='width:10%;text-align:right;padding:0;margin:2px'>" + dr["discount"].ToString() + "</td>");
                    b.Append("<td style='width:10%;text-align:right;padding:0;margin:2px'>" + dr["adl_discount"].ToString() + "</td>");
                    b.Append("<td style='width:10%;text-align:right;padding:0;margin:2px'>" + dr["amount"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td colspan='9'><hr style='margin:-1px 0;'></td>");
                    b.Append("</tr>");
                }
            }
            //b.Append("<tr>");
            //b.Append("<td></td><td colspan='3'><hr style='margin-top:-4px;margin-bottom:-6px;'></td>");
            //b.Append("</tr>");
            b.Append("</table>");

            b.Append("<div style='width:100%;float:left;margin-top:5px'>");
            b.Append("<div style='width:60%;float:left'>");

            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                b.Append("<table style='width:100%;font-size:14px;text-align:left;' border='1' cellspacing='0'>");
                b.Append("<tr>");
                b.Append("<th style='padding-left:5px'>Settlement</th>");
                b.Append("<th style='padding-left:5px'>Payment</th>");
                b.Append("<th style='padding-left:5px'>Receipt No</th>");
                b.Append("<th style='padding-left:5px'>Mode</th>");
                b.Append("<th style='text-align:right'>Amount</th>");
                b.Append("</tr>");
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    b.Append("<tr>");
                    b.Append("<td style='padding-left:5px'>" + dr["payType"].ToString() + "</td>");
                    b.Append("<td style='padding-left:5px'>" + dr["receiptDate"].ToString() + "</td>");
                    b.Append("<td style='padding-left:5px'>" + dr["ReceiptNo"].ToString() + "</td>");
                    b.Append("<td style='padding-left:5px'>" + dr["PayMode"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:5px'>" + dr["Amount"].ToString() + "</td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
            }
            if (CancelAgainstNo.Length > 10)
            {
                b.Append("<p style='width:100%;float:left;font-size:16px'>");
                b.Append("Cancel Against No : " + CancelAgainstNo);
                b.Append("</p>");
            }
            b.Append("</div>");
            b.Append("<div style='width:40%;float:right'>");
            b.Append("<table style='font-size:14px;float:right' border='0' cellspacing='0'>");
            b.Append("<tr style='font-size:16px'>");
            b.Append("<td colspan='3' style='width:80%;text-align:right'><b>Total Amount : </b></td>");
            b.Append("<td style='width:15%;text-align:right;white-space: nowrap;'><b>" + GrossAmount + "</b></td>");
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
            b.Append("<td colspan='3' style='width:80%;text-align:right'><b>Received Amount : </b></td>");
            b.Append("<td style='width:15%;text-align:right;white-space: nowrap;'><b>" + Received + "</b></td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            b.Append("<div style='text-align:left;width:100%;float:left;'>");
            b.Append("<span style='text-align:left;width:70%;float:left;'><b>Received with thanks : </b>" + AmountConverter.ConvertToWords(Convert.ToString(NetAmount).ToString()) + "</span>");
            b.Append("<span style='text-align:right;width:30%;float:right;'><b>" + loginName + "</b></span>");
            b.Append("</div>");
            b.Append("<div style='width:100%;float:left'><br/>");
            b.Append("<p style='font-size:16px;text-align:left;margin:0'>For any query, kindly get in touch with us on</p>");
            b.Append("<p style='font-size:16px;text-align:left;margin:0'><b>customercare@chandandiagnostic.com</b><img src=" + BarcodeGenerator.GenerateBarCode("TH/21-22/00000239", 300, 70) + " style='float:right' /></p>");
            b.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            //b.Append("<p style='font-size:15px;text-align:center'>गर्भ में पल रहे भ्रूण के लिंग की जाँच करना एक दंडनीय अपराध है.</p>");
            //b.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            b.Append("<p style='font-size:15px;text-align:center'><b>Attention Please!!<br/>Get upto 11% discount on pharmacy services through CHANDAN HEALTH CARD</b></p>");
            b.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            b.Append("</div>");

            //b.Append("<span style='text-aligh:center'>");
            //b.Append("<p style='font-size:13px'>09-Oct-2020 12:04PM</p>");
            //b.Append("<p style='font-size:13px'>Prepared By : Arshad Ahmad</p>");			
            //b.Append("<p style='font-size:13px'>Printed By : Mr. Vijay Singh</p>");
            //b.Append("</span>");						
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