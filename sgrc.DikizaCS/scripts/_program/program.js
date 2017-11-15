
var mainJson = null;
var successF = null;
var errorF = null;
var validations = null;



$(document).ready(function () {
  
});

function onRegisterProgram() {
    $('#loader').show();
    var NameO = $('#txtName');
    var DescriptionO = $('#txtDescription');


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
        Description: Description

    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            sgrc.Growl("Program registration successful!", "Success registration", "info", 9000);
            //location.reload();
            clear();
        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error Login", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error registration", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/program/create/', jsonObject, successF, errorF);

}

function reaload() {
    location.reload();
}

function clear() {
    $("input").val("");
}