namespace PPICards.Models
{
    public class OnboardingModel
    {
        public class Model
        {

        }
        public static class COMMON
        {
            public static string EMAILKEY = "9BE744B6F2379746";
            public static string KEY = "37974F8A5997A49B";
        }
        //public class JsonResponse
        //{
        //    public string StatusCode { get; set; }
        //    public string Status { get; set; }
        //    public string mobileNo { get; set; }
        //    public object ResponseContent { get; set; }
        //    public string Crnno { get; set; }
        //}
        public class RegistrationRequest
        {


            public string customerId { get; set; }
            public string shopname { get; set; }
            public string title { get; set; }
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public string gender { get; set; }
            public string mobileNo { get; set; }
            public string emailAddress { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string areaname { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string pincode { get; set; }
            public string gst { get; set; }
            public string pan { get; set; }
            public string userPhotoURL { get; set; }
            public string idProofType { get; set; }
            public string idProofNumber { get; set; }
            public string idProofURL { get; set; }
            public string addressProofType { get; set; }
            public string addressProofNumber { get; set; }
            public string addressProofURL { get; set; }
            public string password { get; set; }
            public string tpin { get; set; }
            public string usertype { get; set; }
            public string commissiontype { get; set; }
            public string agenttype { get; set; }
            public string status { get; set; }
            public string tpinstatus { get; set; }
            public string kycstatus { get; set; }
            public string master { get; set; }
            public string distributer { get; set; }
            public string retailer { get; set; }
            public string passwordcount { get; set; }
            public string passwordexpiry { get; set; }
            public string cdatetime { get; set; }
            public string mdatetime { get; set; }
            public string modifiedby { get; set; }
            public string createdby { get; set; }
            public string remarks { get; set; }
            public string aesKey { get; set; }
            public string consumerkey { get; set; }
            public string consumersecret { get; set; }
            public string StatusCode { get; set; }
            public string StatusDesc { get; set; }
            public string flag { get; set; }
            public string hash { get; set; }
            public string ipaddress { get; set; }
            public string vaccountid { get; set; }
            public string vbankname { get; set; }
            public string vifsccode { get; set; }
            public string walletbalance { get; set; }
            public string mapusertype { get; set; }
            public string mapUserCode { get; set; }
            public string kitStatus { get; set; }
            public string drivingLicense { get; set; }
            public string voterId { get; set; }
            public string aadhaarNo { get; set; }
            public string dob { get; set; }
            //@usertype int, -- 1- admin - 2- super distributor 3 - distributor 4- retailer
            //@commissiontype int, -- 0 - ADMIN 1 - WL - 2 - API - 3- PG
            //@agenttype int, -- 1- admin - 2- super distributor 3 - distributor 4- retailer
            //@status int, -- 0 - pending 1 - active 2 - blocked 3 - in-active
        }
        public class JsonRes
        {
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string CustomerId { get; set; }
            public object ResponseContent { get; set; }
            public object data { get; set; }
            public string Crnno { get; set; }
            public string id { get; set; }
            public string Flag { get; set; }

            public string userId { get; set; }
            public string patronId { get; set; }
            public string refId { get; set; }

            public result getres { get; set; }


            public string UserType { get; set; }

            public string shopname { get; set; }
            public string firstname { get; set; }

        }
        public class LoginRequest
        {
            public string userId { get; set; }
            public string password { get; set; }
            public string userType { get; set; }
            public string kitNo { get; set; }
            public string statusCode { get; set; }
            public string status { get; set; }
        }

        public class KitDetails
        {
            public int kitId { get; set; }
            public string kitReferenceNumber { get; set; }
            public string cardNo { get; set; }
            public DateTime? cardExpiryDate { get; set; }
            public string cardType { get; set; }
            public string companyCode { get; set; }
            public string companyAdminCode { get; set; }
            public DateTime createdDate { get; set; }
            public string createdBy { get; set; }
            public DateTime modifiedDate { get; set; }
            public string modifiedBy { get; set; }
            public bool isActive { get; set; }
            public bool isAssigned { get; set; }
            public bool isActivated { get; set; }
            public string cardCategory { get; set; }
        }
        public class KitMappingModel
        {
            public int KitMappingId { get; set; }
            public string KitReferenceNumber { get; set; }
            public string CardType { get; set; }
            public string CardCategory { get; set; }
            public string EntityId { get; set; }
            public string CustomerId { get; set; }
            public string ProductId { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public bool IsActive { get; set; }
            public Decimal NonKyc_Dom_ATM_Limit { get; set; }
            public Decimal NonKyc_Dom_tab_Limit { get; set; }
            public Decimal NonKyc_Dom_Online_Limit { get; set; }
            public Decimal NonKyc_Dom_Outlet_Limit { get; set; }
            public Decimal Kyc_Dom_ATM_Limit { get; set; }
            public Decimal Kyc_Dom_tab_Limit { get; set; }
            public Decimal Kyc_Dom_Online_Limit { get; set; }
            public Decimal Kyc_Dom_Outlet_Limit { get; set; }
        }
        public class kyc
        {
            public string sno { get; set; }
            public string name { get; set; }
            public string middlename { get; set; }
            public string lastname { get; set; }
            public string title { get; set; }
            public string refId { get; set; }
            public string customerId { get; set; }
            public string shopNumber { get; set; }
            public string shopName { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string pincode { get; set; }
            //public string Zipcode { get; set; }
            public string country { get; set; }
            public string gstNo { get; set; }
            public string onboardType { get; set; }

            public FormFile gstSoftcopy { get; set; }
            public string gst_imgString { get; set; }
            public string gst_imgName { get; set; }
            public string pancardNo { get; set; }
            public FormFile panSoftcopy { get; set; }
            public string pan_imgString { get; set; }
            public string pan_imgName { get; set; }
            public string aadhaarNo { get; set; }
            public FormFile aadhaarfrontSoftcopy { get; set; }
            public string aadhaarfront_imgString { get; set; }
            public string aadharback_imgString { get; set; }
            public string aadharfront_imgName { get; set; }
            public FormFile aadharbackSoftcopy { get; set; }
            public string aadhaarback_imgName { get; set; }

            public string mobileNo { get; set; }
            public string mobile2 { get; set; }
            public string emailId { get; set; }
            public string usertype { get; set; }
            public string mailOTP { get; set; }
            public string mobOTP { get; set; }
            public string flag { get; set; }
            public string mobileOtp { get; set; }
            public string ipAddress { get; set; }
            public string aadhaarOTP { get; set; }
            public string mapUsercode { get; set; }
            public string supUsertype { get; set; }
            public string uniqueId { get; set; }

            // public string Zipcode { get; set; }



        }
        public class sendLink
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailId { get; set; }
            public string mobileNumber { get; set; }
            public string mapUsercode { get; set; }
            public string supUsertype { get; set; }
            public string uniqueId { get; set; }
        }


        public class OnboardKycEkycModel
        {
            public string sno { get; set; }
            public string status { get; set; }
            public string name { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public string title { get; set; }
            public string gender { get; set; }
            public string refId { get; set; }

            public string customerId { get; set; }

            public string shopNumber { get; set; }
            public string shopName { get; set; }
            public string password { get; set; }
            public string tpin { get; set; }

            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string addressLine3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string pincode { get; set; }
            public string Zipcode { get; set; }
            public string country { get; set; }
            public string gstNo { get; set; }
            public string gstSoftcopy { get; set; }
            public string gst_imgString { get; set; }
            public string gst_imgName { get; set; }
            public string imgPath { get; set; }
            public string pancardNo { get; set; }
            public string panSoftcopy { get; set; }
            public string pan_imgString { get; set; }
            public string pan_imgName { get; set; }
            public string aadhaarNo { get; set; }
            public string aadhaarfrontSoftcopy { get; set; }
            public string aadhaarfront_imgString { get; set; }
            public string aadharfront_imgName { get; set; }
            public string aadharbackSoftcopy { get; set; }
            public string aadharback_imgString { get; set; }
            public string aadhaarback_imgName { get; set; }
            public string idProofType { get; set; }
            public string mobileNo { get; set; }
            public string mobile2 { get; set; }
            public string emailId { get; set; }
            public string usertype { get; set; }
            public string mobOTP { get; set; }
            public string mailOTP { get; set; }
            public string ipAddress { get; set; }
            public string onboardType { get; set; }
            public string aesKey { get; set; }
            public string consumerkey { get; set; }
            public string consumersecret { get; set; }
            public string mapusertype { get; set; }
            public string mapUserCode { get; set; }
            public string uniqueId { get; set; }
            public string flag { get; set; }

        }




        public class kycget
        {
            public string sno { get; set; }
            public string name { get; set; }
            public string refId { get; set; }
            public string customerId { get; set; }
            public string shopNumber { get; set; }
            public string shopName { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            //public string pincode { get; set; }
            public string zipcode { get; set; }
            public string country { get; set; }
            public string gstNo { get; set; }
            //  public HttpPostedFileBase gstSoftcopy { get; set; }
            public string gst_imgString { get; set; }
            public string gst_imgName { get; set; }
            public string pancardNo { get; set; }
            // public HttpPostedFileBase panSoftcopy { get; set; }
            public string pan_imgString { get; set; }
            public string pan_imgName { get; set; }
            public string aadhaarNo { get; set; }
            //  public HttpPostedFileBase aadhaarfrontSoftcopy { get; set; }
            public string aadhaarfront_imgString { get; set; }
            public string aadharback_imgString { get; set; }
            public string aadharfront_imgName { get; set; }
            //  public HttpPostedFileBase aadharbackSoftcopy { get; set; }
            public string aadhaarback_imgName { get; set; }

            public string mobileNo { get; set; }
            public string mobile2 { get; set; }
            public string emailId { get; set; }
            public string usertype { get; set; }
            public string mailOTP { get; set; }
            public string mobOTP { get; set; }
            public string flag { get; set; }
            public string mobileOtp { get; set; }


            // public string Zipcode { get; set; }



        }




        public class validation
        {
            public string username { get; set; }
            public string password { get; set; }
            public string url { get; set; }
            public string patronId { get; set; }
            public string accesstoken { get; set; }
            public string requestId { get; set; }

        }
        public class JsonResponse
        {
            public JsonResponse()
            {
                ResponseContent = new List<UserData>();
            }
            //public string statusCode { get; set; }
            public string StatusCode { get; set; }
            //public string status { get; set; }
            public string Status { get; set; }
            //public object responseContent { get; set; }
            public List<UserData> ResponseContent { get; set; }
            //public List<object> ResponseContent { get; set; }
        }
        public class UserData
        {
            public string customerId { get; set; }
            public string usertype { get; set; }
            public string firstName { get; set; }
        }
        public class results
        {
            public string url { get; set; }
            public string requestId { get; set; }
        }
        public class Aadhaarobj
        {
            public Aadhaarobj()
            {
                responseContent = new results();
            }
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string authuid { get; set; }

            public string userId { get; set; }
            public string request_id { get; set; }
            public results responseContent { get; set; }

        }




        public class GetAadharJsonResponse
        {
            public GetAadharJsonResponse()
            {
                result = new getresults();
            }
            public getresults result { get; set; }
            public string ReferId { get; set; }
            public string emailId { get; set; }
            public string aadhaarNo { get; set; }
            public string pancardNo { get; set; }
            public string gstNo { get; set; }
            public string flag { get; set; }
        }
        public class getresults
        {
            public getresults()
            {
                splitAddress = new splitAddr();
            }
            public string name { get; set; }
            public string dob { get; set; }
            public string gender { get; set; }
            public string address { get; set; }
            public string photo { get; set; }
            public string ReferId { get; set; }
            public string Emailid { get; set; }
            public string aadhaarNo { get; set; }
            public string pancardNo { get; set; }
            public string gstNo { get; set; }
            public splitAddr splitAddress { get; set; }

        }
        public class splitAddr
        {
            //public splitAddr()
            //{
            //    district = new List<districtArray>();
            //}
            public List<string> district { get; set; }
            public List<string> city { get; set; }
            public List<string> country { get; set; }
            public List<string[]> state { get; set; }
            public string pincode { get; set; }
        }


        public class GetJsonResponse
        {

            public string ReferId { get; set; }
            public string EmailId { get; set; }
            public string AadhaarNo { get; set; }
            public string PanNo { get; set; }
            public string GstNo { get; set; }
            public string GST_Detail { get; set; }
            public string Aadhaar_Details { get; set; }
            public string PAN_Details { get; set; }


        }

        public class kycgetList
        {

            public string Sno { get; set; }
            public string Name { get; set; }
            public string Refid { get; set; }

            public string CustomerId { get; set; }

            public string ShopNumber { get; set; }
            public string ShopName { get; set; }
            public string Password { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string pincode { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
            public string GSTNo { get; set; }
            public string GSTSoftcopy { get; set; }


            public string GSTImg { get; set; }
            public string Imgpath { get; set; }
            public string PanCardNo { get; set; }
            //public string PanSoftcopy { get; set; }
            //public string PanSoftcopy1 { get; set; }
            public string panSoftcopy { get; set; }
            public string AadhaarNo { get; set; }
            public string AadhaarSoftcopy { get; set; }
            public string AadharBack { get; set; }
            public string Status { get; set; }
            public string AdFImg { get; set; }
            public string onboardType { get; set; }
            public string mapusercode { get; set; }
            public string mapusertype { get; set; }

            public string AdBImg { get; set; }
            public string MobileNo { get; set; }
            public string Mobile2 { get; set; }
            public string EmailId { get; set; }
            public string UserType { get; set; }
            public string mobOTP { get; set; }
            public string mailOTP { get; set; }
            public string flag { get; set; }
        }
        public class kyclist
        {

            public string Sno { get; set; }
            public string Name { get; set; }
            public string Refid { get; set; }

            public string CustomerId { get; set; }

            public string ShopNumber { get; set; }
            public string ShopName { get; set; }
            public string Password { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
            public string GSTNo { get; set; }
            public string GSTSoftcopy { get; set; }
            public string GSTSoftcopy1 { get; set; }
            public string GSTImg { get; set; }
            public string Imgpath { get; set; }
            public string PanCardNo { get; set; }
            //public string PanSoftcopy { get; set; }
            //public string PanSoftcopy1 { get; set; }
            public string PanImg { get; set; }
            public string AadhaarNo { get; set; }
            public string AadhaarSoftcopy { get; set; }
            public string AadhaarSoftcopy1 { get; set; }
            public string AdFImg { get; set; }
            public string AadharBack { get; set; }
            public string AadharBack1 { get; set; }
            public string AdBImg { get; set; }
            public string MobileNo { get; set; }
            public string Mobile2 { get; set; }
            public string EmailId { get; set; }
            public string UserType { get; set; }
            public string mobOTP { get; set; }
            public string mailOTP { get; set; }
            public string flag { get; set; }
        }


        public class JsonReq
        {
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string CustomerId { get; set; }
            public object ResponseContent { get; set; }
            public object ResponseContent1 { get; set; }
            public object ResponseContent2 { get; set; }
            public object data { get; set; }
            public string Crnno { get; set; }
            public string id { get; set; }
            public string Flag { get; set; }

            public string userId { get; set; }
            public string patronId { get; set; }
            public string refId { get; set; }

            public result getres { get; set; }


            public string UserType { get; set; }

            public string shopname { get; set; }
            public string firstname { get; set; }

        }
        public class result
        {
            public string url { get; set; }
            public string requestId { get; set; }
        }


        public class userAadharJsonResponse
        {
            public userAadharJsonResponse()
            {
                result = new aadharResult();
            }
            public string ReferId { get; set; }
            public string emailId { get; set; }
            public string aadhaarNo { get; set; }
            public string pancardNo { get; set; }
            public string gstNo { get; set; }
            public string flag { get; set; }

            public aadharResult result { get; set; }

        }
        public class aadharResult
        {
            public aadharResult()
            {
                user_address = new address();
            }
            public string user_full_name { get; set; }
            public string user_aadhaar_number { get; set; }
            public string user_dob { get; set; }
            public string address_zip { get; set; }
            public address user_address { get; set; }
        }
        public class address
        {

            public string country { get; set; }
            public string dist { get; set; }
            public string state { get; set; }
            public string house { get; set; }
            public string street { get; set; }
            public string loc { get; set; }
            public string po { get; set; }


        }




        public class TxnLimit
        { 
            public string entityId { get; set; }
            public string KitReferenceNumber { get; set; }
            public string txnType { get; set; }
            public string dailyLimitValue { get; set; }
 
    }



        public class LimitConfig
        {
            public string txnType { get; set; }
            public string dailyLimitValue { get; set; }
        }

        public class CustomerPreferencesResponse
        {
            public CustomerPreferencesResponse()
            {
                result = new Result();
            }
            public Result result { get; set; }
            public string exception { get; set; }
            public string pagination { get; set; }

        }
        public class Result
        {
            public string atm { get; set; }
            public string pos { get; set; }
            public string ecom { get; set; }
            public string international { get; set; }
            public string dcc { get; set; }
            public string contactless { get; set; }

        }
        public class AvailabeNewcards
        {
            public string newKitNo { get; set; }        

        }

    }
}























