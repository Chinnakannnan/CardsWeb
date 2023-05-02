using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYPAY.Models;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;

namespace PPICards.Controllers
{
    [Authorize]
    public class OnboardingController : Controller
    {
        public IActionResult Onboarding()
        {
            return View();
        }


        public string lstrFolderName = "ShopKYCSupport";
        private readonly IConfiguration Configuration;
        public string DMTURL;
        public OnboardingController(IConfiguration _configuration)
        {

            Configuration = _configuration;
            DMTURL = Configuration.GetSection("appSettings")["DMTURL"];

        }





        public IActionResult OnBoardingView()
        {
            return View();
        }


        public IActionResult SendLink()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendLink(sendLink objRequest)
        {
            return View("SendLink");
        }











        //public ActionResult Adminview(string CustomerId)
        //        {
        //            kycgetList obj = new kycgetList();

        //            obj =JsonSerializer.Deserialize<kycgetList>(CustomerId);
        //            return View("Adminview", obj);
        //        }



        public ActionResult OnBoardactive()
        {

            JsonRes objResponse = new JsonRes();
            JsonReq objres = new JsonReq();

            kyc objRequest = new kyc();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            string lstrReturn = string.Empty;
            string lstrReturndata = string.Empty;
            string lstrData = string.Empty;
            string lstrConsumerSecret = string.Empty;
            string lstrAPIKEY = string.Empty;
            try
            {


                lstrConsumerSecret = Configuration.GetSection("appSettings")["APIConsumerSecret"];

                lstrAPIKEY = Configuration.GetSection("appSettings")["APIConsumerKey"];

                lstrData = JsonSerializer.Serialize(requestBody);
                //lstrReturn = objPost.GETData("GET", DMTURL + "OnBoarding/OnboardList", lstrAPIKEY, lstrData, "application/json");
                //   lstrReturn = objPost.GETData("GET", DMTURL + "OnBoarding/OnboardList", lstrAPIKEY, lstrData, "application/json");
                objres = JsonSerializer.Deserialize<JsonReq>(lstrReturn);
                if (objres.StatusCode != "000")
                {
                    ViewBag.data = objres.StatusCode + "|" + objres.Status;
                    return View();
                }
                else
                {
                    IList<kycgetList> objReport = new List<kycgetList>();
                    objReport = JsonSerializer.Deserialize<IList<kycgetList>>(objres.ResponseContent.ToString());
                    TempData["flag1"] = objReport;
                    IList<kycgetList> objreq = new List<kycgetList>();
                    objreq = JsonSerializer.Deserialize<IList<kycgetList>>(objres.ResponseContent1.ToString());
                    TempData["flag0"] = objreq;




                    return View();

                }


            }
            catch (Exception ex)
            {
                //objLog.WriteErrorLog("ViewLimit: Exception : " + ex.ToString(), lstrFolderName);
                return View();
            }
            finally
            {
                //objLog = null;
                objRequest = null;
            }
        }








        public void GetShopDetails()
        {

            JsonRes objResponse = new JsonRes();
            kyc objRequest = new kyc();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrConsumerSecret = string.Empty;
            string lstrAPIKEY = string.Empty;
            try
            {

                lstrConsumerSecret = Configuration.GetSection("appSettings")["APIConsumerSecret"];
                lstrAPIKEY = Configuration.GetSection("appSettings")["APIConsumerKey"];
                // objRequest.CompanyID = Crypto.AES_DECRYPT(Session["custid"].ToString(), COMMON.KEY);

                //requestBody.Add("shopNumber", objRequest.shopNumber);


                lstrData = JsonSerializer.Serialize(requestBody);

                //lstrReturn = objPost.GETData("GET", DMTURL + "OnBoarding/GetShopDetails", lstrAPIKEY, lstrData, "application/json");


                if (string.IsNullOrEmpty(lstrReturn))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                }

                objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);


