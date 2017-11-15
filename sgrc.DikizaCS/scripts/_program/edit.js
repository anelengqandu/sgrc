
var mainJson = null;
var successF = null;
var errorF = null;
var validations = null;



$(document).ready(function () {

});

function onSaveChanges(id) {
    $('#loader').show();
    var NameO = $('#txtName');
    var DescriptionO = $('#txtDescription');


    var Name = NameO.val();
    var Description = DescriptionO.val();
    var IsActive = $("input[type='checkbox']").val();


    validations = [
                   sgrc.valid8r.req(Name, NameO),
                    sgrc.valid8r.req(Description, DescriptionO)];

    if (!sgrc.valid8r.range(validations)) {
        $('#loader').hide();
        return false;
    }

    jsonObject = {
        Id:id,
        Name: Name,
        Description: Description
       // IsActive: IsActive

    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            sgrc.Growl("Program successfully updated!", "Success update", "info", 9000);
            
        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error update", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error update", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/program/edit/', jsonObject, successF, errorF);

}

function reaload() {
    location.reload();
}

function clear() {
    $("input").val("");
}