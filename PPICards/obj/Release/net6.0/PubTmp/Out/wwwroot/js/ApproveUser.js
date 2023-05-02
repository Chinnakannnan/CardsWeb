
$(document).ready(function () {

    function gettoken() {
            var token = '@Html.AntiForgeryToken()';
    token = $(token).val();
    return token;
        }
    var switchStatus = false;
    $("#togBtn").on('change', function () {
            if ($(this).is(':checked')) {
        switchStatus = $(this).is(':checked');
    alert(switchStatus);// To verify
            }
    else {
        switchStatus = $(this).is(':checked');
    alert(switchStatus);// To verify
            }
        });



    $(".KitData3").click(function () {
        alert("hello assign");
    status = "kk";

    $.ajax({
        type: 'POST',
    dataType: 'JSON',
    url: '/Admin/AssignUserKit',
    contentType: "application/json; charset=utf-8 ",
                //contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
                //  data: {Status: status },
    success: function (result) {
        alert("success");
    alert("success" + JSON.stringify(result));
                },
    error: function () {
        alert("error");
                }
            }
    );
        });





    $(".Edit_data").click(function () {
            var i = $(this).attr("chck");
    var Custid = $(this).data("value");
    var addr1 = $(this).data("value1");
    var addr2 = $(this).data("value2");
    var city = $(this).data("value3");
    var pin = $(this).data("value4");
    var state = $(this).data("value5");
    var country = $(this).data("value6");
    var usertype = $(this).data("value7");
    var aadhaarNo = $(this).data("value8");
    var panNo = $(this).data("value9");
    var gstNo = $(this).data("value10");
    var Name1 = $(this).data("value11");
    var email = $(this).data("value12");
    var mobileNum = $(this).data("value13");
    //var  = $(this).data("value12");
    // var usertype = $(this).data("value7");




    alert(Custid + addr1 + addr2 + city + pin + state + country + usertype);

    $("#Name1").val(Name1);
    $("#customerId").val(Custid);
    $("#mobileNo").val(mobileNum);
    $("#emailId").val(email);

    $("#addressLine1").val(addr1);
    $("#addressLine2").val(addr2);
    $("#city").val(city);
    $("#pin").val(pin);
    $("#state").val(state);
    $("#country").val(country);
    $("#aadhaarNo").val(aadhaarNo);
    $("#panNo").val(panNo);
    $("#gstNo").val(gstNo);
    $("#userType").val(usertype);
    $("#customerId").attr("readonly", "readonly");
    $("#userType").attr("readonly", "readonly");


        });



    $("#UpdateData").click(function () {
        console.log('came into click');
    alert("updaste")
    var i = $(this).attr("chck");
    var Status = $("#UserStatus").val();
    var customerId = $("#customerId").val();
    alert(Status);

    var name1 = $("#Name1").val();
    //var = $("#customerId").val();
    var mobileNo = $("#mobileNo").val();
    var emailId = $("#emailId").val();

    var address1 = $("#addressLine1").val();
    var address2 = $("#addressLine2").val();
    var City = $("#city").val();
    var Pin = $("#pin").val();
    var State = $("#state").val();
    var Country = $("#country").val();
    var aadhaarNo = $("#aadhaarNo").val();
    var panNo = $("#panNo").val();
    var gstNo = $("#gstNo").val();


    $.ajax(
    {
        type: 'POST',
    dataType: 'JSON',
    url: "/Admin/UpdateStatus",
    contentType: 'application/x-www-form-urlencoded; charset=utf-8',
    data: {
        __RequestVerificationToken: gettoken(), Status: Status, CustomerId: customerId,
    Name1: name1,
    MobileNo: mobileNo,
    EmailId: emailId,

    Address1: address1,
    Address2: address2,
    City1: City,
    Pin1: Pin,
    State1: State,
    Country1: Country,
    AadhaarNo: aadhaarNo,
    PanNo: panNo,
    GstNo: gstNo
                    },
    success:
    function (data) {
        alert("success" + JSON.stringify(data));

    $("#successModal").modal('show');
                        },
    error:
    function (response) {
        alert("approve failed")
                            $(".erroralert").show().delay(8000).fadeOut();
    $("#errortext").text("Please try again!").show().delay(8000).fadeOut();
                        }
                });

    console.log('came into reject user');

      
        });
    });


