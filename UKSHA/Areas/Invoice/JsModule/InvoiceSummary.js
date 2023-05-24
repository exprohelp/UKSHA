$(document).ready(function () {
    //CloseSidebar();
    FillCurrentMonth('txtFrom');
    FillCurrentMonth('txtTo');
    GetCenter();
});
function InvoiceList(logic) {
    $("#tblInvoiceSummary tbody").empty();
    var url = config.baseUrl + "/api/Invoice/Invoice_Queries";
    var objBO = {};
    objBO.CentreId = $('#ddlCentreName option:selected').val();
    objBO.InvoiceNo = '-';
    objBO.Prm1 = '-';
    objBO.from = $('#txtFrom').val() + '-01';
    objBO.to = '1900/01/01';
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {           
            var centreName = '';
            if (data.ResultSet.Table.length > 0) {
                var tbody = '';
                var centre = '';
                var totalAmount = 0;
                var totalDiscount = 0;
                var amount = 0;
                var InvoiceAmount = 0;    
                $.each(data.ResultSet.Table, function (key, val) {                  
                    totalAmount += val.totalAmount;
                    totalDiscount += val.totalDiscount;
                    amount += val.amount;
                    InvoiceAmount += val.InvoiceAmount;                   

                    tbody += "<tr>";
                    tbody += "<td>" + val.InvoiceMonth.replace('1', '') + "</td>";
                    tbody += "<td style='white-space: nowrap'>"
                    tbody += "<i class='fa fa-file-pdf-o'>&nbsp;</i><a href='PrintInvoice?InvoiceNo=" + val.InvoiceNo + "' target='_blank'>" + val.InvoiceNo + "</a>";
                    tbody += "<a target='_blank' href='PrintDetailedInvoice?InvoiceNo=" + val.InvoiceNo + "' class='btn btn-primary btn-xs pull-right' style='margin-left: 3px;'>Detailed</a>";
                    tbody += "<a target='_blank' href='PrintSummarizedInvoice?InvoiceNo=" + val.InvoiceNo + "' class='btn btn-warning btn-xs pull-right'>Summarized</a>";
                    tbody += "</td>";
                    if (val.totalAmount == 0)
                        tbody += "<td class='text-right'></td>";
                    else
                        tbody += "<td class='text-right'>" + val.totalAmount + "</td>";

                    if (val.totalDiscount == 0)
                        tbody += "<td class='text-right'></td>";
                    else
                        tbody += "<td class='text-right'>" + val.totalDiscount + "</td>";

                    if (val.amount == 0)
                        tbody += "<td class='text-right'></td>";
                    else
                        tbody += "<td class='text-right'>" + val.amount + "</td>";

                    tbody += "<td class='text-right'><type style='font-size:8px'>" + val.InvoiceType + '</type> : <b>' + val.InvoiceAmount + "</b></td>";                   
                    tbody += "</tr>";
                });
                tbody += "<tr>";
                tbody += "<th colspan='2' class='text-right'>Total Amount</th>";
                tbody += "<th class='text-right'>" + totalAmount.toFixed(2) + "</th>";
                tbody += "<th class='text-right'>" + totalDiscount.toFixed(2) + "</th>";
                tbody += "<th class='text-right'>" + amount.toFixed(2) + "</th>";
                tbody += "<th class='text-right'>" + InvoiceAmount.toFixed(2) + "</th>";               
                tbody += "</tr>";
                $("#tblInvoiceSummary tbody").append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}