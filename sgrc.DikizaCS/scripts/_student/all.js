
var mainJson = null;
var successF = null;
var errorF = null;
var tblStudents = null;
var validations = null;
var studentId = null;




$(document).ready(function () {
    mainJson = jQuery.parseJSON($('#jsonData').html());
    //getStudents();
  
});


//function getStudents() {
//    var id = $('#hf_clientId').val();
//    tblStudents = $('#tblStudents').dataTable({
//        "oLanguage": { "sEmptyTable": "No data to display" },
//        "bJQueryUI": false,
//        "bAutoWidth": false,
//        "sPaginationType": "full_numbers",
//        "sAjaxSource": '/user/api/clients/dtclientstudents/' + id,
//        "aaData": mainJson.aaData,
//        "iDisplayLength": 10,
//        "bProcessing": true,
//        "bServerSide": true,
//        "bSort": false,
//        "bRetrieve": true,
//        "aoColumns": [
//                      { "sTitle": "FullName", "mDataProp": "Fullname" },
//                     { "sTitle": "Email", "mDataProp": "Email" },
//                      { "sTitle": "Primary Phone", "mDataProp": "PhoneNumber" },
//                      { "sTitle": "Active Status", "mDataProp": "IsActive", "sClass": "text-center w110px" },

//                     { "sTitle": "Actions", "mDataProp": "Id", "sClass": "text-center w110px" }
//        ],
//        "iDeferLoading": [mainJson.count, mainJson.count],
//        "aoColumnDefs": [{ "bVisible": false, "aTargets": [] }],
//        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
//            var btnHtml = '';
//            $('td:eq(3)', nRow).html('<img  ' + (aData["IsActive"] ? ' src="/Resources/images/icons/tick132.png"' : 'src="/Resources/images/icons/cross32.png"')
//             + '" style="cursor:pointer;"  onclick="this.src = onSaveActiveStatus(' + aData["Id"] + ')" ><i></i>');


//            btnHtml += ' <a href="#" data-toggle="modal" data-target="#Modal"  onclick="onView(' + iDisplayIndex + ')" class="btn btn-md btn-default"  data-toggle="tooltip" title="" data-original-title="Status"><i class="fa fa-eye"></i>&nbsp;View</a>';

//            $('td:eq(4)', nRow).html(btnHtml);
//        }

//    });

//    $('.dataTables_filter input').addClass('form-control').attr('placeholder', 'Search');
//    $('.dataTables_length select').addClass('form-control');
//}


function onViewTimes(studentId) {
   
    window.location.href = '/user/logbooks/?studentId=' + studentId;

}