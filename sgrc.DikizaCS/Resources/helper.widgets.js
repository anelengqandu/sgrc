var sgrc = sgrc || {};
sgrc.widgets = sgrc.widgets || {};
sgrc.widgets.date = {};
sgrc.widgets.spin = {};
sgrc.widgets.spin.perc = {};
sgrc.widgets.money = {};
sgrc.widgets.selectbox = {};

sgrc.ajax = {};
sgrc.event = {};
sgrc.button = {};
sgrc.valid8r = {};
sgrc.widgets.loading = {};


//Boostrap growl notifactions
sgrc.Growl = function (body, header, growlType, timedelay) {
    // growlType = success, info, warning, danger
    var icon = null;
    if (typeof growlType === "undefined") {
        growlType = "info";
    }
    if (typeof timedelay === "undefined") {
        timedelay = "5000";
    }
    if (typeof header === "undefined") {
        header = "popup Message";
    }
    if (typeof body === "undefined") {
        body = "";
    }

    if (growlType == "success") {
        icon = "fa fa-check-circle";
    }
    if (growlType == "info") {
        icon = "fa fa-info-circle";
    }
    if (growlType == "warning") {
        icon = "fa fa-exclamation-circle";
    }
    if (growlType == "danger") {
        icon = "fa fa-times-circle";
    }

    $.bootstrapGrowl('<h4 ><i class="' + icon + '"></i>&nbsp;' + header + '</h4> <p>' + body + '</p>', {
        ele: 'body', // which element to append to
        type: growlType, // (null, 'info', 'danger', 'success', 'warning')
        offset: {
            from: 'top',
            amount: parseInt(200)
        }, // 'top', or 'bottom'
        align: 'right', // ('left', 'right', or 'center')
        width: parseInt(250), // (integer, or 'auto')
        delay: timedelay, // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
        allow_dismiss: true, // If true then will display a cross to close the popup.
        stackup_spacing: 10 // spacing between consecutively stacked growls.
    });
};



/* DATAPICKER */
sgrc.widgets.date.create = function (selector, within, options) {
    var defaults = { changeMonth: true, changeYear: true, dateFormat: 'dd M yy', onSelect: function (dateText, inst) { }, onClose: function (dateText, inst) { $(this).trigger("change"); } };
    var sel = (within == null) ? $(selector) : $(selector, within);

    sel.datepicker($.extend(defaults, options));

    // bind blur event
    sel.change(function () {
        var thisO = $(this);
        var dtVal = Date.parse(thisO.val());
        if (isNaN(dtVal)) {
            thisO.val('');
        }
        else {
            thisO.datepicker("setDate", new Date(dtVal));
        }
    });

    return sel;
};
sgrc.widgets.date.getdate = function (SelOrObj) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (isNaN(Date.parse(SelOrObj.val()))) {
        return null
    }
    return SelOrObj.datepicker('getDate');
}
sgrc.widgets.date.getdatelocal = function (SelOrObj) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (isNaN(Date.parse(SelOrObj.val()))) {
        return null
    }
    return sgrc.widgets.date.localdatestring(SelOrObj.datepicker('getDate'));
}
sgrc.widgets.date.localdatestring = function (d) {
    function pad(n) { return n < 10 ? '0' + n : n }
    return d.getFullYear() + '-'
      + pad(d.getMonth() + 1) + '-'
      + pad(d.getDate()) + 'T'
      + pad(d.getHours()) + ':'
      + pad(d.getMinutes()) + ':'
      + pad(d.getSeconds()); //  + 'Z';
}
//format date for display purposes - in this case 11/23/2012 - used in Tenant.aspx
sgrc.widgets.date.simplelocaldatestring = function (d) {
    function pad(n) { return n < 10 ? '0' + n : n }
    return d.getMonth() + 1 + '/'
      + pad(d.getDate()) + '/'
      + pad(d.getFullYear());
}


