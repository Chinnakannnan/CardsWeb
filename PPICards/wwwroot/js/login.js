$(document).ready(function () {


    $('#resendotp').click(function () {

 
        $.ajax({          
            type: 'POST',
            url: '/Login/ResentOTP',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
            success: function (result) {
                if (result == '1') {
                    timer2();
                }
            }

        });
    });

    $('#resendotp').click(function () {
      
        $("#loginbutton").hide();

    });

});

function timer() {

    $("#resendotp").hide();
    var count = 60;
    var timer = setInterval(function () {
        count--;
        $("#timer").text(count);
        if (count == 0) {
            clearInterval(timer);
            $("#timer").text("Expired");
            $("#resendotp").show();
        }
    }, 1000);
}
function timer2() {
    $("#resendotp").hide();
    var count = 60;
    var timer = setInterval(function () {
        count--;
        $("#timer").text(count);
        if (count == 0) {
            clearInterval(timer);
            $("#timer").text("Expired"); 
        }
    }, 1000);
}
function showLoginHide(element1, element2) {
    $('#loginbutton').hide();
    
    var data = $("#loginForm").serialize();
    timer();
    $.ajax({
        type: 'POST',
        url: '/Login/LoginView',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
            //var student = JSON.parse(result);
            if (result.statusCode == "000") {
                //alert(result.emailAddress);
                const myElement2 = document.getElementById(element2);
                myElement2.style.display = "none";
                const myElement1 = document.getElementById(element1);
                myElement1.style.display = "block";
 
            }
            else
            {
                    $('#displaydangermessage').text("Login failed:" + result.statusDesc);
                    $('#alertPopup').modal('show');
 
               
            }

        },
        error: function () {
            const myElement2 = document.getElementById(element2);
            myElement2.style.display = "block";
            const myElement1 = document.getElementById(element1);
            myElement1.style.display = "none";
        }
    })
}
function submitLogin() {
    
    var data = $("#otpform").serialize(); 
    
    $.ajax({
        type: 'POST',
        url: '/Login/ValidateOTP',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
            if (result == 'admin') {
                window.location.href = "/admin/index";
            } else if (result == 'user') {
                window.location.href = "/home/index";
            }
            else if (result == '1') {
                $('#displaydangermessage').text("Login failed:Invalid OTP");
                $('#alertPopup').modal('show');
            }
            else if (result == '2') {
                $('#displaydangermessage').text("OTP Expired");
                $('#alertPopup').modal('show');
            }
            else if (result == '3') {
                $('#displaydangermessage').text("Technical Error Please Contact Administrator");
                $('#alertPopup').modal('show');
            }/*
            else if (result == '4') {
                $('#displaydangermessage').text("Account has block temporarily due to unauthrized attempts");
                $('#alertPopup').modal('show');
            }*/
            else if (result == '5') {
                $('#displaydangermessage').text("Account has block temporarily.");
                $('#alertPopup').modal('show');
            }
            else if (result == '6') {
                $('#displaydangermessage').text("Account has block temporarily due to unauthrized OTP attempts.");
                $('#alertPopup').modal('show');
            }
            else {
                $('#displaydangermessage').text("Technical Error");
                $('#alertPopup').modal('show');
            }
        },
        error: function () {
            //alert('Failed to receive the Data');
            // console.log('Failed ');
        }
    })
}
 