                if (objResponse.StatusCode != "000")
                {
                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                }
                else
                {

                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    IList<kycgetList> objReport = new List<kycgetList>();
                    //objResponse= System.IO.Path.GetFileName(objResponse).ToString();
                    objReport = JsonSerializer.Deserialize<IList<kycgetList>>(objResponse.ResponseContent.ToString());
                    TempData["UserDtls"] = objReport;


                }

            }
            catch (Exception ex)
            {
                // objLog.WriteErrorLog("ViewLimit: Exception : " + ex.ToString(), lstrFolderName);
            }
            finally
            {
                //  objLog = null;
                objRequest = null;
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult AadhaarValidation(string aadhaarNo)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            // kyc objreq = new kyc();

            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();
            requestBody.Add("aadhaarNo", aadhaarNo);

            lstrData = JsonSerializer.Serialize(requestBody);
            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/AadhaarLogin", "", lstrData, "application/json");
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

            //            var client = new RestClient("https://test.zoop.one/in/identity/okyc/otp/request");
            //            client.Timeout = -1;
            //            var request = new RestRequest(Method.POST);
            //            request.AddHeader("api-key", "FJVTS74-RAAM91G-P2Y0DHG-3Z1QGW9");
            //            request.AddHeader("app-id", "62c7df712d4a96001d8dfb23");
            //            request.AddHeader("Content-Type", "application/json");
            //            var body = @"{" + "\n" + @"    ""data"": {" + "\n" + @"  ""customer_aadhaar_number"": ""430076336614""," + "\n" +
            //            @"""consent"": ""Y""," + "\n" +@" ""consent_text"": ""I hear by declare my consent agreement for fetching my information via ZOOP API.""
            //" + "\n" + @"    }" + "\n" +@"}";
            //            request.AddParameter("application/json", body, ParameterType.RequestBody);
            //            IRestResponse response = client.Execute(request);
            //            Console.WriteLine(response.Content);





            //var client = new RestClient("https://test.zoop.one/");
            //   RestRequest request = new RestRequest("in/identity/okyc/otp/request", Method.Post);
            //   var body = @"{" + "\n" + @"    ""data"": {" + "\n" + @"  ""customer_aadhaar_number"": ""430076336614""," + "\n" +
            //              @"""consent"": ""Y""," + "\n" +@" ""consent_text"": ""I hear by declare my consent agreement for fetching my information via ZOOP API.""
            //  " + "\n" + @"    }" + "\n" +@"}";
            //   request.AddHeader("Accept", "application/json");
            //   request.AddHeader("api-key", "FJVTS74-RAAM91G-P2Y0DHG-3Z1QGW9");
            //              request.AddHeader("app-id", "62c7df712d4a96001d8dfb23");
            //   request.AddHeader("Content-Type", "application/json");
            //   request.AddParameter("application/json", body, ParameterType.RequestBody);
            //   RestResponse response = client.Execute(request);



        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetAadhaar(string requestId, string OTP, string emailId)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            //Dictionary<string, object> requestBody = new Dictionary<string, object>();


            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();

            requestBody.Add("requestId", requestId);
            requestBody.Add("OTP", OTP);
            requestBody.Add("emailId", emailId);

            lstrData = JsonSerializer.Serialize(requestBody);
            //lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/GetAadhaar", "", lstrData, "application/json");
            // lstrReturn = "{\"StatusCode\":\"000\",\"Status\":\"Success\",\"CustomerId\":null,\"ResponseContent\":\"{\\\"essentials\\\":{\\\"requestId\\\":\\\"6315c8ec7a490fa69e46388b\\\"},\\\"id\\\":\\\"6315c952a341350e797bc64f\\\",\\\"patronId\\\":\\\"62e8d589f58b0e25e195a290\\\",\\\"task\\\":\\\"getEadhaar\\\",\\\"result\\\":{\\\"name\\\":\\\"S CYNTHIA\\\",\\\"uid\\\":\\\"xxxxxxxx5572\\\",\\\"dob\\\":\\\"12/08/1998\\\",\\\"gender\\\":\\\"FEMALE\\\",\\\"x509Data\\\":{\\\"subjectName\\\":\\\"DS NATIONAL E-GOVERNANCE DIVISION\\\",\\\"certificate\\\":\\\"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\\\",\\\"details\\\":{\\\"version\\\":2,\\\"subject\\\":{\\\"countryName\\\":\\\"IN\\\",\\\"organizationName\\\":\\\"NATIONAL E-GOVERNANCE DIVISION\\\",\\\"organizationalUnitName\\\":\\\"MINISTRY OF ELECTRONICS AND INFORMATION TECHNOLOGY\\\",\\\"postalCode\\\":\\\"110003\\\",\\\"stateOrProvinceName\\\":\\\"Delhi\\\",\\\"streetAddress\\\":\\\"ELECTRONICS NIKETAN,CGO COMPLEX,NEW DELHI,NEW DELHI\\\",\\\"houseIdentifier\\\":\\\"6 CGO COMPLEX\\\",\\\"serialNumber\\\":\\\"0f30cb67333f634db11fdb41868b343a6f0469581bdb9fd2a2e4cb59e7797a43\\\",\\\"commonName\\\":\\\"DS NATIONAL E-GOVERNANCE DIVISION\\\"},\\\"issuer\\\":{\\\"countryName\\\":\\\"IN\\\",\\\"organizationName\\\":\\\"Sify Technologies Limited\\\",\\\"organizationalUnitName\\\":\\\"Sub-CA\\\",\\\"commonName\\\":\\\"SafeScrypt sub-CA for RCAI Class 2 2014\\\"},\\\"serial\\\":\\\"261E807D01019468\\\",\\\"notBefore\\\":\\\"2020-09-05T07:18:29.000Z\\\",\\\"notAfter\\\":\\\"2023-09-05T07:18:29.000Z\\\",\\\"subjectHash\\\":\\\"e1ae3745\\\",\\\"signatureAlgorithm\\\":\\\"sha256WithRSAEncryption\\\",\\\"fingerPrint\\\":\\\"C6:E0:A7:16:D3:CC:BE:4F:CF:06:46:92:ED:9D:6B:C5:5E:4C:E8:E9\\\",\\\"publicKey\\\":{\\\"algorithm\\\":\\\"sha256WithRSAEncryption\\\"},\\\"altNames\\\":[],\\\"extensions\\\":{\\\"basicConstraints\\\":\\\"CA:FALSE\\\",\\\"keyUsage\\\":\\\"Digital Signature, Non Repudiation\\\",\\\"extendedKeyUsage\\\":\\\"E-mail Protection, 1.3.6.1.4.1.311.10.3.12, 1.2.840.113583.1.1.5\\\",\\\"authorityKeyIdentifier\\\":\\\"keyid:43:0E:37:57:E9:27:D9:08\\\",\\\"subjectKeyIdentifier\\\":\\\"4F:91:D8:FE:EC:46:6E:80\\\",\\\"subjectAlternativeName\\\":\\\"email:dnayak@digitalindia.gov.in\\\",\\\"cRLDistributionPoints\\\":\\\"Full Name:\\\\n  URI:http://crl.safescrypt.com/SafeScryptRCAIClass22014.crl\\\",\\\"authorityInformationAccess\\\":\\\"CA Issuers - URI:https://www.safescrypt.com/drupal/download/SafeScryptSub-CAforRCAIClass22014.cer\\\\nOCSP - URI:http://ocsp.safescrypt.com\\\",\\\"certificatePolicies\\\":\\\"Policy: 2.16.356.100.2.2\\\\n  User Notice:\\\\n    Explicit Text: Class 2\\\\nPolicy: 2.16.356.100.10.1\\\\n  User Notice:\\\\n    Explicit Text: Organizational Document Signer Certificate\\\"}},\\\"validAadhaarDSC\\\":\\\"yes\\\"},\\\"address\\\":\\\"D/O B SUNDARARAJAN NO 4/2 C RAMACHANDRAPURAM 2ND LANE SOUTH VELI STREET MADURAI CENTER MADURAI TAMIL NADU 625001\\\",\\\"photo\\\":\\\"https://persist.signzy.tech/api/files/409451833/download/bcd9240d58f24928a796b0eedab177565e0c56160bba4fa8817c1f661fe8cb77.jpeg\\\",\\\"splitAddress\\\":{\\\"district\\\":[\\\"MADURAI\\\"],\\\"state\\\":[[\\\"TAMIL NADU\\\",\\\"TN\\\"]],\\\"city\\\":[\\\"MADURAI SOUTH\\\"],\\\"pincode\\\":\\\"625001\\\",\\\"country\\\":[\\\"IN\\\",\\\"IND\\\",\\\"INDIA\\\"],\\\"addressLine\\\":\\\"D/O B SUNDARARAJAN NO 4/2 C RAMACHANDRAPURAM 2ND LANE CENTER SOUTH VELI STREET\\\",\\\"landMark\\\":\\\"\\\"}}}\",\"RefId\":\"Ref_807746\",\"id\":null,\"userId\":null,\"patronId\":null,\"getres\":null}";
            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json(0);
            }
            JsonRes aaResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            userAadharJsonResponse aaResponse1 = JsonSerializer.Deserialize<userAadharJsonResponse>(aaResponse.ResponseContent.ToString());

