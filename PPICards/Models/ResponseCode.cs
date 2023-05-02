public class ResponseModel
{
    public string statuscode { get; set; }
    public string statusdesc { get; set; }


}
public class ResponseCode
{
    public const string Success = "000";
    public const string Failed = "001";
    public const string Request_Empty = "002";
    public const string User_ID_AlreadyExists = "003";
    public const string Invalid_UserID = "004";
    public const string Invalid_MobileNo = "005";
    public const string MobileNo_Already_Exists = "006";
    public const string Invalid_EmailID = "007";
    public const string EmailID_Already_Exists = "008";
    public const string Invalid_GSTNO = "009";
    public const string GSTNO_Already_Exists = "010";
    public const string Invalid_PANNO = "011";
    public const string PANNO_Already_Exists = "012";
    public const string Please_Try_Again = "013";
    public const string Plese_Try_Again_DSNull = "014";
    public const string Plese_Try_Again_TableNull = "015";
    public const string Invalid_Response = "016";
    public const string Activation_Pending = "017";
    public const string User_Blocked = "018";
    public const string Account_Blocked = "019";
    public const string Invalid_Password = "020";
    public const string User_Inactive = "021";
    public const string Invalid_OTP_for_EmailID = "022";
    public const string Invalid_OTP_for_Mobile = "023";
    public const string Timeout_for_mobotp = "024";
    public const string Timeout_for_mailotp = "025";
    public const string Invalid_OTP = "026";
    public const string Enter_OTP = "027";
}

public class ResponseMsg
{
    public const string Success = "Success";
    public const string Failed = "Please Try Again";
    public const string Request_Empty = "Request Empty";
    public const string User_ID_AlreadyExists = "User ID Already Exists";
    public const string Invalid_UserID = "Invalid User ID";
    public const string Invalid_MobileNo = "Invalid Mobile Number";
    public const string MobileNo_Already_Exists = "Mobile Number Already Exists";
    public const string Invalid_EmailID = "Invalid Email ID";
    public const string EmailID_Already_Exists = "Email ID Already Exists";
    public const string Invalid_GSTNO = "Invalid GST Number";
    public const string GSTNO_Already_Exists = "GST Number Already Exists";
    public const string Invalid_PANNO = "Invalid PAN Number";
    public const string PANNO_Already_Exists = "PAN Number Already Exists";
    public const string Please_Try_Again = "Please Try Again";
    public const string Plese_Try_Again_DSNull = "Please Try Again";
    public const string Plese_Try_Again_TableNull = "Please Try Again";
    public const string Invalid_Response = "Invalid Response";
    public const string Activation_Pending = "Activation Pending";
    public const string User_Blocked = "User Blocked";
    public const string Account_Blocked = "Account Blocked";
    public const string Invalid_Password = "Invalid Password";
    public const string User_Inactive = "User Inactive";
    public const string Invalid_MOTP = "Invalid OTP for Mobile number ";
    public const string Invalid_EOTP = "Invalid OTP for EmailID ";
    public const string Timeout_for_mobotp = "Time out for Mobile OTP. Please Try again";
    public const string Timeout_for_mailotp = "Time out for Mail OTP. Please Try again";
    public const string Invalid_OTP = "Enter Valid OTP!";
    public const string Enter_OTP = "Enter OTP";

}