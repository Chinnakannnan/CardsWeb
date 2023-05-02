function LoadData(type) {
    $("#loader").show();
    
    $.ajax(
        {
            type: 'GET',
            dataType: 'JSON',
            url: "/Home/LoadData?type=" + type,
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            success: function (data) {
                $("#loader").hide();
                if (data != '') {
                    var url = data;
                    window.open(url, '_blank');
                }
            }
        });
}


