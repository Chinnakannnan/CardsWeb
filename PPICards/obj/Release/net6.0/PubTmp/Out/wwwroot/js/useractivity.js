var initCountdownValue = 1200;//20 minutes time interval
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
 

    var intervalid = window.setInterval(function Redirect() {
        var inactivityCountdown = localStorage.getItem("timeinterval");
        inactivityCountdown--;
        localStorage.setItem("timeinterval", inactivityCountdown);
        if (inactivityCountdown < 1) {
            clearInterval(intervalid);
            Logout();
        }
    }, 1 * 1200);

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


