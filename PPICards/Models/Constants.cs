using Microsoft.AspNetCore.Http;

namespace PPICards.Models
{
    public static class OnboardConstants
    {
        public const string DMTURL = "http://localhost:64438/";

        // public const string BaseUrl = "http://localhost:5000/";
          public const string BaseUrl = "https://api.accupayd.com/";

        public const string ErrorMessage = "Please contact administrator!!!";

        // public const string SendingLink = "";
        public const string ContentType = "Content-Type";
        public const string ApplicationJson = "application/json";

        //Login url
        public const string LoginValidate = "/api/v1/Login";
        public const string GetAssignedKitMappingDetailsByCustomer = "api/KitMapping/GetAssignedKitMappingDetailsByCustomer";
        //Transaction details
        public const string TransactionReport = "/api/Transaction/GetTransactionDetails";
        public const string UserRegistration = "/api/OnboardingKycEkyc/UserRegistration";
        public const string GetUser = "/api/OnboardingKycEkyc/GetUser";
        public const string UpdateStatus = "/api/OnboardingKycEkyc/UpdateStatus";
        public const string GetKitData = "/api/Kit/GetKitDetails";
        public const string SetKitNo = "/api/KitMapping/SaveKitMappingDetails";
        public const string LoginOTP = "/api/v1/LoginOtp";
        public const string PassswordReset = "api/User/ResetPassword";
        public const string GetOTP = "/api/Prepaid/ActivateCardOTP";
        public const string CardActivation = "api/Prepaid/ActivateCard";
        public const string GetCustomerBalance = "api/Wallet/FetchCustomerBalance";
        public const string CardLockUnlock = "api/Wallet/BlockCard";
        public const string Customerwallet = "/api/Wallet/LoadCustomerWalletByCard";

        public const string UpdateCustomer = "/api/Prepaid/UpdateCustomer";
        public const string RefundCustomer = "/api/Wallet/RefundCustomerWallet";

        // public const string SendLinkURL = "OnBoardKycEkyc/SendLink/SendLink";
        public const string SendLinkURL = "api/OnboardingKycEkyc/SendLink";

        public const string CheckuniqueId = "api/OnboardingKycEkyc/CheckuniqueId";
        public const string AadhaarLogin = "api/OnboardingKycEkyc/AadhaarLogin";
        public const string GetAadhaar = "api/OnboardingKycEkyc/GetAadhaar";
        public const string Verify = "api/OnboardingKycEkyc/Verify";
        public const string SendingMail = "api/OnboardingKycEkyc/SendingMail";
        public const string SendMobOTP = "api/OnboardingKycEkyc/SendMobOTP";
        public const string GotoRef = "api/OnboardingKycEkyc/GotoRef";
        public const string PanValidation = "api/OnboardingKycEkyc/PanValidation";
        public const string SavePanDetails = "api/OnboardingKycEkyc/SavePanDetails";
        public const string GstValidation = "api/OnboardingKycEkyc/GstValidation";
        public const string SaveGstDetails = "api/OnboardingKycEkyc/SaveGstDetails";
        public const string CreateShop = "api/OnboardingKycEkyc/CreateShop";
        public const string CreateShopKYC_MVC = "api/OnboardingKycEkyc/CreateShopKYC";
        public const string WaitingForAppoval = "api/OnboardingKycEkyc/WaitingForAppoval";
        public const string ApprovedData = "api/OnboardingKycEkyc/ApprovedData";
        public const string Rejectedusers = "api/OnboardingKycEkyc/Rejectedusers";
        public const string SendMail = "api/OnboardingKycEkyc/SendingMail";
        public const string SendMobileOTP = "api/OnboardingKycEkyc/SendMobOTP";
        public const string CreateShopKYC123 = "api/OnboardingKycEkyc/CreateShopKYC";
        // public const string  = "api/OnboardingKycEkyc/";



        //from here support onboarding starts
        public const string SupportAadhaarLogin = "api/OnboardSupport/AadhaarLogin";
        public const string SupportGetAadhaar = "api/OnboardSupport/GetAadhaar";
        public const string SupportVerify = "api/OnboardSupport/Verify";
        public const string SupportSendingMail = "api/OnboardSupport/SendingMail";
        public const string SupportSendMobOTP = "api/OnboardSupport/SendMobOTP";
        public const string SupportGotoRef = "api/OnboardSupport/GotoRef";
        public const string SupportPanValidation = "api/OnboardSupport/PanValidation";
        public const string SupportSavePanDetails = "api/OnboardSupport/SavePanDetails";
        public const string SupportGstValidation = "api/OnboardSupport/GstValidation";
        public const string SupportSaveGstDetails = "api/OnboardSupport/SaveGstDetails";
        public const string SupportCreateShop = "api/OnboardSupport/CreateShop";
        public const string SupportCreateUserMapping = "api/OnboardSupport/CreateUserMapping";
        public const string GetCardDetailsbyCustomer = "api/Wallet/GetCardDetails";
        public const string GetCardCVVDetails = "api/Wallet/GetCardCVVDetails";
        public const string TrancationControl = "/api/Prepaid/CustomerPreferencesExternal";
        public const string TrancationLimitControl = "/api/Prepaid/CustomerTransactionLimit";
        public const string CustomerPreferences = "/api/Prepaid/FetchCustomerPreferences";
        public const string SetCardPin = "api/Prepaid/SetPinWidget";
        public const string FetchCard = "api/Prepaid/FetchCardWidget";
        public const string ReplaceCard = "/api/Wallet/CardReplacement";
        public const string AvailableNewCards = "/api/Wallet/AvailableNewCards";        
        public const string AddKitData = "api/Kit/CreateKitDetails";
        public const string TransactionReportByDate = "api/Wallet/FetchTransactionsByDate";
        public const string RaiseComplaint = "api/User/RaiseComplaint";
        public const string GetuserInfo = "api/v1/GetUserInfo";
        public const string UnauthorizedBlock = "api/v1/BlockUnauthorized";
        
    }

    public class ConstValues
    {
        public const string Username = "userName";
        public const string Password = "password";
        public const string SessionUser = "Raj_User";
        public const string SessionAdmin = "Raj_Admin";
        public const string SessionCustomerId = "CustomerId";
        public const string SessionEmailId = "CustomerEmailId";
        public const string SessionLoginOTP = "LoginOTP";
        public const string SessionUserType = "SessionUserType";
        public const string UserType = "UserType";
        public const string LoginName = "LoginName";
        public const string FirstName = "FirstName";
        public const string UserAccount = "bankaccount";
        public const string EntityId = "entityId";
        public const string SessionMobileNo = "MobileNo";
        public const string SessionDOB = "DOB";
        public const string CardDetails = "mappingtext";
        public const string KycStatus = "KycStatus";
        public const string JwtValue = "JwtValue";
        public const string ClientId = "ClientId";
        public const string ClientValue = "629394edaecc";
        public const string ClientSecrect = "clientsecret";
        public const string ClientSecrectValue = "0b6402c5984041b587f02173958bd13a";
        public const string Bearer = "Bearer"; 
        public const string Authorization = "Authorization";
        public const string CardStatus = "CardStatus";
        public const string Otptime = "OTPTIME";
        public const string Flag = "Flag";
        public const string LoginAttempt = "LoginAttempt";
        public const string OTPAttempt = "OTPAttempt";

    }
}
