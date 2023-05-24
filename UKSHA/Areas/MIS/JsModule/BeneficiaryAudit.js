$(document).ready(function () {
    
});
function BeneficiaryMembers() {
    $("#tblBeneficiaryMembers tbody").empty();
    var url = config.baseUrl + "/api/Report/MIS_ReportQueries";
    var objBO = {};
    objBO.DistrictName = '-';
    objBO.CentreType = '-';
    objBO.CentreId = '';
    objBO.VisitNo = '-';
    objBO.Prm1 = $('#txtInput').val();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = "BeneficiaryMembers";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                var tbody = '';
                var count = 0;
                $.each(data.ResultSet.Table, function (key, val) {
                    count++;                
                    tbody += "<tr>";
                    tbody += "<td>" + val.pmrssm_id + "</td>";
                    tbody += "<td>" + val.PatientName + "</td>";
                    tbody += "<td class='text-right'>" + val.TotalCount + "</td>";                             
                    tbody += "<td><button class='btn-danger btn-go' onclick=BeneficiaryVisits('" + val.pmrssm_id + "')><i class='fa fa-sign-in'></i></button></td>";
                    tbody += "</tr>";
                });
                $("#tblBeneficiaryMembers tbody").append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BeneficiaryVisits(shaId) {
    $("#Report").empty();
    var url = config.baseUrl + "/api/Report/MIS_ReportQueries";
    var objBO = {};
    objBO.DistrictName = '-';
    objBO.CentreType = '-';
    objBO.CentreId = '';
    objBO.VisitNo = '-';
    objBO.Prm1 = shaId;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = "BeneficiaryVisits";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = "";
            var tbody1 = "";
            var VisitNo = "";
            var total = 0;
            var grandTotal = 0;
            $('#Report').empty();
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    if (VisitNo != val.VisitNo) {
                        tbody += '<div class="info">';
                        tbody += '<span><b>Name :</b> ' + val.PatientName + '   <b>Visit No :</b> ' + val.VisitNo + '</span>';
                        tbody += '<div class="table table-responsive" style="border:1px solid #ccc;padding: 3px;">';
                        tbody += '<table class="table-bordered" id="tblPatientReport" style="width: 100%;">';
                        tbody += '<tbody>';
                        VisitNo = val.VisitNo;
                        total = 0;
                        $.each(data.ResultSet.Table, function (key, val) {
                            if (VisitNo == val.VisitNo) {
                                total += val.netAmount;
                                grandTotal += val.netAmount;
                                tbody += '<tr>';
                                tbody += '<td>' + val.TestName + '</td>';
                                tbody += '<td class="text-right" style="width:10%">' + val.netAmount + '</td>';
                                tbody += '</tr>';
                            }
                        });
                        tbody += '<tr style="background:#ddd">';
                        tbody += '<td>Total Amount</td>';
                        tbody += '<td class="text-right">' + total.toFixed(0) + '</td>';
                        tbody += '</tr>';
                        tbody += '</tbody>';
                        tbody += '</table>';
                        tbody += '</div></div>';
                    }
                });
                tbody1 += '<table class="table-bordered" id="tblPatientReport" style="width: 100%;">';
                tbody1 += '<tr style="background:#ffd4d4">';
                tbody1 += '<td>Total Amount</td>';
                tbody1 += '<td class="text-right">' + grandTotal.toFixed(0) + '</td>';
                tbody1 += '</tr>';
                tbody1 += '</table>';
                tbody = tbody1 + tbody;
                $('#Report').append(tbody);

            }
            else {
                //alert('Data Not Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

