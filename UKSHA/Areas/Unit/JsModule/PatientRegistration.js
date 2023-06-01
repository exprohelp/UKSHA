var _doctorId = '';
var _memberInfo = '';
var _pmrssmId = '';
var _isActive = '';
var _emp_code = '';
$(document).ready(function () {
    $('#txtSHAId').on('keyup', function (e) {
        if (e.keyCode == 13)
            getMemberInfo()
    });

    GetCenterMaster();   
    GetTestMaster();
    GetDegreeSpec();

    $('#tblTest tbody').on('change', 'input:checkbox', function () {
        $('#tblSelectedTest tbody').empty();
        var tbody = '';
        $('#tblTest tbody tr').find('input[type=checkbox]:checked').each(function () {
            tbody += "<tr>";
            tbody += "<td><button class='btn btn-danger btn-xs btnDelete'><i class='fa fa-trash'></i></button></td>";
            tbody += "<td>" + $(this).closest('tr').find('td:eq(1)').text() + "</td>";
            tbody += "<td>" + $(this).closest('tr').find('td:eq(2)').text() + "</td>";
            tbody += "</tr>";
        });
        $('#tblSelectedTest tbody').append(tbody);
    });
    $('#tblSelectedTest tbody').on('click', 'button.btnDelete', function () {
        var testCode = $(this).closest('tr').find('td:eq(1)').text();
        $('#tblTest tbody tr').each(function () {
            if ($(this).find('td:eq(1)').text() == testCode)
                $(this).find('input[type=checkbox]').prop('checked', false);
        });
        $(this).closest('tr').remove();
    });
});
function GetCenterMaster() {
    $("#ddlCentre").empty();
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
                $("#ddlCentre").select2();
                GetDoctorMaster();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDegreeSpec() {
    var url = config.baseUrl + "/api/ApplicationResources/MasterQueries";
    var objBO = {};
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetDegreeSpec";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                //Degree
                $("#ddlDegree").empty().append($("<option></option>").val("Select").html("Select")).select2();
                $.each(data.ResultSet.Table, function (key, val) {
                    $("#ddlDegree").append($("<option></option>").val(val.DegId).html(val.DegreeName));
                });
                //Specialization
                $("#ddlSpecialization").empty().append($("<option></option>").val("Select").html("Select")).select2();
                $.each(data.ResultSet.Table1, function (key, val) {
                    $("#ddlSpecialization").append($("<option></option>").val(val.Spec_id).html(val.SpecializationName));
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function getMemberInfo() {    
    _emp_code = "";
    if ($('#txtSHAId').val() == '') {
        alert('Please Provide SHA Id');
        return
    }
    //var url = "http://103.116.27.113/AYUSH_LabAPI/api/SHA/GetMemberInformationJSON";
    var url = config.baseUrl + "/api/Unit/getMemberInfo";
    var AuthKey = 'XBKJGFPPUHBC178HJKLP984LKJGDCNMLK9087640';
    var pmrssm_id = $('#txtSHAId').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(pmrssm_id),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {            
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    _memberInfo = JSON.stringify(data.ResultSet.Table[0]);
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtMemberId').val(val.member_id);
                        $('#txtMemberName').val(val.member_name_eng);
                        $('#txtFamilyId').val(val.family_id);
                        $('#txtMobileNo').val(val.mobile_member);
                        _emp_code = val.empcode;
                        if (val.dob==null) {
                            $('#txtDOB').val(val.dob).prop('disabled', false);
                        }
                        else {
                            $('#txtDOB').val(val.dob).prop('disabled', true);
                        }
                        if (val.gender == 'F')
                            $('#txtGender').val('Female');
                        else
                            $('#txtGender').val('Male');

                        $('#txtStateName').val(val.state_name_ben);
                        $('#txtDistrictName').val(val.district_name_ben);
                        $('#txtMemberStatus').removeClass('activeStatus').removeClass('activeNotStatus');
                        _isActive = val.isactive;
                        if (val.isactive == 'ACTIVE') {
                            $('#txtMemberStatus').html('<i class="fa fa-check-circle">&nbsp;</i>Active').addClass('activeStatus');
                            $('#btnSave').prop('disabled', false);
                        }
                        else {
                            $('#txtMemberStatus').html('<i class="fa fa-close">&nbsp;</i>Not Active').addClass('activeNotStatus');
                            $('#btnSave').prop('disabled', true);
                        }
                        $('#txtSHAId').prop('disabled', true);
                    });
                    GetSHAPatientId();
                }
                else
                    alert('Record Not Found for this SHA Id.');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetSGHSEmpContributionData() {
    if (_emp_code == '') {
        alert('Emp Code Not Found.')
        return
    }
    $('#tblContInfo tbody').empty();
    var url = config.baseUrl + "/api/Unit/GetSGHSEmpContributionData";
    var AuthKey = 'XBKJGFPPUHBC178HJKLP984LKJGDCNMLK9087640';
    var emp_code = _emp_code;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(emp_code),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            if (data.record.length > 0) {
                var tbody = '';
                var count = 0;
                $.each(data.record, function (key, val) {
                    count++;
                    tbody += '<tr>';
                    tbody += '<td>' + val.Emp_name + '</td>';
                    tbody += '<td>' + val.Contribution_month + '</td>';
                    tbody += '<td class="text-right">' + val.Contribution_amount + '</td>';
                    tbody += '</tr>';
                });
                $('#tblContInfo tbody').append(tbody);
                $('#modalContribution').modal('show');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctorMaster() {
    $('#tblDoctorMaster tbody').empty();
    var url = config.baseUrl + "/api/ApplicationResources/MasterQueries";
    var objBO = {};
    objBO.Prm1 = $("#ddlCentre option:selected").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "DoctorInfoForPatientRegistration";
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
                        tbody += '<td>' + val.DoctorId + '</td>';
                        tbody += '<td>' + val.DoctorName + '</td>';
                        tbody += '<td>' + val.MobileNo + '</td>';
                        tbody += '<td>' + val.degree + '</td>';
                        tbody += '<td>' + val.specialization + '</td>';
                        tbody += '<td><button class="btn-danger" onclick=selectRow(this);doctorSelection(this)><i class="fa fa-sign-in">&nbsp;</i></button></td>';
                        tbody += '</tr>';
                    });
                    $('#tblDoctorMaster tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetTestMaster() {
    $('#tblTest tbody').empty();
    var url = config.baseUrl + "/api/ApplicationResources/MasterQueries";
    var objBO = {};
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetTestMaster";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            if (data.ResultSet.Table.length > 0) {
                if (data.ResultSet.Table.length > 0) {
                    var tbody = '';
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><input type='checkbox'/></td>";
                        tbody += "<td>" + val.testCode + "</td>";
                        tbody += "<td>" + val.testName + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTest tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertDoctor() {
    if (Validation()) {
        if (confirm('Are you sure to Submit?')) {
            var url = config.baseUrl + "/api/ApplicationResources/InsertUpdatemaster";
            var objBO = {};
            objBO.DoctorId = _doctorId;
            objBO.DoctorName = $('#txtDoctorName').val();
            objBO.Degree = $('#ddlDegree option:selected').text();
            objBO.Specialization = $('#ddlSpecialization option:selected').text();
            objBO.CenterId = $("#ddlCentre option:selected").val();
            objBO.MobileNo = $('#txtMobile').val();
            objBO.Prm1 = '-';
            objBO.Prm2 = '-';
            objBO.login_id = Active.userId;
            objBO.Logic = 'InsertDoctor';
            $.ajax({
                method: "POST",
                url: url,
                data: JSON.stringify(objBO),
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data.includes('Success')) {
                        alert(data);
                        GetDoctorMaster();
                        $('#txtDoctorName').val('');
                        $('#txtMobile').val('');
                        $('.newDoctor').slideUp()
                    }
                    else {
                        alert(data);
                    }
                },
                error: function (response) {
                    alert('Server Error...!');
                }
            });

        }
    }

}
function InsertPatient() {
    if (confirm('Are you sure to Submit?')) {
        if ($('#txtMemberId').val() == '') {
            alert('Please Provide Member Info.');
            return
        }
        if ($('#txtABHAId').val() == '') {
            alert('Please Provide ABHA Id.');
            $('#txtABHAId').focus();
            return
        }
        if (_doctorId == '') {
            alert('Please Select Doctor.');
            return
        }
        if ($('#tblSelectedTest tbody tr').length == 0) {
            alert('Please Select Test.');
            return
        }
        if ($('#txtDOB').val() == '') {
            alert('Please Select DOB.');
            $('#txtDOB').focus();
            return
        }
        var url = config.baseUrl + "/api/Unit/Register_Patient";
        var objBO = {};
        var selectedTestCodes = [];
        $('#tblSelectedTest tbody tr').each(function () {
            selectedTestCodes.push($(this).find('td:eq(1)').text())
        });
        if (_isActive != 'ACTIVE') {
            alert('Patient Not Active');
            return
        }
        var Info = JSON.parse(_memberInfo);
        var dob = Info.dob.split('-')[2] + '-' + Info.dob.split('-')[1] + '-' + Info.dob.split('-')[0];
        //TEST
        objBO.pmrssm_id = Info.pmrssm_id;
        objBO.trea_code = Info.trea_code;
        objBO.trea_name = Info.trea_name;
        objBO.ddo_code = Info.ddo_code;
        objBO.ddo_name = Info.ddo_name;
        objBO.family_id = Info.family_id;
        objBO.emp_pen_code = Info.emp_pen_code;
        objBO.member_id = Info.member_id;
        objBO.ABHA_Id = $('#txtABHAId').val();
        objBO.member_name_eng = $('#txtMemberName').val();
        objBO.dob = $('#txtDOB').val();
        objBO.gender = (Info.gender == 'M') ? 'Male' : 'Female';
        objBO.relation = Info.relation;
        objBO.care_of_type_dec = Info.care_of_type_dec;
        objBO.rural_urban_ben = Info.rural_urban_ben;
        objBO.state_name_ben = $('#txtStateName').val();
        objBO.district_name_ben = $('#txtDistrictName').val();
        objBO.member_type = Info.member_type;
        objBO.mobile_member = '9670244590';
        //objBO.mobile_member = Info.mobile_member;
        objBO.CentreId = $("#ddlCentre option:selected").val();
        objBO.doctorId = _doctorId;
        objBO.doctorName = $('#selectedDoctor').text();
        objBO.tyepofemployee = Info.tyepofemployee;
        objBO.remark = $('#txtRemark').val();
        objBO.TestCodes = selectedTestCodes.join('|');
        objBO.login_id = Active.userId;
        objBO.Logic = 'Insert';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    _visitNoUploadPresc = data.split('|')[1];
                    _centerIdUploadPresc = $("#ddlCentre option:selected").val();
                    Clear();
                    $('#modalResponse').modal('show');
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });

    }
}
function OpenUploadPresc() {
    PatientInfo();
    $('#modalResponse').modal('hide')
}
function doctorSelection(elem) {
    _doctorId = $(elem).closest('tr').find('td:eq(0)').text();
    $('#selectedDoctor').text($(elem).closest('tr').find('td:eq(1)').text());
}
function GetSHAPatientId() {  
    var url = config.baseUrl + "/api/Unit/LabQueries";
    var objBO = {};
    objBO.VisitNo = '-';
    objBO.SHAId = $('#txtSHAId').val(); 
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetSHAPatientInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    $("#txtABHAId").val(val.ABHA_Id);
                });               
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Validation() {
    var doctorName = $('#txtDoctorName').val();
    var degree = $('#ddlDegree option:selected').text();
    var specialization = $('#ddlSpecialization option:selected').text();

    if (doctorName == '') {
        alert('Please Provide Doctor Name.');
        $('#txtDoctorName').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtDoctorName').removeAttr('style');
    }
    if (degree == 'Select') {
        alert('Please Select Degree.');
        $('span.selection').find('span[aria-labelledby=select2-ddlDegree-container]').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDegree-container]').removeAttr('style');
    }
    if (specialization == 'Select') {
        alert('Please Select Specialization.');
        $('span.selection').find('span[aria-labelledby=select2-ddlSpecialization-container]').css({ 'border-color': 'red' }).focus();
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlSpecialization-container]').removeAttr('style');
    }
    return true;
}
function Clear() {
    $('input[type=text]').val('');
    $('#txtSHAId').prop('disabled', false);
    $('#textarea').val('');
    $('#tblTest tbody input:checkbox').prop('checked', false);
    $('#tblSelectedTest tbody').empty();
    $('#txtMemberStatus').html('');
    $('#txtMemberStatus').removeClass('activeStatus').removeClass('activeNotStatus');
}

