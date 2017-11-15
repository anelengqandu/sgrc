var tblPrograms = null;
var mainJson = null;
var successF = null;
var errorF = null;
var validations = null;
var delayTimer;

$(document).ready(function () {
   
    mainJson = jQuery.parseJSON($('#jsonData').html());
   
    getPrograms();


});


function getPrograms() {
    tblPrograms = $('#tblPrograms').dataTable({
        "oLanguage": { "sEmptyTable": "No data to display" },
        "bJQueryUI": false,
        "bAutoWidth": false,
        "sPaginationType": "full_numbers",
        "sAjaxSource": '/user/api/program/dtPrograms',
        "aaData": mainJson.aaData,
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
        "iDeferLoading": [mainJson.count, mainJson.count],
        "aoColumnDefs": [{ "bVisible": false, "aTargets": [] }],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            var btnHtml = '';
            $('td:eq(3)', nRow).html('<img  ' + (aData["IsActive"] ? ' src="/Resources/images/icons/tick132.png"' : 'src="/Resources/images/icons/cross32.png"')
             + '" style="cursor:pointer;"  onclick="this.src = onSaveActiveStatus(' + aData["Id"] + ')" ><i></i>');

          
            btnHtml += ' <a href="#"  onclick="onEdit(' + aData["Id"] + ')" class="btn btn-md btn-default"  data-toggle="tooltip" title="" data-original-title="Status"><i class="fa fa-eye"></i>&nbsp;View</a>';

            $('td:eq(4)', nRow).html(btnHtml);
        }

    });

    $('.dataTables_filter input').addClass('form-control').attr('placeholder', 'Search');
    $('.dataTables_length select').addClass('form-control');
}



//Fnction to filter data
function filterApplied() {
    clearTimeout(delayTimer);
    delayTimer = setTimeout(function () {
        // Do the ajax stuff
        tblPrograms.fnDraw();
    }, 500); // Will do the ajax stuff after 1000 ms, or 1 s
}




function onSaveActiveStatus(Id) {
    $('#loader').show();
    //Ajax Post
    successF = function (result) {

        if (result.Status === "Fail" || result.Status === null || result.Status === "undefined") {
            sgrc.Growl(result.DescripText, "Error!", "danger", 9500); $('#loader').hide();
        }
        else {

            $('#loader').hide();

            sgrc.Growl("Active status changed successfully!.", "Successful!", "info", 9000);
            tblPrograms.fnDraw();
        }

    }
    errorF = function (result) {
        tblPrograms.fnDraw();
    }
    sgrc.ajax.post('/user/api/program/activestatus/' + Id, {}, successF, errorF);
}



//redirect to View
function onEdit(ProgramId) {

    window.location.href = '/User/Program/Edit/?ProgramId=' + ProgramId;

}

