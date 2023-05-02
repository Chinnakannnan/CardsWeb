function DispalyError(message) {
    if (message != '') {
        $('#displaydangermessage').html(message);
        $('#alertPopup').modal('show');
    }
}
var columns = [];
$(document).ready(function () {
    var columndisplayname = [];
    columndisplayname.push("Transaction ID");
    columndisplayname.push("Transaction Date");
    columndisplayname.push("Description");
    columndisplayname.push("Credits");
    columndisplayname.push("Debits");
    columndisplayname.push("Balance");
    var columnname = [];
    columnname.push("transactionId");
    columnname.push("transactionOn");
    columnname.push("description");
    columnname.push("credit");
    columnname.push("debit");
    columnname.push("balance");

    $("#btnsubmit").on("click", function () {
        $('#loader').attr("style", "display:block !important")
        var fromdate = $("#form_date").val();
        var todate = $("#to_date").val();
        $.ajax({
            url: "/Transaction/GetTransactionReports?fromdate=" + fromdate + "&todate=" + todate + "&actiontype=filter" ,
            contentType: 'application/json',
            type: "POST",
            success: function (data) {
                if (data.isError) {
                    $('#loader').attr("style", "display:none !important")
                    var message = data.errorMessage;
                    if (message == "") {
                        message = "Internal Error,Contact Admin!!!"
                    }
                    DispalyError(message);
                }
                else {
                    for (var i in columnname) {
                        columns.push({
                            data: columnname[i],
                            title: columndisplayname[i]
                        });
                    }
                    $('#transactionStatement').DataTable({
                        "oLanguage": {
                            "sEmptyTable": "No Record Found !!!"
                        },
                        "processing": true, // for show progress bar
                        "serverSide": false, // for process server side
                        "filter": true, // this is for disable filter (search box)
                        "orderMulti": false, // for disable multiple column at once
                        "retrieve": true,
                        "data": data.transactionDisplayModels,
                        "columns": columns
                    });
                    var datanew = data.transactionDisplayModels;
                    $('#transactionStatement').DataTable().clear().rows.add(datanew).order([1, 'desc']).draw();;
                    $('#loader').attr("style", "display:none !important")
                }
            }

        });
    });
});