            if (aaResponse.StatusCode == "000")
            {

                HttpContext.Session.Remove("RefId"); ;

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
        public JsonResult NewReg(string emailId, string mailOTP, string aadhaarNo, string lstrMailOtp, string lstrMailTime)
        {




            string lstrnumotp = string.Empty;
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrTime = string.Empty;
            string lstrTime1 = string.Empty;

            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            JsonRes objResponse = new JsonRes();
            //object objReturn = null;
            // lstrTime = TempData["mailtime"].ToString();
            // lstrTime1 =
            //  if (TempData["OTPmail"] == null) /*|| Session["MobOTP"] == null*/
            if (lstrMailOtp == null)
            {
                // ViewBag.data = ResponseCode.Invalid_OTP + "|" + ResponseMsg.Invalid_OTP;
                return Json(0);
            }
            //  lstrmailotp = TempData["OTPmail"].ToString();



            else if ((DateTime.Now - Convert.ToDateTime(HttpContext.Session.GetString("MailTime").ToString())).TotalSeconds > 900)
            {
                ViewBag.data = ResponseCode.Timeout_for_mailotp + "|" + ResponseMsg.Timeout_for_mailotp;
                return Json(0);
            }

            else if (mailOTP != lstrMailOtp)
            {
                //  ViewBag.data = ResponseCode.Invalid_OTP_for_EmailID + "|" + ResponseMsg.Invalid_EOTP;
                return Json(0);
            }
            else
            {
                try
                {

                    string referenceId = HttpContext.Session.GetString("RefId").ToString();


                    requestBody.Add("EmailId", emailId);
                    requestBody.Add("Refid", referenceId);
                    requestBody.Add("mailOTP", mailOTP);
                    requestBody.Add("AadhaarNo", aadhaarNo);
                    requestBody.Add("onboardType", "Support");

                    lstrData = JsonSerializer.Serialize(requestBody);


                    // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/Verify", "", lstrData, "application/json");

                    if (string.IsNullOrEmpty(lstrReturn))
                    {
                        ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                        return Json(0);
                    }

                    objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
                    if (objResponse.StatusCode != "000")
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
                    // objLog.WriteErrorLog("AgentDashboard: Exception : " + ex.ToString(), lstrFolderName);
                    return Json(0);
                }
                finally
                {
                    // objLog = null;
                    objResponse = null;
                }
                //return View();
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult EmailOTP(string emailId)
        {
            string lstrReturn = string.Empty;

            string lstrData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();


            kyc objRequest = new kyc();
            //string otp = string.Empty;
            JsonRes objResponse = new JsonRes();

            HttpContext.Session.SetString("MailTime", DateTime.Now.ToString());
            //string mailTime = DateTime.Now.ToString();
            objRequest.emailId = emailId;
            string lstrMailOtp = utility.GetStan();
            //  otp = utility.GetStan();
            // TempData["OTPmail"] = otp;


            requestBody.Add("emailId", emailId);
            requestBody.Add("mailOTP", lstrMailOtp);
            // requestBody.Add("mailTime", mailTime);
            lstrData = JsonSerializer.Serialize(requestBody);

            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/testing", "", lstrData, "application/json");
            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/SendingMail", "", lstrData, "application/json");      real data

            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json("Please try again");
            }
            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            if (objResponse.StatusCode != "000")
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json("Enter valid email address");
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                // return Json("OTP sent to your Registered MailID :" + emailId);
                return Json(requestBody);
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult MobileOTP(string mobileNo)
        {


            string lstrReturn = string.Empty;
            string lstrData = string.Empty;

            string otp = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            kyc obj = new kyc();
            //TempData["mobtime"] = DateTime.Now;
            //string mobileOtp = DateTime.Now.ToString();
            JsonRes objResponse = new JsonRes();
            otp = utility.GetStan();

            HttpContext.Session.SetString("MobTime", DateTime.Now.ToString());

            string num = "91" + mobileNo;

            requestBody.Add("mobileNo", num);
            requestBody.Add("mobOTP", otp);

            lstrData = JsonSerializer.Serialize(requestBody);

            //  lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/SendMobOTP", "", lstrData, "application/json");
            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/SendMobOTP", "", lstrData, "application/json");

            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json("Please try again");
            }
            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            if (objResponse.StatusCode != "000")
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json("Enter valid MobileNumber");
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                //return Json("OTP sent to your Registred Mobile :" + mobileNo);
                return Json(requestBody);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetbyRefId(string refId)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string aadharData = string.Empty;
            string aadharno = string.Empty;
            string panno = string.Empty;
            string gstno = string.Empty;
            string email = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();



            JsonRes objResponse = new JsonRes();


            string lstrAPIKEY = crypto.AES_DECRYPT(HttpContext.Session.GetString("consumerkey").ToString(), COMMON.KEY);


            requestBody.Add("refId", refId);


            lstrData = JsonSerializer.Serialize(requestBody);


            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/GotoRef", lstrAPIKEY, lstrData, "application/json");

            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json(1);
            }
            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);

            if (objResponse.Flag == "0")
            {
                IList<GetJsonResponse> objReport = new List<GetJsonResponse>();

                objReport = JsonSerializer.Deserialize<IList<GetJsonResponse>>(objResponse.ResponseContent.ToString());
                foreach (var item in objReport)
                {
                    aadharData = item.Aadhaar_Details;
                    panno = item.PanNo;
                    aadharno = item.AadhaarNo;
                    gstno = item.GstNo;
                    email = item.EmailId;

                    HttpContext.Session.Remove("RefId");
                    HttpContext.Session.SetString("RefId", item.ReferId);
                }


                lstrData = crypto.AES_DECRYPT(aadharData, Configuration.GetSection("appSettings")["APIAESKEY"]);


                userAadharJsonResponse Response = JsonSerializer.Deserialize<userAadharJsonResponse>(lstrData);
                Response.aadhaarNo = aadharno;
                Response.pancardNo = panno;
                Response.gstNo = gstno;
                Response.emailId = email;
                Response.flag = "0";




                if (objResponse.StatusCode == "000")
                {

                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(Response);

                }

                else
                {

                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(0);
                }
            }
            else if (objResponse.Flag == "1")

            {

                IList<kycget> objReport = new List<kycget>();
                //objResponse.ResponseContent

                objReport = JsonSerializer.Deserialize<IList<kycget>>(objResponse.ResponseContent.ToString());

                objReport[0].flag = "1";



                if (objResponse.StatusCode == "000")
                {

                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(objReport[0]);

                }

                else
                {

                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(0);
                }
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json(0);
            }
        }


