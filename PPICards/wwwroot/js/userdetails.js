$(document).ready(function () {
    LoadCardDetails();
    $('#cvvchkbox').click(function () {

        if (this.checked) {        
            Loadcvv(2);
        }
        else { 
       
            Loadcvv(1);
        }
    });

   

});

function Loadcvv(obj) {
    $.ajax({
        type: 'POST',
        url: '/Home/Loadcvv?type=' + obj,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            $("#cardNumber").text(result.cardNumber);
            $("#cardexpiry").text(result.cardExpiryDate);
            $("#cardcvv").text(result.cvv);

        }
    });
}
function LoadCardDetails() {
    $('#cardNo').empty();
    $.ajax({
        type: 'POST',
        url: '/Home/LoadCardDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result != null && result.data != null) {
                $('#cardNo')
                    .append($('<option>', { value: "Select" })
                        .text("Select"));
                for (var i = 0; i < result.data.userKitDetails.length; i++) {
                    var singleobj = result.data.userKitDetails[i];
                    $('#cardNo')
                        .append($('<option>', { value: singleobj.kitReferenceNumber })
                            .text(singleobj.kitReferenceNumber));
                }

                var cardinfo = result.data.cardModel;
                $("#cardNumber").text(cardinfo.cardNumber);
                $("#cardexpiry").text(cardinfo.cardExpiryDate);
                $("#cardcvv").text(cardinfo.cvv);
               
            }
        }
   
    });
}
function LoadActivatedCardDetails() {
    $("#Active_Card").empty();

    $.ajax({
        type: 'POST',
        url: '/Home/LoadActivatedCardDetails',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            if (result != null && result.data != null) {
                $("#Active_Card")
                    .append($('<option>', { value: "" })
                        .text("Select"));
                for (var i = 0; i < result.data.userKitDetails.length; i++) {
                    var singleobj = result.data.userKitDetails[i];

                    $("#Active_Card")
                        .append($('<option>', { value: singleobj.kitReferenceNumber })
                            .text(singleobj.kitReferenceNumber));
                }

            }
        }
    });
    return false

}

function GenerateOTP() {
    var value = $('select#cardNo option:selected').val();
    if (value == "Select") {
        DispalyError("Please select any one card for the activation !!!")
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Home/GenerateOTP?CardNumber=' + value,
            success: function (result) {
                if (result != '') {
                    var split = result.split("@");
                    var mail = split[split.length - 2];
                    $("#emailId").text("An otp has been sent to ********" + mail.substr(mail.length - 3) + "@" + split[split.length - 1]);
                    showHideFlex('otp', 'addcard');
                    $("#otpSubmit").show();
                    $("#otpgenarte").hide();
                }
                else {
                    DispalyError("OTP generation failed !!!")
                }
            }
        });
    }
}
function ActivateCard() {
    var value = $('select#cardNo option:selected').val();
    var OTP = '';
    for (var i = 1; i <= 6; i++) {
        var id = "#a_otp_" + i;
        OTP = OTP + $(id).val();
    }
    if (OTP != '' && OTP.length == 6) {
        $.ajax({
            type: 'POST',
            url: '/Home/ActivateCard?CardNumber=' + value + "&OTP=" + OTP,
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            success: function (result) {
                if (result.isSuccess) {
                    $("#btnSuccessok").attr("href", "/home/index");
                    DispalySuccess("Card activated successfully !!!");
                }
                else {
                    DispalyError(result.errorMessage);
                }

            }

        });
    }
    else {
        DispalyError("Invalid OTP !!!");
    }
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