/* SPINNER */
sgrc.widgets.spin.create = function (selector, within, options) {
    var sel = (within == null) ? $(selector) : $(selector, within);
    sel.each(function () {
        var meta = eval("(" + $(this).attr('meta') + ')');
        $(this).spinner($.extend(meta, options));
    });
    return sel;
}
sgrc.widgets.spin.perc.create = function (selector, within, options) {
    var defaults = { suffix: '%', min: 0, max: 100, places: 2 };
    var sel = (within == null) ? $(selector) : $(selector, within);
    return sel.spinner($.extend(defaults, options));
}
sgrc.widgets.spin.val = function (SelOrObj, val) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (typeof val === "undefined") {
        return SelOrObj.spinner("value");
    }
    else {
        SelOrObj.spinner("value", val);
    }
}


/* MONEY */
sgrc.widgets.money.create = function (selector, within, options, useMetaOptions) {
    var defaults = { prefix: 'R', group: ',', step: 0.01, largeStep: 10.00, min: 0, max: null };
    var sel = (within == null) ? $(selector) : $(selector, within);

    if (useMetaOptions != 'undefined' && useMetaOptions != null && useMetaOptions == true) {
        sel.each(function () {
            var meta = eval("(" + $(this).attr('meta') + ')');
            $(this).spinmoney($.extend($.extend(defaults, meta), options));
        });
    }
    else {
        sel.spinmoney($.extend(defaults, options));
    }
    return sel;
}
sgrc.widgets.money.val = function (SelOrObj, val) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (typeof val === "undefined") {
        return SelOrObj.spinmoney("value");
    }
    else {
        SelOrObj.spinmoney("value", val);
    }
}
sgrc.widgets.money.disable = function (SelOrObj) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }
    SelOrObj.spinmoney("disable");
}
sgrc.widgets.money.enable = function (SelOrObj) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }
    SelOrObj.spinmoney("enable");
}

/* SELECTBOX */
sgrc.widgets.selectbox.supported = function () {
    if ($.browser.msie && ($.browser.version === "6.0" || $.browser.version === "7.0")) {
        return false;
    }
    return true;
}
sgrc.widgets.selectbox.create = function (selector, within, options) {
    var sel = (within == null) ? $(selector) : $(selector, within);
    //if (!sgrc.widgets.selectbox.supported()) {
    //    return sel;
    //}

    // sets empty option to correct format
    var first = $("option:first", sel);
    if (typeof first !== "undefined") {
        if (first.val() === "") {
            first.removeAttr("value");
        }
    }

    return sel.select2(options);
}
sgrc.widgets.selectbox.enable = function (SelOrObj, isenabled) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (!sgrc.widgets.selectbox.supported()) {
        isenabled == true ? SelOrObj.removeAttr('disabled') : SelOrObj.attr('disabled', 'disabled');
    }
    else {
        isenabled == true ? SelOrObj.select2("enable") : SelOrObj.select2("disable");
    }
}
sgrc.widgets.selectbox.val = function (SelOrObj, val) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (typeof val === "undefined") {
        //if (!sgrc.widgets.selectbox.supported()) {
        //    return SelOrObj.val();
        //}
        //else {
        return SelOrObj.select2("val");
        //}
    }
    else {
        //if (!sgrc.widgets.selectbox.supported()) {
        //    SelOrObj.val(val);
        //}
        //else {
        SelOrObj.select2("val", val);
        //}
    }
}
sgrc.widgets.selectbox.validatecont = function (SelOrObj) {
    if (typeof SelOrObj === "string") {
        SelOrObj = $(SelOrObj);
    }

    if (!sgrc.widgets.selectbox.supported()) {
        return SelOrObj;
    }
    else {
        return SelOrObj.select2("container");
    }
}




