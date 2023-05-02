
$(document).ready(function () { 
    $('#replacecard').click(function () { 
        
     var oldKitNo = $("#PBlocked_Card").val();
        if (oldKitNo == '' || null) { alert("Please select any one"); return; }

     $.ajax({
         type: 'POST',
         url: '/ManageCard/CardReplace?oldKitNo=' + oldKitNo,
         contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
         success: function (result) {

         }

     });
    });
 
$('#limit1action').click(function () {
    var Flag = $("#limitflag1").val();
    var Limit = $("#limit1").val();

    if (Limit > 10000) {
        DispalyError("Maximum Limit is Rs.10000... Please try Below 10000 Rupees");
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardLimitDoestic?flag=' + Flag + "&Limit=" + Limit,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result.result = 'true') {
                DispalySuccess("Limit Set successfully !!!");
            }
            else {
                DispalyError("Limit Set Failed ");
            }
        }
    });;
});

$('#limit2action').click(function () {

    var Flag = $("#limitflag2").val();
    var Limit = $("#limit2").val();
    if (Limit > 10000) {
        DispalyError("Maximum Limit is Rs.10000... Please try Below 10000 Rupees");
        return false;
    }
    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardLimitDoestic?flag=' + Flag + "&Limit=" + Limit,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result.result = 'true') {
                DispalySuccess("Limit Set successfully !!!");
            }
            else {
                DispalyError("Limit Set Failed ");
            }
        }
    });
});

$('#limit3action').click(function () {

    var Flag = $("#limitflag3").val();
    var Limit = $("#limit3").val();
    if (Limit > 10000) {
        DispalyError("Maximum Limit is Rs.10000... Please try Below 10000 Rupees");
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardLimitDoestic?flag=' + Flag + "&Limit=" + Limit,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result.result = 'true') {
                DispalySuccess("Limit Set successfully !!!");
            }
            else {
                DispalyError("Limit Set Failed ");
            }
        }
    });;
});

$('#limit4action').click(function () {

    var Flag = $("#limitflag4").val();
    var Limit = $("#limit4").val();
    if (Limit > 10000) {
        DispalyError("Maximum Limit is Rs.10000... Please try Below 10000 Rupees");

        return;
    }
    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardLimitDoestic?flag=' + Flag + "&Limit=" + Limit,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result.result = 'true') {
                DispalySuccess("Limit Set successfully !!!");
            }
            else {
                DispalyError("Limit Set Failed ");
            }
        }
    });;
});

$('#onoff1').click(function () {

    var Flag = $("#flag1").val();
    var Status = $("#onoff1").is(":checked");
    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardDoestic?flag=' + Flag + "&status=" + Status,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {

        }
    });;
});

$('#onoff2').click(function () {
    var Flag = $("#flag2").val();
    var Status = $("#onoff2").is(":checked");

    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardDoestic?flag=' + Flag + "&status=" + Status,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {

        }
    });
});

$('#onoff3').click(function () {
    var Flag = $("#flag3").val();
    var Status = $("#onoff3").is(":checked");

    $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardDoestic?flag=' + Flag + "&status=" + Status,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {

        }
    });
});

$('#onoff4').click(function () {
    var Flag = $("#flag4").val();
    var Status = $("#onoff4").is(":checked");
     $.ajax({
        type: 'POST',
        url: '/ManageCard/ManageCardDoestic?flag=' + Flag + "&status=" + Status,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {

        }
    });
});
   
});

function LoadPermanentBlockCardDetails() { 
    $("#PBlocked_Card").empty();
 
    $.ajax({
        type: 'POST',
        url: '/Home/LoadPermanentBlockCardDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result != null && result.data != null) {
               
                $("#PBlocked_Card")
                    .append($('<option>', { value: "" })
                        .text("Select"));
                for (var i = 0; i < result.data.userKitDetails.length; i++) {
                    var singleobj = result.data.userKitDetails[i];

                    $("#PBlocked_Card")
                        .append($('<option>', { value: singleobj.kitReferenceNumber })
                            .text(singleobj.kitReferenceNumber));
                }

            }
        }
    });
    return false

}
function DispalyError(message) {
    if (message != '') {
        $('#displaydangermessage').text(message);
        $('#alertPopup').modal('show');
    }
}

function DispalySuccess(message) {
    if (message != '') {
        $('#displaysuccessmessage').text(message);
        $('#SuccessPopup').modal('show');
    }
}

function DisableSuccessPopup() {
    var hrf = $("#btnSuccessok").attr("href");
    if (hrf != '' && hrf != undefined && hrf != "undefined") {
        window.location.href = hrf;
    }
    else {
        $('#SuccessPopup').modal('hide');
    }
}