        //[HttpPost, ValidateAntiForgeryToken]
        //public JsonResult DeleteShop(string customerId)
        //{
        //    HTTPPOST objPost = new HTTPPOST();
        //    Log objLog = new Log();
        //    string lstrReturn = string.Empty;
        //    string lstrData = string.Empty;
        //    Dictionary<string, string> requestBody = new Dictionary<string, string>();

        //    JsonRes objResponse = new JsonRes();
        //    requestBody.Add("CustomerId", customerId);
        //    lstrData = JsonSerializer.Serialize(requestBody);

        //    lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/DeleteShop", "", lstrData, "application/json");
        //    if (string.IsNullOrEmpty(lstrReturn))
        //    {
        //        TempData["My"] = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
        //        //return View("ListShop");
        //    }

        //    objResponse =JsonSerializer.Deserialize<JsonRes>(lstrReturn);

        //    Directory.Delete(Server.MapPath("~/OnBoarding/" + objResponse.CustomerId), true);


        //    if (objResponse.StatusCode != "000")
        //    {
        //        TempData["My"] = objResponse.StatusCode + "|" + objResponse.Status;
        //        return Json("Failed");

        //    }
        //    else
        //    {
        //        TempData["My"] = objResponse.StatusCode + "|" + objResponse.Status;
        //        return Json("success");
        //    }



