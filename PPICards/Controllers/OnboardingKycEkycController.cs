using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPICards.Models;
using System.Text;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;

namespace PPICards.Controllers
{
    [Authorize]
    public class OnboardingKycEkycController : Controller
    {


        //public IActionResult Onboarding()
        //{
        //    return View();
        //}
        public IActionResult KycEkyc()
        {
            // HttpContext.Session.SetString("SenderUsertype", "1");
            // TempData["uniqueId"] = "dfserw53453";
            return View();

        }



        public string lstrFolderName = "ShopKYC";
        private readonly IConfiguration Configuration;
        public string DMTURL;
        public OnboardingKycEkycController(IConfiguration _configuration)
        {

            Configuration = _configuration;
            DMTURL = Configuration.GetSection("appSettings")["DMTURL"];

        }
        //private IWebHostEnvironment _hostingEnvironment;

        //public OnboardLink (IWebHostEnvironment environment)
        //{
        //    _hostingEnvironment = environment;
        //}


        //public IActionResult KycEkyc(string uniqueId)
        //{
        //    HttpContext.Session.SetString("SenderUsertype", "1");
        //    return View("KycEkyc");
        //}
        //public ActionResult KycEkyc(string uniqueId)
        //{



        //    JsonRes objResponse = new JsonRes();
        //    sendLink objRequest = new sendLink();
        //    Dictionary<string, string> requestBody = new Dictionary<string, string>();
        //    string lstrReturn = string.Empty;
        //    string lstrData = string.Empty;
        //    string lstrConsumerSecret = string.Empty;
        //    string lstrAPIKEY = string.Empty;
        //    // string uniId = "";
        //    try
        //    {


        //        lstrConsumerSecret = Configuration.GetSection("appSettings")["APIConsumerSecret"];
        //        lstrAPIKEY = Configuration.GetSection("appSettings")["APIConsumerKey"];

        //        //for testinmg purpose
        //        //string mapusertyp = crypto.AES_DECRYPT(Session["custusertype"].ToString(), COMMON.KEY);

        //        requestBody.Add("uniqueId", uniqueId);

        //        HttpClient http = new HttpClient();
        //        http.BaseAddress = new Uri(OnboardConstants.DMTURL);
        //        http.DefaultRequestHeaders.Accept.Clear();

        //        http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);



        //        var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
        //        HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.CheckuniqueId, stringContent).Result;
        //        string responsestring = JsonSerializer.Serialize(responseMessage);

        //        if (string.IsNullOrEmpty(responsestring))
        //        {
        //            ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
        //            return View("SendLink");
        //        }
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status + " Check your Email ID for Link ";

        //            return View("SendLink");

        //        }

        //        else
        //        {
        //            ViewBag.data = "Failed to send link";

        //            return View("SendLink");
        //        }

        //        lstrData = JsonSerializer.Serialize(requestBody);

        //      //  lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoardingLink/CheckuniqueId", "", lstrData, "application/json");
        //        //lstrReturn = "{\"StatusCode\":\"000\",\"Status\":\"Success\",\"CustomerId\":null,\"ResponseContent\":null,\"ResponseContent1\":null,\"ResponseContent2\":null,\"RefId\":null,\"Flag\":null,\"id\":null,\"userId\":null,\"patronId\":null,\"getres\":null,\"GroupID\":null,\"UserType\":null,\"shopname\":null,\"firstname\":null,\"uniId\":null}";

        //        if (string.IsNullOrEmpty(lstrReturn))
        //        {
        //            ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
        //        }

        //        objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn); ;

        //        //uncommand these lines while working

        //        //IList<sendLink> objReport = new List<sendLink>();

        //        //objReport = JsonSerializer.Deserialize<IList<sendLink>>(objResponse.ResponseContent.ToString());
        //        //command this line when moving the code.........
        //        objResponse.StatusCode = "000";

        //        if (objResponse.StatusCode != "000")
        //        {
        //            // TempData["uniqueId"] == null;
        //            TempData.Clear();
        //            TempData["result"] = objResponse.StatusCode + "|" + objResponse.Status;

        //            return View("KycEKyc");
        //        }
        //        else
        //        {


        //            TempData["uniqueId"] = objResponse;
        //            HttpContext.Session.Remove("uniId");

        //            HttpContext.Session.SetString("uniId", TempData["uniqueId"].ToString());
        //            //these lines are for testing purpose

        //            HttpContext.Session.SetString("SenderUsertype", "2");
        //            HttpContext.Session.SetString("SenderUserCustId", "A01016");
        //            HttpContext.Session.GetString("SenderUserCustId");
        //            ViewBag.uniqueId = 123123123;



