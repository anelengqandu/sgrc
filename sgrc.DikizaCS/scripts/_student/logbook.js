

var mainJson = null;
var successF = null;
var errorF = null;
var validations = null;

$(document).ready(function () {
  
    $('.date').datepicker({
        format: 'dd MM yyyy',
        autoclose: true
    });

});

function onEditModal(id) {


    

    $.get('/logbook/api/edittime/' + id,
     function (data) {
         $("#txtEditDateOfActivity").val(data.data.DateOfActivityS);
         $("#cbEditActivityType").val(data.data.ActivityType);
         $("#txtEditDescription").val(data.data.ActivityDescription);
         $("#workedHEdit").val(data.data.TimeHours);
         $("#workedMEdit").val(data.data.TimeMinutes);
        
     });

}

function onSaveEditTime() {
    $('#loader').show();
    var id = $("#hf_logbookId").val();

    var dateOfActivityO = $("#txtEditDateOfActivity");
    var activityTypeO = $("#cbEditActivityType");
    var descriptionO = $("#txtEditDescription");
    var workedHO = $("#workedHEdit");
    var workedMO = $("#workedMEdit");



    var dateOfActivity = dateOfActivityO.val();
    var activityType = activityTypeO.val();
    var description = descriptionO.val();
    var workedH = workedHO.val();
    var workedM = workedMO.val();


    validations = [
                  sgrc.valid8r.req(dateOfActivity, dateOfActivityO),
                  sgrc.valid8r.req(activityType, activityTypeO),
                  sgrc.valid8r.req(description, descriptionO),
                  sgrc.valid8r.req(workedH, workedHO),
                  sgrc.valid8r.req(workedM, workedMO),
                  sgrc.valid8r.req(activityType, activityTypeO)];

    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();
        return false;
    }

    jsonObject = {

        Id:id,
        DateOfActivity: dateOfActivity,
        ActivityType: activityType,
        ActivityDescription: description,
        TimeHours: workedH,
        TimeMinutes: workedM

    };

    successF = function (result) {
        if (result.Status == "Fail" || result.Status === null || result.Status === "undefined") {
            sgrc.Growl(result.descripText, "Error on insert", "danger", 9500); $('#loader').hide();
            location.Reload();
        }
        else {
            if (result.Status == "Success") {
                $('#loader').hide();

                location.reload();
                sgrc.Growl("Successful add.", "Signup successful!", "info", 9000);
            }
        }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.descripText + ". " + "Please contact administrator", "Error on signup", "warning", 9500);
    }
    sgrc.ajax.post('/logbook/api/edit/', jsonObject, successF, errorF);
}


function onSaveTime() {
    $('#loader').show();


   
   var dateOfActivityO = $("#txtDateOfActivity");
   var activityTypeO = $("#cbActivityType");
   var descriptionO = $("#txtDescription");
   var workedHO = $("#workedH");
   var workedMO = $("#workedM");


 
  var dateOfActivity = dateOfActivityO.val();
  var activityType = activityTypeO.val();
  var description = descriptionO.val();
  var workedH = workedHO.val();
  var workedM = workedMO.val();


    validations = [
                  sgrc.valid8r.req(dateOfActivity, dateOfActivityO),
                  sgrc.valid8r.req(activityType, activityTypeO),
                  sgrc.valid8r.req(description, descriptionO),
                  sgrc.valid8r.req(workedH, workedHO),
                  sgrc.valid8r.req(workedM, workedMO),
                  sgrc.valid8r.req(activityType, activityTypeO)];

    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();
        return false;
    }

    jsonObject = {
      
       
        DateOfActivity: dateOfActivity,
        ActivityType: activityType,
        ActivityDescription: description,
        TimeHours: workedH,
        TimeMinutes: workedM

    };

    successF = function (result) {
        if (result.Status == "Fail" || result.Status === null || result.Status === "undefined") {
            sgrc.Growl(result.descripText, "Error on insert", "danger", 9500); $('#loader').hide();
            location.Reload();
        }
        else {
            if (result.Status == "Success") {
                $('#loader').hide();

                location.reload();
                sgrc.Growl("Successful add.", "Signup successful!", "info", 9000);
            }
        }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.descripText + ". " + "Please contact administrator", "Error on signup", "warning", 9500);
    }
    sgrc.ajax.post('/logbook/api/create/', jsonObject, successF, errorF);
}


function onSaveActiveStatus(id) {
    $('#loader').show();
    
    successF = function (result) {

        if (result.Status == "Fail" || result.Status === null || result.Status === "undefined") {
            sgrc.Growl(result.descripText, "Error", "danger", 9500); $('#loader').hide();
        }
        else {


            sgrc.Growl("Time submitted successfully!.", "Successful!", "info", 9000);
            location.reload();
            $('#loader').hide();

        }

    }
    errorF = function (result) {
        tblStudents.fnDraw();
        $('#loader').hide();
    }
    sgrc.ajax.post('/logbook/api/activestatus/' + id, {}, successF, errorF);
}


function onSaveDeleteStatus(id) {
    $('#loader').show();

    successF = function (result) {
        if (result.Status == "Fail" || result.Status === null || result.Status === "undefined") {
            sgrc.Growl(result.descripText, "Error", "danger", 9500); $('#loader').hide();
        }
        else {



            sgrc.Growl("Delete status changed successfully!.", "Successful!", "info", 9000);
            location.reload();
            $('#loader').hide();
        }

    }
    errorF = function (result) {
        alert(result.descripText);
    }
    sgrc.ajax.post('/logbook/api/deletestatus/' + id, {}, successF, errorF);


    // return false;
}


function clear() {
    $("input").val("");
}

function closeEditPop() {
    $("#Modal").hide();
    $(".modal-backdrop").hide();
}