/* AJAX */
sgrc.ajax.csrfToken = null;
sgrc.ajax.get = function (url, successF, errorF) {
    if (sgrc.ajax.csrfToken == null) {
        var csrfToken = $("input[name='__RequestVerificationToken']").val();
    }
    var h_opt = csrfToken ? { "X-XSRF-Token": csrfToken } : {};

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        contentType: "application/json",
        headers: h_opt,
        success: function (result) {
            if (typeof successF !== "undefined") {
                successF(result);
            }
        },
        error: function (result) {
            if (errorF == null || typeof errorF === "undefined") {
                alert('Error - ' + result.statusText);
            }
            else {
                errorF(result);
            }
        }
    });
}
sgrc.ajax.post = function (url, jsonO, successF, errorF) {

    if (sgrc.ajax.csrfToken == null) {
        var csrfToken = $("input[name='__RequestVerificationToken']").val();
    }
    var h_opt = csrfToken ? { "X-XSRF-Token": csrfToken } : {};

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(jsonO),//$.toJSON(jsonO),
        processData: false,
        dataType: "json",
        contentType: "application/json",
        headers: h_opt,
        success: function (result) {
            if (typeof successF !== "undefined") {
                successF(result);
            }
        },
        error: function (result) {
            debugger;
            if (errorF == null || typeof errorF === "undefined") {
                alert('Error - ' + result.statusText);
            }
            else {
                errorF(result);
            }
        }
    });
}

/* EVENTS */
sgrc.event.stopPropagate = function (e) {
    if (!e) { e = window.event };
    try {
        e.stopPropagation();
    }
    catch (err) {
        try {
            event.cancelBubble = true;
        }
        catch (err2) {
            e.preventDefault();
        }
    }
}

/* BUTTONS */
sgrc.button.backcapture = function () {
    window.onbeforeunload = function () {
        return "Are you sure you wish to leave this page?";
    }
}
sgrc.button.backclear = function () {
    window.onbeforeunload = null;
}


/* VALID8R */
sgrc.valid8r.seterror = function (cont, msg) {
    cont.addClass("valid8r_Error");
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        return;
    }

    var getID = cont.attr('id');
    var error = null;
    if (msg == null)
        msg = "Required!";

    $("#" + getID).removeClass('form-control input-lg status');
    $("#" + getID).addClass('form-control input-lg status borders');
    $("#" + getID).closest('.form-group').addClass('has-error');
    var html = '<div  class="errorIcon help-block help-block-error" style="color:#e73d4a;">' + msg + '</div>';
    cont.after(html);
}

sgrc.valid8r.seterrorEmail = function (cont, msg) {
    cont.addClass("valid8r_Error");
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        return;
    }


    // var html = '<img class="errorIcon" title="' + msg + '" onmouseout="sgrc.valid8r.ttout(this)" onmouseover="sgrc.valid8r.ttIn(this)" alt="?" src="/Resources/img/icon76.png"/>';
    var getID = cont.attr('id');

    // var html = '<img class="errorIcon" title="' + msg + '" onmouseout="sgrc.valid8r.ttout(this)" onmouseover="sgrc.valid8r.ttIn(this)" alt="?" src="/Resources/img/icon76.png"/>';
    $("#" + getID).removeClass('form-control input-lg status');
    $("#" + getID).addClass('form-control input-lg status borders');
    var html = '<div for="login-email" class="errorIcon help-block animation-slideDown" style="color:#e73d4a;">' + msg + '</div>';
    cont.after(html);
}
sgrc.valid8r.seterrorPassword = function (cont, msg) {
    cont.addClass("valid8r_Error");
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        return;
    }

    var getID = cont.attr('id');

    // var html = '<img class="errorIcon" title="' + msg + '" onmouseout="sgrc.valid8r.ttout(this)" onmouseover="sgrc.valid8r.ttIn(this)" alt="?" src="/Resources/img/icon76.png"/>';
    $("#" + getID).removeClass('form-control input-lg status');
    $("#" + getID).addClass('form-control input-lg status borders');
    var html = '<div  class="errorIcon help-block animation-slideDown" style="color:#bd1414;">' + msg + '</div>';
    cont.after(html);
}