function getMemberInfo1() {
    //var url = "http://103.116.27.113/AYUSH_LabAP/api/SHA/SyncChandanPatientData"; 
    var url = config.baseUrl + "/api/SHA/SyncChandanPatientData";
    var objBO = {};
    objBO.SGHS_Card = 'SGHSCard5764',
        objBO.abhaNo = 'AbhaNo789',
        objBO.claim_id = 'CLM345',
        objBO.visit_id = 'VST345',
        objBO.benef_Name = 'Ajeet Kumar Maurya',
        objBO.tyepOfEmployee = 'REGULAR',
        objBO.empcode = 'CHCL-00631',
        objBO.d_o_b = '1993-05-03',
        objBO.depttCode = 'depttCode123',
        objBO.depttName = 'depttName123',
        objBO.tres_code = 'tresCode1234',
        objBO.tres_Name = 'tresName123',
        objBO.totalcost = 3500,
        objBO.discount = 10,
        objBO.net = 400,
        objBO.billFile = '-',
        objBO.prescrFile = '-',
        objBO.vendorID = 'VDR123',
        objBO.PayVendSyncFlag = 'Y',
        objBO.PayVendSyncDate = '2023-05-18'

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data)
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PrintBill() {
    if (_visitNoUploadPresc.length < 2) {
        alert('Visit No Not Found.');
        return
    }
    //var url = config.rootUrl + "/MIS/Print/PrintBillByKey?visitNo=" + _visitNoUploadPresc;
    var url = config.rootUrl + "/MIS/Print/PrintBill?visitNo=" + _visitNoUploadPresc;
    window.open(url, '_blank');
}