        //}
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult PanValidation(string pancardNo)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrPanData = string.Empty;
            string lstrRespData = string.Empty;
            string lstrtemp = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();


            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();

            requestBody.Add("pancardNo", pancardNo);
            lstrData = JsonSerializer.Serialize(requestBody);
            //  lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/PanValidation", "", lstrData, "application/json");
            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json(0);
            }
            else
            {
                lstrtemp = savePanData(pancardNo, lstrReturn);
            }

            if (lstrtemp == "0")
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json(0);
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json(1);
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public string savePanData(string pancardNo, string lstrReturn)
        {

            string lstrData = string.Empty;
            string lstrPanData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();


            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();


            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            lstrPanData = crypto.AES_ENCRYPT(lstrReturn, Configuration.GetSection("appSettings")["APIAESKEY"]);

            string referenceId = HttpContext.Session.GetString("RefId").ToString();
            requestBody.Add("pancardNo", pancardNo);
            requestBody.Add("refId", referenceId);
            requestBody.Add("panDetails", lstrPanData);


            lstrData = JsonSerializer.Serialize(requestBody);

            //lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/SavePanDetails", "", lstrData, "application/json");
            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            if (objResponse.StatusCode != "000")
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return "0";
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return "1";
            }
        }


        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GstValidation(string gstNo)
        {
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrPanData = string.Empty;
            string lstrRespData = string.Empty;
            string lstrtemp = string.Empty;

            Dictionary<string, string> requestBody = new Dictionary<string, string>();


            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();

            requestBody.Add("gstNo", gstNo);
            lstrData = JsonSerializer.Serialize(requestBody);
            //lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/GstValidation", "", lstrData, "application/json");
            if (string.IsNullOrEmpty(lstrReturn))
            {
                return Json(0);
            }
            else
            {


                lstrtemp = saveGstData(gstNo, lstrReturn);
            }

            if (lstrtemp == "0")
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json(0);
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return Json(1);
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public string saveGstData(string gstNo, string lstrReturn)
        {

            string lstrData = string.Empty;
            string lstrgstData = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();


            string otp = string.Empty;
            JsonRes objResponse = new JsonRes();

            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            lstrgstData = crypto.AES_ENCRYPT(lstrReturn, Configuration.GetSection("appSettings")["APIAESKEY"]);

            string referenceId = HttpContext.Session.GetString("RefId").ToString();
            requestBody.Add("gstNo", gstNo);
            requestBody.Add("refId", referenceId);
            requestBody.Add("gstDetails", lstrgstData);


            lstrData = JsonSerializer.Serialize(requestBody);

            // lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/SaveGstDetails", "", lstrData, "application/json");
            objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
            if (objResponse.StatusCode != "000")
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return "0";
            }
            else
            {
                ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                return "1";
            }
        }





        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult NewRegistration(FormFile PanSoftcopy, FormFile gstSoftcopy,
                FormFile aadhaarfrontSoftcopy, FormFile aadharbackSoftcopy, string data)
        //public JsonResult NewRegistration(string name,
        //                   string shopNumber,
        //                   string shopName,
        //                   string addressLine1,
        //                   string addressLine2,
        //                   string city,
        //                   string state,
        //                   string pincode,
        //                   string country,
        //                   string gstNo,
        //                   string pancardNo,
        //                   string mobileNo,
        //                   string mobile2,
        //               string  mobOTP,
        //               string     usertype,
        //               string    refId,
        //               string     mobileOtp,
        //               string ipAddress ,string data)


        {

            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrnumotp = string.Empty;
            string Istrreturn2 = string.Empty;
            string lstrAPIKEY = string.Empty;
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            JsonRes objResponse = new JsonRes();
            // declare images and add it to the request body

            string pan = "";
            string adf = "";
            string adb = "";
            string gst = "";
            string GSTImg = "";
            string PanImg = "";
            string AdFImg = "";
            string AdBImg = "";
            kyc formData = JsonSerializer.Deserialize<kyc>(data);
            //formData.panSoftcopy = PanSoftcopy;
            //formData.aadhaarfrontSoftcopy = aadhaarfrontSoftcopy;
            //formData.aadharbackSoftcopy = aadharbackSoftcopy;
            //formData.gstSoftcopy = gstSoftcopy;

            if (formData.mobileOtp == null)
            {
                //  ViewBag.data = ResponseCode.Invalid_OTP + "|" + ResponseMsg.Invalid_OTP;
                return Json(0);
            }

            //lstrnumotp = TempData["OTPMob"].ToString();
            if ((DateTime.Now - Convert.ToDateTime(HttpContext.Session.GetString("MobTime").ToString())).TotalSeconds > 900)
            {
                // ViewBag.data = ResponseCode.Timeout_for_mobotp + "|" + ResponseMsg.Timeout_for_mobotp;
                return Json(0);
            }



            if (formData.mobOTP != formData.mobileOtp)
            {
                //ViewBag.data = ResponseCode.Invalid_OTP_for_Mobile + "|" + ResponseMsg.Invalid_MOTP;
                return Json(11);
            }
            //if (Request.Files.Count > 0)
            //{
            //    // as you wish
            //}




            try
            {
                //if (formData.panSoftcopy != null)

                //{

                //    byte[] panupl = new byte[formData.panSoftcopy.Length];
                //    formData.panSoftcopy.OpenReadStream();

                //    pan = Convert.ToBase64String(panupl);

                //    PanImg = Path.GetFileName(formData.panSoftcopy.FileName);

                //}
                //else pan = null;
                //if (formData.aadhaarfrontSoftcopy != null)
                //{
                //    byte[] aadafr = new byte[formData.aadhaarfrontSoftcopy.InputStream.Length];
                //    formData.aadhaarfrontSoftcopy.InputStream.Read(aadafr, 0, aadafr.Length);
                //    adf = Convert.ToBase64String(aadafr);
                //    AdFImg = Path.GetFileName(formData.aadhaarfrontSoftcopy.FileName);

                //}
                //else adf = null;
                //if (formData.aadharbackSoftcopy != null)
                //{


                //    byte[] aadaba = new byte[formData.aadharbackSoftcopy.InputStream.Length];
                //    formData.aadharbackSoftcopy.InputStream.Read(aadaba, 0, aadaba.Length);
                //    adb = Convert.ToBase64String(aadaba);
                //    AdBImg = Path.GetFileName(formData.aadharbackSoftcopy.FileName);

                //}
                //else adb = null;
                //if (formData.gstSoftcopy != null)
                //{
                //    byte[] gstupl = new byte[formData.gstSoftcopy.InputStream.Length];
                //    formData.gstSoftcopy.InputStream.Read(gstupl, 0, gstupl.Length);
                //    gst = Convert.ToBase64String(gstupl);
                //    GSTImg = Path.GetFileName(formData.gstSoftcopy.FileName);

                //}
                //else gst = null;



                //string file = Server.MapPath("~/OnBoarding/");



                string referenceId = HttpContext.Session.GetString("RefId").ToString();
                requestBody.Add("refId", referenceId);
                requestBody.Add("name", formData.name);
                requestBody.Add("shopNumber", formData.shopNumber);
                requestBody.Add("shopName", formData.shopName);
                requestBody.Add("addressLine1", formData.addressLine1);
                requestBody.Add("addressLine2", formData.addressLine2);
                requestBody.Add("city", formData.city);
                requestBody.Add("state", formData.state);
                requestBody.Add("pincode", formData.pincode);
                requestBody.Add("country", formData.country);
                requestBody.Add("gstNo", formData.gstNo);
                requestBody.Add("gstSoftcopy", gst);
                requestBody.Add("pancardNo", formData.pancardNo);
                requestBody.Add("PanSoftcopy", pan);
                //requestBody.Add("AadhaarNo", objRequest.AadhaarNo);
                requestBody.Add("aadhaarfrontSoftcopy", adf);
                requestBody.Add("aadharbackSoftcopy", adb);
                requestBody.Add("mobileNo", formData.mobileNo);
                requestBody.Add("mobile2", formData.mobile2);
                //requestBody.Add("EmailId", objRequest.EmailId);
                requestBody.Add("usertype", formData.usertype);
                requestBody.Add("mobOTP", formData.mobOTP);

                requestBody.Add("gst_imgName", GSTImg);
                requestBody.Add("pan_imgName", PanImg);
                requestBody.Add("aadharfront_imgName", AdFImg);
                requestBody.Add("aadhaarback_imgName", AdBImg);
                // requestBody.Add("imgPath", file);
                requestBody.Add("onboardType", "Support");
                requestBody.Add("ipAddress", formData.ipAddress);




                lstrData = JsonSerializer.Serialize(requestBody);

                //lstrReturn = objPost.POSTData("POST", DMTURL + "OnBoarding/CreateShop", "", lstrData, "application/json");


                if (string.IsNullOrEmpty(lstrReturn))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return Json(0);
                }

                objResponse = JsonSerializer.Deserialize<JsonRes>(lstrReturn);
                if (objResponse.UserType == "2")
                {
                    requestBody.Add("mapusercode", objResponse.CustomerId);
                    requestBody.Add("mapusertype", "AS");
                    requestBody.Add("customerID", objResponse.CustomerId);


                    lstrData = JsonSerializer.Serialize(requestBody);

                    //  Istrreturn2 = objPost.POSTDatabyKeyHeader("POST", DMTURL + "OnBoarding/CreateUserMapping", lstrAPIKEY, lstrData, "application/json");
                }

                //if (formData.gstSoftcopy != null || formData.panSoftcopy != null
                //    || formData.aadhaarfrontSoftcopy != null || formData.aadharbackSoftcopy != null)
                //{
                //    string folder = Server.MapPath("~/OnBoarding/" + objResponse.CustomerId);
                //    Directory.CreateDirectory(folder);
                //    if (formData.gstSoftcopy != null && formData.gstSoftcopy.Length > 0)
                //    {
                //        string imgname = Path.GetFileName(formData.gstSoftcopy.FileName);
                //        string imgext = Path.GetExtension(imgname);
                //        if (imgext == ".jpg" || imgext == ".png" || imgext == ".pdf")
                //        {

                //            string imgpath = Path.Combine(folder, imgname);
                //            formData.gstSoftcopy.(imgpath);
                //        }
                //    }
                //    if (formData.panSoftcopy != null && formData.panSoftcopy.ContentLength > 0)
                //    {
                //        string panname = Path.GetFileName(formData.panSoftcopy.FileName);
                //        string panext = Path.GetExtension(panname);
                //        if (panext == ".jpg" || panext == ".png" || panext == ".pdf")
                //        {

                //            string panpath = Path.Combine(folder, panname);
                //            formData.panSoftcopy.SaveAs(panpath);
                //        }
                //    }
                //    if (formData.aadhaarfrontSoftcopy != null && formData.aadhaarfrontSoftcopy.ContentLength > 0)
                //    {
                //        string adfname = Path.GetFileName(formData.aadhaarfrontSoftcopy.FileName);
                //        string adfext = Path.GetExtension(adfname);
                //        if (adfext == ".jpg" || adfext == ".png" || adfext == ".pdf")
                //        {

                //            string adfpath = Path.Combine(folder, adfname);
                //            formData.aadhaarfrontSoftcopy.SaveAs(adfpath);
                //        }
                //    }

                //    if (formData.aadharbackSoftcopy != null && formData.aadharbackSoftcopy.ContentLength > 0)
                //    {
                //        string adbname = Path.GetFileName(formData.aadharbackSoftcopy.FileName);
                //        string adbext = Path.GetExtension(adbname);
                //        if (adbext == ".jpg" || adbext == ".png" || adbext == ".pdf")
                //        {

                //            string adbpath = Path.Combine(folder, adbname);
                //            formData.aadharbackSoftcopy.SaveAs(adbpath);
                //        }
                //    }
                //}




                if (objResponse.StatusCode != "000")
                {


                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    return Json(0);
                }
                else
                {

                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status + "|" + objResponse.CustomerId;

                    HttpContext.Session.SetString("customerId", objResponse.CustomerId);
                    TempData["notimsg"] = ("One was added");



                    return Json(1);
                }


            }
            catch (Exception ex)
            {
                // objLog.WriteErrorLog("NewRegistration: Exception : " + ex.ToString(), lstrFolderName);
                return Json(0);
            }
            finally
            {
                //objPost = null;
                // objLog = null;
                objResponse = null;
            }


        }

    }
}
