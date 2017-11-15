
var mainJson = null;
var successF = null;
var errorF = null;
var validations = null;



$(document).ready(function () {
    $('.bootstrap-switch-default').click(function () {
        $('.make-switch').attr("checked", true);
    });

    $('.bootstrap-switch-primary').click(function () {
        $('.make-switch').attr("checked", false);
    });
});

function onRegisterClient() {
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
        return false;
    }

    jsonObject = {
   Name : organizationName,
   PhoneNumber: organizationPhone,
   Email : organizationEmail,

   ContactName : contactName,
   ContactSurname : contactSurname,
   ContactPhone : contactPhone,
   ContactEmail : contactEmail

    };
    successF = function (result) {
        if (result.Success) {
            $('#loader').hide();
            clear();
            sgrc.Growl("Client successfully created!", "Success", "info", 9000);

        }
        else { $('#loader').hide(); sgrc.Growl(result.DescripText, "Error create", "warning", 9000); }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText, "Error create", "warning", 9000);
    }
    sgrc.ajax.post('/user/api/client/create/', jsonObject, successF, errorF);

}

function reaload() {
    location.reload();
}

function clear() {
    $("input").val("");
}