var successF = null;
var errorF = null;
var validations = null;

jQuery(document).ready(function () {
    Login.init();
});
var Login = function () {

    var handleLogin = function () {
       
        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                username: {
                    required: "Username is required."
                },
                password: {
                    required: "Password is required."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                $('.alert-danger', $('.login-form')).show();
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
                $('#loader').hide();
            },

            submitHandler: function (form) {
                //form.submit(); // form validation success, call ajax form submit
                auth();

            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });

        $('.forget-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.forget-form').validate().form()) {
                    $('.forget-form').submit();
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
                "../Resources/theme/assets/pages/img/login/bg_img.png"
                //"../Resources/theme/assets/pages/img/login/bg2.jpg",
                //"../Resources/theme/assets/pages/img/login/bg3.jpg"
            ], {
                fade: 1000,
                duration: 8000
            }
            );

            $('.forget-form').hide();

        }

    };

}();

function auth() {
    $('#loader').show();
    var usernameO = $('#username');
    var passwordO = $('#password');
    var username = usernameO.val();
    var password = passwordO.val();

   
   var jsonObject = {
        Username: username,
        Password: password
    };
    successF = function (result) {
        if (result.Success) {

            window.location = result.Redirect;
            $('#loader').hide();
        }
        else {
            sgrc.Growl(result.Message, "Error Login", "danger", 9000);
            $('#loader').hide();
        }
    }
    errorF = function (result) {
        $('#loader').hide();
        sgrc.Growl(result.Message, "Error Login", "warning", 9000);
    }
    sgrc.ajax.post('/Account/LoginUser/', jsonObject, successF, errorF);

}
