var initCountdownValue = 600;//10 minutes time interval
localStorage.setItem("timeinterval", initCountdownValue);
$(document).ready(function () {

    window.addEventListener('storage', function (event) {
        if (event.key == "app-logout") {
            window.location.reload();
        }
    }, false);

    function Logout() {
        LoadLogout();
    }
 /*   $('#NewPwd').on('keyup', function () {
        alert("hi ajih")
        var password = $(this).val();
        var passwordStrength = checkPasswordStrength(password);
        $('#passstrength').html('Password Strength: ' + passwordStrength);
    });

    function checkPasswordStrength(password) {
        var strength = 0;

        if (password.length < 6) {
            return 'Too short';
        }

        if (password.length > 7) strength += 1;

        if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1;

        if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1;

        if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1;

        if (password.length > 12) strength += 1;

        if (strength < 2) {
            return 'Weak';
        } else if (strength == 2) {
            return 'Good';
        } else {
            return 'Strong';
        }
    }*/

    var intervalid = window.setInterval(function Redirect() {
        var inactivityCountdown = localStorage.getItem("timeinterval");
        inactivityCountdown--;
        localStorage.setItem("timeinterval", inactivityCountdown);
        if (inactivityCountdown < 1) {
            clearInterval(intervalid);
            Logout();
        }
    }, 1 * 600);

    document.addEventListener('mousemove', function (e) {
        localStorage.setItem("timeinterval", initCountdownValue);
    });
});




 



function LoadLogout() {
    $.ajax(
        {
            type: 'POST',
            dataType: 'JSON',
            url: "/Login/signout",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            success: function () {
                localStorage.setItem("app-logout", 'logout' + Math.random());
                window.location.reload();
            }
        });
}


