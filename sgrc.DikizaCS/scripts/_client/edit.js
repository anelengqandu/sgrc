
var mainJson = null;
var mainJsonPrograms = null;
var successF = null;
var errorF = null;
var tblStudents = null;
var tblPrograms = null;
var validations = null;
var studentId = null;
var programId = null;
var clientId = null;

var validationsCredantials = null;

$(document).ready(function () {
    clientId = $('#hf_clientId').val();
    mainJson = jQuery.parseJSON($('#jsonData').html());
    mainJsonPrograms = jQuery.parseJSON($('#jsonDataPrograms').html());

    getStudents();
    getPrograms();
    
    $('.bootstrap-switch-default').click(function () {
        $('.make-switch').attr("checked", true);
    });

    $('.bootstrap-switch-primary').click(function () {
        $('.make-switch').attr("checked", false);
    });

    $('#cboPasswordOption').change(function () {
        var option = $(this).find('option:selected').val();
        getChange(option);
    });
});

function onRegisterProgram() {
    clear();
    $('#btnSaveProgramData').attr('onClick', 'onSaveProgram()');
}


function onSaveProgram() {
    $('#loader').show();
    var NameO = $('#txtProgramName');
    var DescriptionO = $('#txtProgramDescription');


    var Name = NameO.val();
    var Description = DescriptionO.val();



    validations = [
        sgrc.valid8r.req(Name, NameO),
        sgrc.valid8r.req(Description, DescriptionO)];

    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();
        return false;
    }

    jsonObject = {
        Name: Name,
        Description: Description,
        ClientId: clientId
    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            sgrc.Growl("Program registration successful!", "Success registration", "info", 9000);
            //location.reload();
            clear();
            closePop();
            tblPrograms.fnDraw();
        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error Login", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error registration", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/program/create/', jsonObject, successF, errorF);

}
function getChange(option) {
    switch (option) {
        case 'auto':
            $('#divAutoPassword').show(); $('.credentials').hide();
            $('#divManualPassword').hide();
            break;
        case 'custom':
            $('#divAutoPassword').hide(); $('.credentials').show();
            $('#divManualPassword').show();
            break;
        default:
            $('#divAutoPassword').hide(); $('.credentials').show();

    }
}


function onEditClientCredantials(id) {

    var option = $('#cboPasswordOption').val();

    var passwordO = $('#txtPassword');
    var confirmPasswordO = $('#txtConfirmPassword');


    var passwordCharsO = $('#input-chars');
    var passwordNonAphaO = $('#input-nonalphanumerics');

    var password = passwordO.val();
    var confirmPassword = confirmPasswordO.val();

    var passwordChars = passwordCharsO.val();
    var passwordNonApha = passwordNonAphaO.val();

    if (!sgrc.valid8r.passwords(password, passwordO, confirmPassword, confirmPasswordO)) {
        $('#loader').hide();
        sgrc.Growl("Password issues on credantials tab", "Client Credantials", "danger", 9000);
        return false;
    }


}


function getPrograms() {
   // var clientId = $('#hf_clientId').val();
    tblPrograms = $('#tblPrograms').dataTable({
        "oLanguage": { "sEmptyTable": "No data to display" },
        "bJQueryUI": false,
        "bAutoWidth": false,
        "sPaginationType": "full_numbers",
        "sAjaxSource": '/user/api/program/dtPrograms/' + clientId,
        "aaData": mainJsonPrograms.aaData,
        "iDisplayLength": 10,
        "bProcessing": true,
        "bServerSide": true,
        "bSort": false,
        "bRetrieve": true,
        "aoColumns": [{ "sTitle": "Program Name", "mDataProp": "Name" },
        { "sTitle": "Description", "mDataProp": "Description" },
        { "sTitle": "Created Date", "mDataProp": "CreationDateTimeS" },
        { "sTitle": "Active Status", "mDataProp": "IsActive", "sClass": "text-center w110px" },

        { "sTitle": "Actions", "mDataProp": "Id", "sClass": "text-center w110px" }
        ],
        "iDeferLoading": [mainJsonPrograms.count, mainJsonPrograms.count],
        "aoColumnDefs": [{ "bVisible": false, "aTargets": [] }],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            debugger;
            var btnHtml = '';
            $('td:eq(3)', nRow).html('<img  ' + (aData["IsActive"] ? ' src="/Resources/images/icons/tick132.png"' : 'src="/Resources/images/icons/cross32.png"')
                + '" style="cursor:pointer;"  onclick="this.src = onSaveProgramActiveStatus(' + aData["Id"] + ')" ><i></i>');


            btnHtml += ' <a href="#" data-toggle="modal" data-target="#ModalProgram"  onclick="onViewProgram(' + iDisplayIndex+ ')" class="btn btn-md btn-default"  data-toggle="tooltip" title="" data-original-title="Status"><i class="fa fa-eye"></i>&nbsp;View</a>';

            $('td:eq(4)', nRow).html(btnHtml);
        }

    });

    $('.dataTables_filter input').addClass('form-control').attr('placeholder', 'Search');
    $('.dataTables_length select').addClass('form-control');
}

