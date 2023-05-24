$(document).ready(function () {        
    GetCenterMaster(); 
    GetPendingUpload(); 
});
function GetCenterMaster() {
    $("#ddlCentre").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
    var url = config.baseUrl + "/api/Unit/Unit_VerificationQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.CentreId = '-';
    objBO.VisitNo = '-';
    objBO.Prm1 = window.location.origin;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetCenterMaster";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {                             
                $.each(data.ResultSet.Table, function (key, val) {
                    $("#ddlCentre").append($("<option></option>").val(val.centreId).html(val.centre_name));
                });                            
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPendingUpload() {
    $('#tblPendingUpload tbody').empty();
    var url = config.baseUrl + "/api/Unit/Unit_VerificationQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.CentreId = $("#ddlCentre option:selected").val();
    objBO.VisitNo = '-';
    objBO.Prm1 = window.location.origin;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetPendingPresPatient";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                if (data.ResultSet.Table.length > 0) {
                    var tbody = '';
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += '<tr>';
                        tbody += '<td class="hide">' + val.CentreId + '</td>';
                        tbody += '<td>' + val.pmrssm_id + '</td>';
                        tbody += '<td>' + val.VisitNo + '</td>';
                        tbody += '<td>' + val.visitDate + '</td>';
                        tbody += '<td>' + val.PatientName + '</td>';
                        tbody += '<td>' + val.age  + '</td>';
                        tbody += '<td>' + val.gender + '</td>';
                        tbody += '<td><button class="btn btn-success btn-xs" onclick=selectRow(this);UploadPendingDoc(this)><i class="fa fa-upload">&nbsp;</i>Upload</button></td>';
                        tbody += '</tr>';
                    });
                    $('#tblPendingUpload tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UploadPendingDoc(elem) {
    _centerIdUploadPresc = $(elem).closest('tr').find('td:eq(0)').text();
    _visitNoUploadPresc = $(elem).closest('tr').find('td:eq(2)').text();
    PatientInfo();
}