sgrc.valid8r.seterrorPhoneNumber = function (cont, msg) {
    cont.addClass("valid8r_Error");
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        return;
    }

    var getID = cont.attr('id');

    // var html = '<img class="errorIcon" title="' + msg + '" onmouseout="sgrc.valid8r.ttout(this)" onmouseover="sgrc.valid8r.ttIn(this)" alt="?" src="/Resources/img/icon76.png"/>';
    $("#" + getID).removeClass('form-control input-lg status');
    $("#" + getID).addClass('form-control input-lg status borders');
    var html = '<div  class="errorIcon help-block animation-slideDown" style="color:#bd1414;">' + msg + '</div>';
    cont.after(html);
}

sgrc.valid8r.clearerror = function (cont) {
    cont.removeClass("valid8r_Error");
    var getID = cont.attr('id');


    $("#" + getID).removeClass('borders');
    $("#" + getID).closest('.form-group').removeClass('has-error');
    var n = cont.next();
    if (n.hasClass('errorIcon') || n.hasClass('help-block help-block-error')) {

        n.remove();
    }
}

sgrc.valid8r.clearerrorEmail = function (cont) {
    cont.removeClass("valid8r_Error");
    var getID = cont.attr('id');


    $("#" + getID).removeClass('borders');
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        n.remove();
    }
}

sgrc.valid8r.clearerrorPassword = function (cont) {
    cont.removeClass("valid8r_Error");
    var getID = cont.attr('id');


    $("#" + getID).removeClass('borders');
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        n.remove();
    }
}

sgrc.valid8r.clearerrorPhoneNumber = function (cont) {
    cont.removeClass("valid8r_Error");
    var getID = cont.attr('id');


    $("#" + getID).removeClass('borders');
    var n = cont.next();
    if (n.hasClass('errorIcon')) {
        n.remove();
    }
}

sgrc.valid8r.ttIn = function (obj) {
    //var that = $(obj);
    //var pos = that.position();
    //obj.t = obj.title;
    //obj.title = "";
    //that.after("<p id='valTTip'>" + obj.t + "</p>");
    //$("#valTTip").css("top", (pos.top - 10) + "px").css("left", (pos.left + 20) + "px").fadeIn("fast");
}
sgrc.valid8r.ttout = function (obj) {
    //var that = $(obj);
    //obj.title = obj.t;
    //that.next('#valTTip').remove();
}

sgrc.valid8r.range = function (validationsArray) {
    var isValid = true;
    for (var i = 0; i < validationsArray.length; i++) {
        var item = validationsArray[i];
        if (item != true) {
            isValid = false;
        }
    }
    return isValid;
}

sgrc.valid8r.req = function (val, cont, error) {
    //if it's NOT valid  
    if (val == null || val.length < 1) {
        sgrc.valid8r.seterror(cont, error || 'Required!');
        return false;
    }
        //if it's valid  
    else {
        sgrc.valid8r.clearerror(cont);
        return true;
    }
}
sgrc.valid8r.reqamt = function (val, cont, error) {
    /* function to check if amount is not <= R0.00, just included [val <= 0] - Anele */
    //if it's NOT valid  
    if (val == null || val <= 0 || val.length < 1) {
        sgrc.valid8r.seterror(cont, error || 'Required!');
        return false;
    }
        //if it's valid  
    else {
        sgrc.valid8r.clearerror(cont);
        return true;
    }
}
sgrc.valid8r.reqamtneg = function (val, cont, error) {
    /* function to check if amount is not = R0.00, but can be negative */
    //if it's NOT valid  
    if (val == null || val == 0 || val.length < 1) {
        sgrc.valid8r.seterror(cont, error || 'Required!');
        return false;
    }
        //if it's valid  
    else {
        sgrc.valid8r.clearerror(cont);
        return true;
    }
}
sgrc.valid8r.email = function (val, cont, error) {

    //if it's Empty
    if (val == null || val.length < 1) {
        sgrc.valid8r.seterrorEmail(cont, error || 'Invalid Email!');
        return false;
    }
    else {
        //if it's NOT valid 

        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (re.test(val) == false) {
            sgrc.valid8r.seterrorEmail(cont, 'Invalid Email!');
            return false;
        }
            //if it's valid  
        else {
            sgrc.valid8r.clearerrorEmail(cont);
            return true;
        }
    }
}