function getStudents() {
    var id = $('#hf_clientId').val();
    tblStudents = $('#tblStudents').dataTable({
        "oLanguage": { "sEmptyTable": "No data to display" },
        "bJQueryUI": false,
        "bAutoWidth": false,
        "sPaginationType": "full_numbers",
        "sAjaxSource": '/user/api/clients/dtclientstudents/' + id,
        "aaData": mainJson.aaData,
        "iDisplayLength": 10,
        "bProcessing": true,
        "bServerSide": true,
        "bSort": false,
        "bRetrieve": true,
        "aoColumns": [
            { "sTitle": "FullName", "mDataProp": "Fullname" },
            { "sTitle": "Email", "mDataProp": "Email" },
            { "sTitle": "Primary Phone", "mDataProp": "PhoneNumber" },
            { "sTitle": "Active Status", "mDataProp": "IsActive", "sClass": "text-center w110px" },

            { "sTitle": "Actions", "mDataProp": "Id", "sClass": "text-center w110px" }
        ],
        "iDeferLoading": [mainJson.count, mainJson.count],
        "aoColumnDefs": [{ "bVisible": false, "aTargets": [] }],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            
            var btnHtml = '';
            $('td:eq(3)', nRow).html('<img  ' + (aData["IsActive"] ? ' src="/Resources/images/icons/tick132.png"' : 'src="/Resources/images/icons/cross32.png"')
                + '" style="cursor:pointer;"  onclick="this.src = onSaveActiveStatus(' + aData["Id"] + ')" ><i></i>');


            btnHtml += ' <a href="#" data-toggle="modal" data-target="#Modal"  onclick="onView(' + iDisplayIndex + ')" class="btn btn-md btn-default"  data-toggle="tooltip" title="" data-original-title="Status"><i class="fa fa-eye"></i>&nbsp;View</a>';

            $('td:eq(4)', nRow).html(btnHtml);
        }

    });

    $('.dataTables_filter input').addClass('form-control').attr('placeholder', 'Search');
    $('.dataTables_length select').addClass('form-control');
}

function onView(idx) {
    var aData = tblStudents.fnGetData(idx);
    studentId = aData["Id"];
    $("#txtStudentName").val(aData["Name"]);
    $("#txtStudentSurname").val(aData["Surname"]);
    $("#txtStudentEmail").val(aData["Email"]);
    $("#txtStudentPhoneNumber").val(aData["PhoneNumber"]);
    $("#txtStudentTelephoneNumber").val(aData["TelephoneNumber"]);
    $('#btnSaveData').attr('onClick', 'onSaveChanges()');
}

function onViewProgram(idx) {
    var aData = tblPrograms.fnGetData(idx);
    programId = aData["Id"];
    $("#txtProgramName").val(aData["Name"]);
    $("#txtProgramDescription").val(aData["Description"]);
    $('#btnSaveProgramData').attr('onClick', 'onSaveProgramChanges()');
}


function onRegisterStudent() {
    clear();

    $('#btnSaveData').attr('onClick', 'onSave()');
}

function onSave() {

    $('#loader').show();
    var nameO = $("#txtStudentName");
    var surnameO = $("#txtStudentSurname");
    var emailO = $("#txtStudentEmail");
    var phoneNumberO = $("#txtStudentPhoneNumber");
    var telephoneO = $("#txtStudentTelephoneNumber");

    var name = nameO.val();
    var surname = surnameO.val();
    var email = emailO.val();
    var phonenumber = phoneNumberO.val();
    var telephone = telephoneO.val();


    validations = [
        sgrc.valid8r.req(name, nameO),
        sgrc.valid8r.req(surname, surnameO),
        sgrc.valid8r.req(email, emailO),
        sgrc.valid8r.req(telephone, telephoneO)];


    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();

        return false;
    }

    jsonObject = {
        ClientId: clientId,
        Name: name,
        PhoneNumber: phonenumber,
        Surname: surname,
        Email: email,
        TelephoneNumber: telephone


    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            closeEditPop();
            tblStudents.fnDraw();
            clear();
            sgrc.Growl("Student successfully updated!", "Success", "info", 9000);

        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error update", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error update", "warning", 9000);
    }
    sgrc.ajax.post('/api/student/create/', jsonObject, successF, errorF);
}