        //            //these lines should be uncommanded for live purpose!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //            //foreach (var item in objReport)
        //            //{
        //            //    Session["SenderUsertype"] = item.supUsertype;
        //            //    Session["SenderUserCustId"] = item.mapUsercode;
        //            // uniId = item.uniqueId.ToString();

        //            //}
        //            //ViewBag.uniqueId = uniId;







        //            //TempData["uniqueId"] = 123456;

        //            //not needed i guess
        //            //TempData["MUsrType"] = "3";
        //            return View("KycEKyc");

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //       // objLog.WriteErrorLog("ViewLimit: Exception : " + ex.ToString(), lstrFolderName);
        //    }
        //    finally
        //    {

        //        objRequest = null;
        //    }


        //    return View("KycEKyc");

        //}

















        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult AadhaarValidation(string aadhaarNo)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            // kyc objreq = new kyc();
            // HTTPPOST objPost = new HTTPPOST();
            string otp = string.Empty;
            // JsonRes objResponse = new JsonRes();
            requestBody.Add("aadhaarNo", aadhaarNo);



            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(OnboardConstants.DMTURL);
            http.DefaultRequestHeaders.Accept.Clear();

            http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);



            var stringContent = new StringContent(JsonSerializer.Serialize(aadhaarNo), Encoding.UTF8, OnboardConstants.ApplicationJson);
            HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.CheckuniqueId, stringContent).Result;
            string responsestring = JsonSerializer.Serialize(responseMessage);

            if (string.IsNullOrEmpty(responsestring))
            {
                ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                return Json(0);
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.data = " ";

                return Json(0);

            }

            else
            {
                ViewBag.data = "Failed to send link";

                return Json(0);
            }
            lstrData = JsonSerializer.Serialize(requestBody);
            //  lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoardingLink/AadhaarLogin", "", lstrData, "application/json");
            // lstrReturn = "{\"StatusCode\":\"000\",\"Status\":\"Success\",\"authuid\":\"GSuIljV6d7Mo7AwbzGsXO2Tq5Dc9uz6RjdLpHRaiV4DrSFyte3vjvcOAcjSsK3an\",\"ttl\":null,\"created\":null,\"userId\":\"62e8d589f58b0e25e195a290\",\"requestId\":null,\"Aadharno\":null,\"ResponseContent\":{\"url\":\"https://api.digitallocker.gov.in/public/oauth2/1/authorize?client_id=7E5773C4&dl_flow&redirect_uri=https%3A%2F%2Fdigilocker-preproduction.signzy.tech%2Fdigilocker-auth-complete&response_type=code&state=6315c8ec7a490fa69e46388b\",\"requestId\":\"6315c8ec7a490fa69e46388b\"}}";
            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json(0);
            }
            Aadhaarobj aaResponse = JsonSerializer.Deserialize<Aadhaarobj>(lstrReturn);
            if (aaResponse.StatusCode != "000")
            {
                ViewBag.data = aaResponse.StatusCode + "|" + aaResponse.Status;
                return Json(0);
            }
            else
            {
                ViewBag.data = aaResponse.StatusCode + "|" + aaResponse.Status;
                return Json(aaResponse);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetAadhaar(string requestId, string OTP, string emailId)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            //Dictionary<string, object> requestBody = new Dictionary<string, object>();

            //  HTTPPOST objPost = new HTTPPOST();
            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();

            requestBody.Add("requestId", requestId);
            requestBody.Add("OTP", OTP);
            requestBody.Add("emailId", emailId);

            lstrData = JsonSerializer.Serialize(requestBody);
            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoardingLink/GetAadhaar", "", lstrData, "application/json");
            // lstrReturn = "{\"StatusCode\":\"000\",\"Status\":\"Success\",\"CustomerId\":null,\"ResponseContent\":\"{\\\"essentials\\\":{\\\"requestId\\\":\\\"6315c8ec7a490fa69e46388b\\\"},\\\"id\\\":\\\"6315c952a341350e797bc64f\\\",\\\"patronId\\\":\\\"62e8d589f58b0e25e195a290\\\",\\\"task\\\":\\\"getEadhaar\\\",\\\"result\\\":{\\\"name\\\":\\\"S CYNTHIA\\\",\\\"uid\\\":\\\"xxxxxxxx5572\\\",\\\"dob\\\":\\\"12/08/1998\\\",\\\"gender\\\":\\\"FEMALE\\\",\\\"x509Data\\\":{\\\"subjectName\\\":\\\"DS NATIONAL E-GOVERNANCE DIVISION\\\",\\\"certificate\\\":\\\"MIIGUTCCBTmgAwIBAgIIJh6AfQEBlGgwDQYJKoZIhvcNAQELBQAwdDELMAkGA1UEBhMCSU4xIjAgBgNVBAoTGVNpZnkgVGVjaG5vbG9naWVzIExpbWl0ZWQxDzANBgNVBAsTBlN1Yi1DQTEwMC4GA1UEAxMnU2FmZVNjcnlwdCBzdWItQ0EgZm9yIFJDQUkgQ2xhc3MgMiAyMDE0MB4XDTIwMDkwNTA3MTgyOVoXDTIzMDkwNTA3MTgyOVowggFhMQswCQYDVQQGEwJJTjEnMCUGA1UEChMeTkFUSU9OQUwgRS1HT1ZFUk5BTkNFIERJVklTSU9OMTswOQYDVQQLEzJNSU5JU1RSWSBPRiBFTEVDVFJPTklDUyBBTkQgSU5GT1JNQVRJT04gVEVDSE5PTE9HWTEPMA0GA1UEERMGMTEwMDAzMQ4wDAYDVQQIEwVEZWxoaTE8MDoGA1UECRMzRUxFQ1RST05JQ1MgTklLRVRBTixDR08gQ09NUExFWCxORVcgREVMSEksTkVXIERFTEhJMRYwFAYDVQQzEw02IENHTyBDT01QTEVYMUkwRwYDVQQFE0AwZjMwY2I2NzMzM2Y2MzRkYjExZmRiNDE4NjhiMzQzYTZmMDQ2OTU4MWJkYjlmZDJhMmU0Y2I1OWU3Nzk3YTQzMSowKAYDVQQDEyFEUyBOQVRJT05BTCBFLUdPVkVSTkFOQ0UgRElWSVNJT04wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDeGBT4S0TkE6XsHrGZ/dAyYSh/oOrglaKJsrWX4qre7cdRLBgPGvzLSXZNLrndhJmCYi7VTixWf3ZVR8V4nn+RjdPsb7g9SH+L/VYXRmlCjFXSEHyf1xg2muQd1Uzlka429l4+0y42cD93MGgkdSha0e4k3cYJK5cxDEVJb0GNDpCZJ0cgd2sAguR2PA0gvb1U3otmbkbDKphhrF+aJUuUQ04MsJnKFCSxlkSi0iUqxKdmaCMsKyknuUhLnuiRF22nQIQpjPRkzLzQ5bA4Ra8v1Gamh5NUdiZ3hc2uFWCg84gIeIxRBpqBTOO7wlH1BvF6pLvh7IAk5WkpsKO6W4FdAgMBAAGjggH2MIIB8jAPBgNVHRMBAf8EBTADAQEAMA4GA1UdDwEB/wQEAwIGwDAqBgNVHSUEIzAhBggrBgEFBQcDBAYKKwYBBAGCNwoDDAYJKoZIhvcvAQEFMBMGA1UdIwQMMAqACEMON1fpJ9kIMBEGA1UdDgQKBAhPkdj+7EZugDAlBgNVHREEHjAcgRpkbmF5YWtAZGlnaXRhbGluZGlhLmdvdi5pbjBHBgNVHR8EQDA+MDygOqA4hjZodHRwOi8vY3JsLnNhZmVzY3J5cHQuY29tL1NhZmVTY3J5cHRSQ0FJQ2xhc3MyMjAxNC5jcmwwgZYGCCsGAQUFBwEBBIGJMIGGMFwGCCsGAQUFBzAChlBodHRwczovL3d3dy5zYWZlc2NyeXB0LmNvbS9kcnVwYWwvZG93bmxvYWQvU2FmZVNjcnlwdFN1Yi1DQWZvclJDQUlDbGFzczIyMDE0LmNlcjAmBggrBgEFBQcwAYYaaHR0cDovL29jc3Auc2FmZXNjcnlwdC5jb20wcgYDVR0gBGswaTAhBgZggmRkAgIwFzAVBggrBgEFBQcCAjAJGgdDbGFzcyAyMEQGBmCCZGQKATA6MDgGCCsGAQUFBwICMCwaKk9yZ2FuaXphdGlvbmFsIERvY3VtZW50IFNpZ25lciBDZXJ0aWZpY2F0ZTANBgkqhkiG9w0BAQsFAAOCAQEAT1EpD83VHv3iDJ+5IqNDqqPYJW3V2BNMYOdxMO7tDtve2N8iF55c+XCCWHbh+MHqz5fhuOK1icCoN2zhDtZRx08x2yVhlf0acaPwz77xoRKZAzguBFSSuUKyy2Uz9loDNDSavsq3aMynT0TUvTOMC7nQu8wJJqsWZC+aUQomkQfHozJHXTm4eaTqCXLL1xtlD2oB3zPdPQMgHvo2t6Y5mHA7qhlCpPnW5anWuyPlHKVtBynFxIOVpu39vJgh+Lavmec0LOWowkd2QGyjvGDozAnOKmNPM0e2VCP4Nnllh9YzcZ+DjKcyc+xsRf2mqPImAC6rzm7Rd2Btq4fb9+xgNQ==\\\",\\\"details\\\":{\\\"version\\\":2,\\\"subject\\\":{\\\"countryName\\\":\\\"IN\\\",\\\"organizationName\\\":\\\"NATIONAL E-GOVERNANCE DIVISION\\\",\\\"organizationalUnitName\\\":\\\"MINISTRY OF ELECTRONICS AND INFORMATION TECHNOLOGY\\\",\\\"postalCode\\\":\\\"110003\\\",\\\"stateOrProvinceName\\\":\\\"Delhi\\\",\\\"streetAddress\\\":\\\"ELECTRONICS NIKETAN,CGO COMPLEX,NEW DELHI,NEW DELHI\\\",\\\"houseIdentifier\\\":\\\"6 CGO COMPLEX\\\",\\\"serialNumber\\\":\\\"0f30cb67333f634db11fdb41868b343a6f0469581bdb9fd2a2e4cb59e7797a43\\\",\\\"commonName\\\":\\\"DS NATIONAL E-GOVERNANCE DIVISION\\\"},\\\"issuer\\\":{\\\"countryName\\\":\\\"IN\\\",\\\"organizationName\\\":\\\"Sify Technologies Limited\\\",\\\"organizationalUnitName\\\":\\\"Sub-CA\\\",\\\"commonName\\\":\\\"SafeScrypt sub-CA for RCAI Class 2 2014\\\"},\\\"serial\\\":\\\"261E807D01019468\\\",\\\"notBefore\\\":\\\"2020-09-05T07:18:29.000Z\\\",\\\"notAfter\\\":\\\"2023-09-05T07:18:29.000Z\\\",\\\"subjectHash\\\":\\\"e1ae3745\\\",\\\"signatureAlgorithm\\\":\\\"sha256WithRSAEncryption\\\",\\\"fingerPrint\\\":\\\"C6:E0:A7:16:D3:CC:BE:4F:CF:06:46:92:ED:9D:6B:C5:5E:4C:E8:E9\\\",\\\"publicKey\\\":{\\\"algorithm\\\":\\\"sha256WithRSAEncryption\\\"},\\\"altNames\\\":[],\\\"extensions\\\":{\\\"basicConstraints\\\":\\\"CA:FALSE\\\",\\\"keyUsage\\\":\\\"Digital Signature, Non Repudiation\\\",\\\"extendedKeyUsage\\\":\\\"E-mail Protection, 1.3.6.1.4.1.311.10.3.12, 1.2.840.113583.1.1.5\\\",\\\"authorityKeyIdentifier\\\":\\\"keyid:43:0E:37:57:E9:27:D9:08\\\",\\\"subjectKeyIdentifier\\\":\\\"4F:91:D8:FE:EC:46:6E:80\\\",\\\"subjectAlternativeName\\\":\\\"email:dnayak@digitalindia.gov.in\\\",\\\"cRLDistributionPoints\\\":\\\"Full Name:\\\\n  URI:http://crl.safescrypt.com/SafeScryptRCAIClass22014.crl\\\",\\\"authorityInformationAccess\\\":\\\"CA Issuers - URI:https://www.safescrypt.com/drupal/download/SafeScryptSub-CAforRCAIClass22014.cer\\\\nOCSP - URI:http://ocsp.safescrypt.com\\\",\\\"certificatePolicies\\\":\\\"Policy: 2.16.356.100.2.2\\\\n  User Notice:\\\\n    Explicit Text: Class 2\\\\nPolicy: 2.16.356.100.10.1\\\\n  User Notice:\\\\n    Explicit Text: Organizational Document Signer Certificate\\\"}},\\\"validAadhaarDSC\\\":\\\"yes\\\"},\\\"address\\\":\\\"D/O B SUNDARARAJAN NO 4/2 C RAMACHANDRAPURAM 2ND LANE SOUTH VELI STREET MADURAI CENTER MADURAI TAMIL NADU 625001\\\",\\\"photo\\\":\\\"https://persist.signzy.tech/api/files/409451833/download/bcd9240d58f24928a796b0eedab177565e0c56160bba4fa8817c1f661fe8cb77.jpeg\\\",\\\"splitAddress\\\":{\\\"district\\\":[\\\"MADURAI\\\"],\\\"state\\\":[[\\\"TAMIL NADU\\\",\\\"TN\\\"]],\\\"city\\\":[\\\"MADURAI SOUTH\\\"],\\\"pincode\\\":\\\"625001\\\",\\\"country\\\":[\\\"IN\\\",\\\"IND\\\",\\\"INDIA\\\"],\\\"addressLine\\\":\\\"D/O B SUNDARARAJAN NO 4/2 C RAMACHANDRAPURAM 2ND LANE CENTER SOUTH VELI STREET\\\",\\\"landMark\\\":\\\"\\\"}}}\",\"RefId\":\"Ref_807746\",\"id\":null,\"userId\":null,\"patronId\":null,\"getres\":null}";
            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json(0);
            }
            JsonRes aaResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            userAadharJsonResponse aaResponse1 = JsonSerializer.Deserialize<userAadharJsonResponse>(aaResponse.ResponseContent.ToString());

            if (aaResponse.StatusCode == "000")
            {
                HttpContext.Session.Remove("RefId");
                HttpContext.Session.SetString("RefId", aaResponse.refId);

                ViewBag.data = aaResponse.StatusCode + "|" + aaResponse.Status;
                return Json(aaResponse1);

            }
            else if (aaResponse.StatusCode == "001")
            {
                ViewBag.data = aaResponse.StatusCode + "|" + aaResponse.Status;
                return Json(1);
            }
            else
            {

                ViewBag.data = aaResponse.StatusCode + "|" + aaResponse.Status;
                return Json(0);
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult NewReg(string emailId, string mailOTP, string aadhaarNo, string lstrMailOtp)
        {
            string lstrnumotp = string.Empty;
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrTime = string.Empty;
            string lstrTime1 = string.Empty;

            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            JsonRes objResponse = new JsonRes();

            //if (lstrMailOtp == null)
            //{
            //    // ViewBag.data = ResponseCode.Invalid_OTP + "|" + ResponseMsg.Invalid_OTP;
            //    return Json(0);
            //}



            ///*  else*/
            //if ((DateTime.Now - Convert.ToDateTime(HttpContext.Session.GetString("MailTime"))).TotalSeconds > 900)
            //{
            //    // ViewBag.data = ResponseCode.Timeout_for_mailotp + "|" + ResponseMsg.Timeout_for_mailotp;
            //    return Json(0);
            //}

            //else if (mailOTP != lstrMailOtp)
            //{
            //    //  ViewBag.data = ResponseCode.Invalid_OTP_for_EmailID + "|" + ResponseMsg.Invalid_EOTP;
            //    return Json(0);
            //}
            //else
            //{
            try
            {
                //string referenceId = HttpContext.Session.GetString("RefId").ToString();


                requestBody.Add("EmailId", emailId);
                // requestBody.Add("Refid", referenceId);
                requestBody.Add("mailOTP", mailOTP);
                requestBody.Add("AadhaarNo", aadhaarNo);
                requestBody.Add("onboardType", "EKYC");
                lstrData = JsonSerializer.Serialize(requestBody);


                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.DMTURL);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);



                var stringContent = new StringContent(JsonSerializer.Serialize(aadhaarNo), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.CheckuniqueId, stringContent).Result;
                string responsestring = JsonSerializer.Serialize(responseMessage);

                if (string.IsNullOrEmpty(responsestring))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return Json(0);
                }
                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.data = " ";

                    return Json(0);

                }

                else
                {
                    ViewBag.data = "Failed to send link";

                    return Json(0);
                }

                // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoardingLink/Verify", "", lstrData, "application/json");

                if (string.IsNullOrEmpty(lstrReturn))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return Json(1);
                }

                objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
                if (objResponse.StatusCode == "000")
                {
                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(0);
                }
                //send mail
                else
                {
                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(1);
                }
            }
            catch (Exception ex)
            {
                //  objLog.WriteErrorLog("AgentDashboard: Exception : " + ex.ToString(), lstrFolderName);
                return Json(0);
            }
            finally
            {
                // objLog = null;
                objResponse = null;
            }
            //return View();
            //}
        }

    }
}









