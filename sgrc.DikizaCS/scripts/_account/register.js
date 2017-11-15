var successF = null;
var errorF = null;
var validations = null;

jQuery(document).ready(function () {
    Register.init();
});
var Register = function () {
   
    var handleLogin = function () {

        $('.register-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                email: {
                    required: true,
                    email: true
                },
                name: {
                    required: true,
                    
                },
                surname: {
                    required: true,
                   
                },
                password: {
                    required: true
                },
                rpassword: {
                    equalTo: "#password"
                }
            },

            messages: {
                name: {
                    required: "Name is required."
                },
                surname: {
                    required: "Surname is required."
                },
                email: {
                    required: "Email is required."
                },
                password: {
                    required: "Password is required."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                // $('.alert-danger', $('.register-form')).show();
                $('#loader').hide();
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
                $('#loader').hide();
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
                $('#loader').hide();
            },

            errorPlacement: function (error, element) {
                  error.insertAfter(element.closest('.input-icon'));
                error.insertAfter(element);
            },

            submitHandler: function (form) {
                //form.submit(); // form validation success, call ajax form submit
                register();

            }
        });

        $('.register-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.register-form').validate().form()) {
                    $('.register-form').submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });

       

       
    }


    return {
        //main function to initiate the module
        init: function () {

            handleLogin();

            // init background slide images
            $('.login-bg').backstretch([
                "../Resources/theme/assets/pages/img/login/bg1.jpg"
                //"../Resources/theme/assets/pages/img/login/bg2.jpg",
                //"../Resources/theme/assets/pages/img/login/bg3.jpg"
            ], {
                fade: 1000,
                duration: 8000
            }
            );

         

        }

    };

}();

function register() {
    $('#loader').show();
    var nameO = $('#name');
    var surnameO = $('#surname');
    var emailO = $('#email');
    var passwordO = $('#password');

    var name = nameO.val();
    var surname = surnameO.val();
    var email = emailO.val();
    var password = passwordO.val();


    var jsonObject = {
        Name: name,
        Surname: surname,
        Email: email,
        Password: password
    };
    successF = function (result) {
        if (result.Status == "Fail" || result.Status === null || result.Status === "undefined") {
            sgrc.Growl(result.DescripText, "Error on signup", "danger", 9500); $('#loader').hide();
        }
        else {
            if (result.Status == "Success") {
                $('#loader').hide();
                clear();
                sgrc.Growl("Registration successful,Please signin.", "Signup successful!", "info", 9000);
            }


        }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.DescripText + ". " + "Please contact administrator", "Error on signup", "warning", 9500);
    }
    sgrc.ajax.post('/api/user/createorupdate/', jsonObject, successF, errorF);

}
function clear() {
    $("input").val("");
}