//Created Craig - 06/02/2013 - used to check val is of required length (passed through with optional error message)
sgrc.valid8r.reqLength = function (val, cont, requiredLength, errorMsg) {

    val = val.replace(/\s+/g, "");

    if (requiredLength == null || requiredLength == undefined || (requiredLength.isNaN == false)) {
        sgrc.valid8r.seterrorPhoneNumber(cont, 'Invalid Phone Number');
        return false;
    }
    //if it's NOT valid  
    if (val.length < requiredLength || val.length > requiredLength) {
        if (errorMsg != null && errorMsg != undefined && errorMsg.length > 11) {
            sgrc.valid8r.seterrorPhoneNumber(cont, errorMsg);
            return false;
        }
        else {
            sgrc.valid8r.seterrorPhoneNumber(cont, 'Invalid Phone Number e.g(0757899624 Or 0114692376)');
            return false;
        }
    }
        //if it's valid  
    else {
        sgrc.valid8r.clearerrorPhoneNumber(cont);
        return true;
    }
}


//Created Craig - 02/04/2013
sgrc.valid8r.passwords = function (newPassVal, newPassCont, confPassVal, confPassCont) {
    var isValid = false;

    //clear errors
    sgrc.valid8r.clearerrorPassword(newPassCont);
    sgrc.valid8r.clearerrorPassword(confPassCont);

    //if it's NOT valid  
    if (newPassVal == null || newPassVal.length < 1 || newPassVal == '' || confPassVal == null || confPassVal.length < 1 || confPassVal == '') {
        sgrc.valid8r.seterrorPassword(newPassCont, 'Required!');
        sgrc.valid8r.seterrorPassword(confPassCont, 'Required!');
        isValid = false;
    }
    else {
        //4 chars while testing
        if (newPassVal.length < 4 || confPassVal.length < 4) {
            sgrc.valid8r.seterrorPassword(newPassCont, 'Minimum of 4 characters required!');
            sgrc.valid8r.seterrorPassword(confPassCont, 'Minimum of 4 characters required!');
            isValid = false;
        } else {
            isValid = true;
        }
    }

    if (isValid === true) {
        //if it's valid  
        if (newPassVal === confPassVal) {
            sgrc.valid8r.clearerrorPassword(newPassCont);
            sgrc.valid8r.clearerrorPassword(confPassCont);
        } else {
            sgrc.valid8r.seterrorPassword(newPassCont, 'Passwords do not match!');
            sgrc.valid8r.seterrorPassword(confPassCont, 'Passwords do not match!');
            isValid = false;
        }
    }

    return isValid;
}

//Created Craig - 25/01/2013 
sgrc.valid8r.clearErrorRange = function (errorArray) {
    for (var i = 0; i < errorArray.length; i++) {
        var cont = errorArray[i];
        cont.removeClass("valid8r_Error");
        var n = cont.next();
        if (n.hasClass('errorIcon')) {
            n.remove();
        }
    }
}

//Created Craig - 25/01/2013
sgrc.valid8r.clearError_ResetTextCont_Range = function (errorArray) {
    for (var i = 0; i < errorArray.length; i++) {
        var cont = errorArray[i];
        cont.val('');
        cont.removeClass("valid8r_Error");
        var n = cont.next();
        if (n.hasClass('errorIcon')) {
            n.remove();
        }
    }
}