function onSaveChanges() {
    $('#loader').show();
    var nameO = $("#txtStudentName");
    var surnameO = $("#txtStudentSurname");
    var emailO = $("#txtStudentEmail");
    var phoneNumberO = $("#txtStudentPhoneNumber");
    var telephoneO = $("#txtStudentTelephoneNumber");

    var name = nameO.val();
    var surname = surnameO.val();
    var email = emailO.val();
    var phonenumber = phoneNumberO.val();
    var telephone = telephoneO.val();


    validations = [
        sgrc.valid8r.req(name, nameO),
        sgrc.valid8r.req(surname, surnameO),
        sgrc.valid8r.req(email, emailO),
        sgrc.valid8r.req(telephone, telephoneO)];


    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();

        return false;
    }

    jsonObject = {
        Id: studentId,
        Name: name,
        PhoneNumber: phonenumber,
        Surname: surname,
        Email: email,
        TelephoneNumber: telephone


    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            closeEditPop();
            tblStudents.fnDraw();
            clear();
            sgrc.Growl("Student successfully updated!", "Success", "info", 9000);

        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error update", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error update", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/student/edit/', jsonObject, successF, errorF);


}

function onSaveProgramChanges() {
    
    $('#loader').show();
    var NameO = $('#txtProgramName');
    var DescriptionO = $('#txtProgramDescription');


    var Name = NameO.val();
    var Description = DescriptionO.val();
   // var IsActive = $("input[type='checkbox']").val();


    validations = [
        sgrc.valid8r.req(Name, NameO),
        sgrc.valid8r.req(Description, DescriptionO)];

    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();
        return false;
    }

    jsonObject = {
        Id: programId,
        Name: Name,
        Description: Description
        // IsActive: IsActive

    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            sgrc.Growl("Program successfully updated!", "Success update", "info", 9000);
            clear();
            closePop();
            tblPrograms.fnDraw();
        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error update", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error update", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/program/edit/', jsonObject, successF, errorF);
}

function onSaveActiveStatus(id) {
    $('#loader').show();
    //Ajax Post
    successF = function (result) {

        if (result.Status == "Fail" || result.status === null || result.status === "undefined") {
            sgrc.Growl(result.descripText, "Error!", "danger", 9500); $('#loader').hide();
        }
        else {


            sgrc.Growl("Active status changed successfully!.", "Successful!", "info", 9000);
            tblStudents.fnDraw();
            $('#loader').hide();

        }

    }
    errorF = function (result) {
        tblStudents.fnDraw();
        $('#loader').hide();
    }
    sgrc.ajax.post('/user/api/student/activestatus/' + id, {}, successF, errorF);
}

function onSaveProgramActiveStatus(id) {
    $('#loader').show();
    //Ajax Post
    successF = function (result) {

        if (result.Status == "Fail" || result.status === null || result.status === "undefined") {
            sgrc.Growl(result.descripText, "Error!", "danger", 9500); $('#loader').hide();
        }
        else {


            sgrc.Growl("Active status changed successfully!.", "Successful!", "info", 9000);
            tblPrograms.fnDraw();
            $('#loader').hide();

        }

    }
    errorF = function (result) {
        tblPrograms.fnDraw();
        $('#loader').hide();
    }
    sgrc.ajax.post('/user/api/program/activestatus/' + id, {}, successF, errorF);
}


function onEditClient(id) {



    $('#loader').show();
    var organizationNameO = $('#txtName');
    var organizationPhoneO = $('#txtPhone');
    var organizationEmailO = $('#txtEmail');

    var contactNameO = $('#txtContactName');
    var contactSurnameO = $('#txtContactSurname');
    var contactPhoneO = $('#txtContactPhone');
    var contactEmailO = $('#txtContactEmail');


    var isActive = $('.make-switch').is(":checked");


    var organizationName = organizationNameO.val();
    var organizationPhone = organizationPhoneO.val();
    var organizationEmail = organizationEmailO.val();

    var contactName = contactNameO.val();
    var contactSurname = contactSurnameO.val();
    var contactPhone = contactPhoneO.val();
    var contactEmail = contactEmailO.val();







    validations = [
        sgrc.valid8r.req(organizationName, organizationNameO),
        sgrc.valid8r.email(contactEmail, contactEmailO),
        sgrc.valid8r.req(contactPhone, contactPhoneO),
        sgrc.valid8r.req(contactSurname, contactSurnameO),
        sgrc.valid8r.req(contactName, contactNameO)];


    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();
        sgrc.Growl("Required field(s)", "Client Info", "danger", 9000);
        return false;
    }


    jsonObject = {
        Id: id,
        IsActive: isActive,
        Name: organizationName,
        PhoneNumber: organizationPhone,
        Email: organizationEmail,

        ContactName: contactName,
        ContactSurname: contactSurname,
        ContactPhone: contactPhone,
        ContactEmail: contactEmail

    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();

            sgrc.Growl("Client successfully updated!", "Success", "info", 9000);
            closeEditPop();
        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error update", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error update", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/client/edit/', jsonObject, successF, errorF);

}

function reaload() {
    location.reload();
}

function clear() {
    $("input").val("");
}

function closeEditPop() {
    $("#Modal").hide();
    $(".modal-backdrop").hide();
}

function closePop() {
    $("#ModalProgram").hide();
    $(".modal-backdrop").hide();
}