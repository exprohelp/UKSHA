using System;
using System.Data;
using System.Text;
using System.Web.Mvc;
using UKSHA.App_Start;
using UKSHA.Repository;
using UKSHAApi.Models;

namespace UKSHA.Areas.Invoice.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult InvoiceSummary()
        {
            return View();
        }
        public FileResult PrintSummarizedInvoice(string InvoiceNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            if (!TempData.ContainsKey("IsActive"))
            {
                return pdfConverter.ConvertToPdf("-", "<h3 style='text-align:center;color:red'>Your Session is Out. Kindly Login Again.</h3>", "-", "EQAS-Report.pdf");
            }
            ipInvoice obj = new ipInvoice();
            obj.InvoiceNo = InvoiceNo;
            obj.Logic = "PrintSummarizedInvoice";
            dataSet dsResult = APIProxy.CallWebApiMethod("Invoice/Invoice_Queries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            StringBuilder f = new StringBuilder();
            int Count = 0;
            double TotalAmount = 0;
            double Discount = 0;
            //double AdjAmount = 0;
            double NetAmount = 0;
            double AmountToBePaid = 0;
            string invoiceNo = string.Empty;
            string InvoiceType = string.Empty;
            double InvoiceAmount = 0;

            b.Append("<div style='width:100%;float:left;margin-top:-12px;padding:8px'>");
            string chandanLogo = HttpContext.Server.MapPath(@"~/Content/img/logo.png");
            string nhmLogo = HttpContext.Server.MapPath(@"~/Content/img/logo_nhm.png");
            //string QRCode = HttpContext.Server.MapPath(@"/Content/img/QRCode.png");
            b.Append("<div style='text-align:left;width:32%;float:left'>");
            b.Append("<img src=" + chandanLogo + " style='width:170px;margin-top:5px;' />");
            b.Append("</div>");
            b.Append("<div style='text-align:left;width:auto;float:left;width:60%;'>");
            b.Append("<h2 style='font-weight:bold;margin:0'>Chandan Healthcare Ltd</h2>");
            b.Append("<span style='text-align:left;'>Biotech Park, Kursi Road, Lucknow</span><br/>");
            b.Append("<span style='text-align:left;'><b>CIN No: U85196UP1995PLC018739</b>, <b>GSTIN : 09AACCC1996N1Z2</b></span><br/>");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("<hr/>");
            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                invoiceNo = ds.Tables[2].Rows[0]["InvoiceNo"].ToString();
                InvoiceType = ds.Tables[2].Rows[0]["InvoiceType"].ToString();
                InvoiceAmount = Convert.ToDouble(ds.Tables[2].Rows[0]["InvoiceAmount"].ToString());
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TotalAmount = Convert.ToDouble(dr["totalAmount"].ToString());
                    Discount = Convert.ToDouble(dr["totalDiscount"].ToString());
                    //AdjAmount = Convert.ToDouble(dr["adj_amount"].ToString());
                    NetAmount = Convert.ToDouble(dr["amount"].ToString());

                    b.Append("<table style='width:100%;font-size:14px;text-align:left;background:#ececec;'>");
                    b.Append("<tr>");
                    b.Append("<td><b>Invoice Type</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td><b>" + InvoiceType + "</b></td>");
                    b.Append("<td colspan='4'>&nbsp;</td>");
                    b.Append("<td><b>Vendor Name</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["VendorName"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Invoice No</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + invoiceNo + "</td>");
                    b.Append("<td colspan='4'>&nbsp;</td>");
                    b.Append("<td><b>Pan No</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["PanNo"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Invoice Month</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["InvoiceMonth"].ToString() + "</td>");
                    b.Append("<td colspan='4'>&nbsp;</td>");
                    b.Append("<td><b>Bill To</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["BillTo"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("</tr>");
                    b.Append("</table>");
                }
            }
            b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse;margin-top:10px;'>");
            b.Append("<tr>");
            b.Append("<th style='width:1%;text-align:left;padding-left:4px;'>S.No.</th>");
            b.Append("<th style='text-align:left;padding-left:4px;'>Test Category</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>No. of Test</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>Total Amount</th>");
            b.Append("</tr>");
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Count++;
                    b.Append("<tr>");
                    b.Append("<td style='padding-left:4px;'>" + Count + "</td>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["testCategory"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;'>" + Convert.ToInt32(dr["testCount"]) + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;'>" + Convert.ToInt32(dr["netAmount"]) + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("</table>");
            b.Append("<div style='width:100%;float:left;margin-top:5px'>");
            b.Append("<hr/>");
            b.Append("<div style='width:40%;float:left'>");
            b.Append("<b>Remark : </b>");
            b.Append("</div>");
            b.Append("<div style='width:60%;float:right'>");
            b.Append("<table style='font-size:12px;float:right' border='0' cellspacing='0'>");
            b.Append("<tr style='font-size:13px'>");
            b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Total Amount</b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + TotalAmount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");
            b.Append("<tr style='font-size:13px'>");
            b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Discount (45%) </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + Discount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");
            b.Append("<tr style='font-size:13px'>");
            b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Net Amount </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + NetAmount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");

            b.Append("<tr style='font-size:13px'>");
            b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Payable Amount</b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + InvoiceAmount + "</b></td>");
            b.Append("</tr>");
            //b.Append("<tr style='font-size:13px'>");
            //b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Prev. Month EQA Amount (20%)</b></td>");
            //b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            //b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            //b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + equas_prvMonth.ToString("0.00") + "</b></td>");
            //b.Append("</tr>");

            b.Append("</table>");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("<div style='text-align:right;width:100%;float:right;'>");
            b.Append("<span style='text-align:right;width:70%;float:right;font-size:13px'><b>Amount in Words : </b>" + AmountConverter.ConvertToWords(Convert.ToString(InvoiceAmount).ToString()) + "</span>");
            b.Append("</div>");

            f.Append("<div style='width:100%;float:left;margin-top:5px;zoom:1.5'>");
            f.Append("<div style='width:50%;float:left'>");
            string QRCode = GenerateQRCode.GenerateMyQCCode("https://exprohelp.com/UKNHM//Invoice/Invoice/PrintInvoice?InvoiceNo=" + InvoiceNo);
            f.Append("<img src=" + QRCode + " style='width:100px;margin-top:5px;' />");
            f.Append("</div>");
            f.Append("<div style='width:50%;float:right;margin-top:85px;text-align:center'>");
            f.Append("<hr/>Authorized Signature");
            f.Append("</div>");
            f.Append("</div>");
            f.Append("<div style='width:100%;float:left'><br/>");
            f.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            f.Append("<p style='font-size:13px;text-align:center'>Note : this is system generated invoice, it is strictly recommended to check every entries (including recoveries)</p>");
            f.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            f.Append("</div>");

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 135;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "Print-Invoice.pdf");
        }
        public FileResult PrintDetailedInvoice(string InvoiceNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            if (!TempData.ContainsKey("IsActive"))
            {
                return pdfConverter.ConvertToPdf("-", "<h3 style='text-align:center;color:red'>Your Session is Out. Kindly Login Again.</h3>", "-", "Bill-Report.pdf");
            }
            ipInvoice obj = new ipInvoice();
            obj.InvoiceNo = InvoiceNo;
            obj.Logic = "PrintDetailedInvoice";
            dataSet dsResult = APIProxy.CallWebApiMethod("Invoice/Invoice_Queries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            StringBuilder f = new StringBuilder();
            int Count = 0;
            double TotalAmount = 0;
            double GroupTotalAmount = 0;
            double Discount = 0;
            //double AdjAmount = 0;
            double NetAmount = 0;
            double AmountToBePaid = 0;
            string invoiceNo = string.Empty;
            string InvoiceType = string.Empty;
            double InvoiceAmount = 0;

            b.Append("<div style='width:100%;float:left;margin-top:-12px;padding:8px'>");
            string chandanLogo = HttpContext.Server.MapPath(@"~/Content/img/logo.png");
            string nhmLogo = HttpContext.Server.MapPath(@"~/Content/img/logo_nhm.png");
            //string QRCode = HttpContext.Server.MapPath(@"/Content/img/QRCode.png");
            b.Append("<div style='text-align:left;width:32%;float:left'>");
            b.Append("<img src=" + chandanLogo + " style='width:170px;margin-top:5px;' />");
            b.Append("</div>");
            b.Append("<div style='text-align:left;width:auto;float:left;width:60%;'>");
            b.Append("<h2 style='font-weight:bold;margin:0'>Chandan Healthcare Ltd</h2>");
            b.Append("<span style='text-align:left;'>Biotech Park, Kursi Road, Lucknow</span><br/>");
            b.Append("<span style='text-align:left;'><b>CIN No: U85196UP1995PLC018739</b>, <b>GSTIN : 09AACCC1996N1Z2</b></span><br/>");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("<hr/>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TotalAmount = Convert.ToDouble(dr["totalAmount"].ToString());
                    Discount = Convert.ToDouble(dr["totalDiscount"].ToString());
                    invoiceNo = dr["InvoiceNo"].ToString();
                    InvoiceType = dr["InvoiceType"].ToString();
                    NetAmount = Convert.ToDouble(dr["amount"].ToString());

                    b.Append("<table style='width:100%;font-size:14px;text-align:left;background:#ececec;'>");
                    b.Append("<tr>");
                    b.Append("<td><b>Invoice Type</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td><b>" + InvoiceType + "</b></td>");
                    b.Append("<td colspan='4'>&nbsp;</td>");
                    b.Append("<td><b>Vendor Name</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["VendorName"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Invoice No</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + invoiceNo + "</td>");
                    b.Append("<td colspan='4'>&nbsp;</td>");
                    b.Append("<td><b>Pan No</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["PanNo"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Invoice Month</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["InvoiceMonth"].ToString() + "</td>");
                    b.Append("<td colspan='4'>&nbsp;</td>");
                    b.Append("<td><b>Bill To</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["BillTo"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("</tr>");
                    b.Append("</table>");
                }
            }
            b.Append("<table border='0' style='width:100%;font-size:11px;border-collapse: collapse;margin-top:10px;'>");
            b.Append("<tr>");
            b.Append("<th style='width:1%;text-align:left;padding-left:4px;'>SHA Id </th>");
            b.Append("<th style='text-align:left;padding-left:4px;'>Visit No</th>");
            b.Append("<th style='text-align:left;padding-left:4px;'>Visit Date</th>");
            b.Append("<th style='text-align:left;padding-left:4px;'>Patient Name</th>");
            b.Append("<th style='text-align:left;padding-left:4px;'>Age Info</th>");
            b.Append("<th style='text-align:left;padding-left:4px;'>Prescribed By</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>Amount</th>");
            b.Append("</tr>");
            string temp = "";
            int Counter = 0;
            int totalRow = ds.Tables[1].Rows.Count;
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Counter++;
                    GroupTotalAmount += Convert.ToDouble(dr["Amount"].ToString());
                    if (temp != dr["GrpName"].ToString())
                    {
                        Count++;
                        if (Count > 1)
                        {
                            b.Append("<tr>");
                            b.Append("<td colspan='6' style='white-space: nowrap;padding-left:4px;text-right;font-size:13px'><b>Total Amount </b></td>");
                            b.Append("<td style='white-space: nowrap;padding-left:4px;text-right;font-size:13px'><b>" + GroupTotalAmount + "</b></td>");
                            b.Append("</tr>");
                        }
                        GroupTotalAmount = 0;
                        b.Append("<tr style='background:#ddd'>");
                        b.Append("<td colspan='7' style='white-space: nowrap;padding-left:4px;'>" + dr["GrpName"].ToString() + "</td>");
                        b.Append("</tr>");
                        temp = dr["GrpName"].ToString();
                    }

                    b.Append("<tr>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["pmrssm_id"].ToString() + "</td>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["VisitNo"].ToString() + "</td>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["visitDate"].ToString() + "</td>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["PatientName"].ToString() + "</td>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["AgeInfo"].ToString() + "</td>");
                    b.Append("<td style='white-space: nowrap;padding-left:4px;'>" + dr["PrescribedBy"].ToString() + "</td>");
                    b.Append("<td style='white-space: nowrap;text-align:right;padding-right:4px;'>" + dr["Amount"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td colspan='7' style='white-space: nowrap;padding-left:4px;'><b>Test : " + dr["Investigations"].ToString() + "</b></td>");
                    b.Append("</tr>");
                    if (totalRow == Counter) {
                        b.Append("<tr>");
                        b.Append("<td colspan='6' style='white-space: nowrap;padding-left:4px;text-right;font-size:13px'><b>Total Amount </b></td>");
                        b.Append("<td style='white-space: nowrap;padding-left:4px;text-right;font-size:13px'><b>" + GroupTotalAmount + "</b></td>");
                        b.Append("</tr>");
                    }
                }
            }
            b.Append("</table>");

            f.Append("<div style='width:100%;float:left;margin-top:5px;zoom:1.5'>");
            f.Append("<div style='width:50%;float:left'>");
            string QRCode = GenerateQRCode.GenerateMyQCCode("https://exprohelp.com/UKNHM//Invoice/Invoice/PrintInvoice?InvoiceNo=" + InvoiceNo);
            f.Append("<img src=" + QRCode + " style='width:100px;margin-top:5px;' />");
            f.Append("</div>");
            f.Append("<div style='width:50%;float:right;margin-top:85px;text-align:center'>");
            f.Append("<hr/>Authorized Signature");
            f.Append("</div>");
            f.Append("</div>");
            f.Append("<div style='width:100%;float:left'><br/>");
            f.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            f.Append("<p style='font-size:13px;text-align:center'>Note : this is system generated invoice, it is strictly recommended to check every entries (including recoveries)</p>");
            f.Append("<p><hr style='margin-top:-14px;margin-bottom:-14px;border:1px solid #000'></p>");
            f.Append("</div>");

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 135;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "Print-Invoice.pdf");
        }
    }
}