//verification if value exist
sgrc.valid8r.verify = function (url, val, cont, error) {
    //var value = this.checkExistance(url, val, error);


    $.get(url + val, function (result) {
        if (typeof result !== "undefined") {
            if (result.status == "Fail") {
                sgrc.valid8r.seterror(cont, error);
                return false;
            }
                //if it's valid  
            else {
                sgrc.valid8r.clearerror(cont);
                return true;
            }
        }
    });
    debugger;
    //if (sgrc.ajax.csrfToken == null) {
    //    var csrfToken = $("input[name='__RequestVerificationToken']").val();
    //}
    //var h_opt = csrfToken ? { "X-XSRF-Token": csrfToken } : {};

    //$.ajax({
    //    type: 'GET',
    //    url: url + val,
    //    dataType: 'json',
    //    contentType: "application/json",
    //    headers: h_opt,
    //    success: function (result) {
    //        if (typeof result !== "undefined") {
    //            if (result.status == "Fail") {
    //                sgrc.valid8r.seterror(cont, error);
    //                return false;
    //            }
    //                //if it's valid  
    //            else {
    //                sgrc.valid8r.clearerror(cont);
    //                return true;
    //            }
    //        }
    //    },
    //    error: function (result) {
    //        if (errorF == null || typeof errorF === "undefined") {
    //            alert('Error - ' + result.statusText);
    //        }
    //        else {
    //            errorF(result);
    //        }
    //    }
    //});









}


function checkExistance(url, val, error) {
    return $.get(url + val, function (result) {
        if (typeof result !== "undefined") {
            if (result.status == "Fail") {
                sgrc.valid8r.seterror(cont, error);
                return false;
            }
                //if it's valid  
            else {
                sgrc.valid8r.clearerror(cont);
                return true;
            }
        }
    });
}






/* BACKWARD SUPPORT */
// Date Picker
function createDatePicker(selector, options) { return sgrc.widgets.date.create(selector, null, options); }
function createDatePickerWithin(selector, within, options) { return sgrc.widgets.date.create(selector, within, options); }
function getDatePickerDate(selector) { return sgrc.widgets.date.getdate(selector); }
function getDatePickerDateO(object) { return sgrc.widgets.date.getdate(object); }
function localDateString(d) { return sgrc.widgets.date.localdatestring(d); }

// Spin
function createSpinQty(selector, options) { return sgrc.widgets.spin.create(selector, null, options); }
function createSpinQtyWithin(selector, within, options) { return sgrc.widgets.spin.create(selector, within, options); }
function createSpinPerc(selector, options) { return sgrc.widgets.spin.perc.create(selector, null, options); }
function createSpinPercWithin(selector, within, options) { return sgrc.widgets.spin.perc.create(selector, within, options); }


// Ajax
function ajaxGet(url, successF) { sgrc.ajax.get(url, successF); }
function ajaxPost(url, jsonO, successF, errorF) { sgrc.ajax.post(url, jsonO, false, successF, errorF); }
function ajaxPostWithLoading(url, jsonO, successF, errorF) { sgrc.ajax.post(url, jsonO, true, successF, errorF); }
function ajaxPostWithLoadingAndUrl(url, jsonO, successF) { sgrc.ajax.post(url, jsonO, true, successF); }
function ajaxPostwithLoadingAndData(url, jsonO, successF) { sgrc.ajax.post(url, jsonO, true, successF); }


// Events
function stopEventPropagate(e) { sgrc.event.stopPropagate(e); }

// Buttons
function backButtonCapture() { sgrc.button.backcapture(); }
function backButtonClear() { sgrc.button.backclear(); }

// Valid8r
function valid8r_SetError(cont, msg) { sgrc.valid8r.seseterrort(cont, msg); }
function valid8r_ClearError(cont) { sgrc.valid8r.clearerror(cont); }
function valid8r_Range(validationsArray) { return sgrc.valid8r.range(validationsArray); }
function valid8r_Req(val, cont) { return sgrc.valid8r.req(val, cont); }
function valid8r_ReqAmount(val, cont) { return sgrc.valid8r.reqamt(val, cont); }
function valid8r_Email(val, cont) { return sgrc.valid8r.email(val, cont); }
function valid8r_Verify(val, cont) { return sgrc.valid8r.verify(url, val, cont, error); }
