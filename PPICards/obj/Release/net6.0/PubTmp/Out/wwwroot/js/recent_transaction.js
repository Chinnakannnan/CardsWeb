$(document).ready(function () {
    $('#loader').attr("style", "display:block !important")
    $.ajax({
        url: "/Transaction/GetTransactionReports",
        contentType: 'application/json',
        type: "POST",
        success: function (data) {
            var html = "";
            for (var i = 0; i < data.transactionDisplayModels.length; i++) {

                if (i < 4) {
                    var sl = data.transactionDisplayModels[i];
                    var type = sl.description;
                    var amount = sl.description;
                    var classes = "";
                    if (type == "CREDIT") {
                        amount = sl.credit;
                        classes = "credited";
                    }
                    else {
                        amount = sl.debit;
                        classes = "debited";
                    }
                    html = html + "<tr><td scope=\"row\"><div class=\"contactDetails d-flex\"><div class=\"contactInfo\"><div class=\"contactName\">" + sl.transactionId + "</div>";
                    html = html + "<div class=\"contactRef small\">Ref ID:<span class=\"contactRefNo strong\">" + sl.refno + "</span></div></div></div></td>";
                    html = html + "<td class=\"align-middle\"><span class=\"small text-gray-400\">" + sl.description + "</span></td><td><div class=\"transactionDetails\"><div class=\"transactionAmount " + classes + "\">";
                    html = html + "<i class=\"fas fa-rupee-sign\"></i> " + amount + "</div><div class=\"transactionDate small\">" + sl.transactionOn + "</div></div></td></tr>";
                }
            }
            if (html == "") {
                html = "<tr><td colspan=\"3\" scope=\"row\"><div style=\"text-align: center;font-weight: bold;\">No recent transaction found !!!</div></td></tr>"
            }
            //var date = "(" + data.fromdate + "-" + data.todate + ")";
            //$("#recentTransactionheader").text(date)
            $("#redenttransactiontbody").html(html);
            $('#loader').attr("style", "display:none !important")
        }

    });

});