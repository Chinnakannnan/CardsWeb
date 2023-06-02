using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYPAY.Models;
using NuGet.Common;
using OfficeOpenXml;
using PPICards.API_Service;
using PPICards.Helper;
using PPICards.Models;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;  

namespace PPICards.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public const string errorFolder = "Admin";
        public string lstrFolderName = "ShopKYCSupport";
        private readonly IConfiguration _configuration;
        private readonly IAPIClient _clientService;
       
        public AdminController(IConfiguration configuration, IAPIClient clientServiceInstance) => (_configuration, _clientService) = (configuration, clientServiceInstance);
        public IActionResult Index()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") {  return RedirectToAction("Login", "Login"); }

            if (HttpContext.Session.GetString(ConstValues.LoginName) != null)
            {
                string admin = HttpContext.Session.GetString(ConstValues.LoginName);
                ViewBag.Admin = admin;
            }
            return View();
        }
        public IActionResult UserDetails()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            return View();
        }
        public IActionResult CardDetails()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            return View();
        }
        public IActionResult Fund()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            return View();
        }
        public IActionResult FundReversal()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            return View();
        }
        public IActionResult PaymentRequest()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            return View();
        }
        public IActionResult Transactions()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            int hh = 0;
            hh = hh + 10;
            return View("Transactions");
        }
        public IActionResult PPITransactionReport(TransactionModel objRequest)
        {
            string aesKey = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;

            try
            {
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    if (objRequest.fromdate != null)
                    {
                        string[] splitFromDate = objRequest.fromdate.Split('-');
                        FromDate = splitFromDate[1] + "-" + splitFromDate[2] + "-" + splitFromDate[0];
                    }
                    if (objRequest.todate != null)
                    {
                        string[] splitToDate = objRequest.todate.Split('-');
                        ToDate = splitToDate[1] + "-" + splitToDate[2] + "-" + splitToDate[0];
                    }
                    objRequest.userType = HttpContext.Session.GetString(ConstValues.SessionUserType);
                    objRequest.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);

                    string responsestring = string.Empty;
                    using (HttpResponseMessage responseMessage = _clientService.PPITransactionReport(objRequest, token))
                    {
                        responsestring = JsonSerializer.Serialize(responseMessage);

                        if (string.IsNullOrEmpty(responsestring))
                        {
                            ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                            return View("Transactions");
                        }
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            IList<PayoutHistoryDetails> objReport = new List<PayoutHistoryDetails>();
                            var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                            objReport = JsonSerializer.Deserialize<IList<PayoutHistoryDetails>>(resultValue);
                            TempData["GetTransactionDetails"] = objReport;
                              return View("Transactions"); 
                        }
                    }
                }
                return View("Transactions");
            }
            catch (Exception ex)
            {
                utility.ErrorLog(errorFolder, ex.Message.ToString());
                if (objRequest.userType == "1")
                {
                    return RedirectToAction("Transactions", "Admin");
                }
                else
                {
                    return View("Index");
                }
            }
        }

        public IActionResult AddKitData()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") { return RedirectToAction("Login", "Login"); }
            JsonRes objResponse = new JsonRes();
            kyc objRequest = new kyc();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrConsumerSecret = string.Empty;
            string lstrAPIKEY = string.Empty;
            try
            {
                lstrData = JsonSerializer.Serialize(requestBody);
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.GetAsync(OnboardConstants.GetKitData).Result;
                string responsestring = JsonSerializer.Serialize(responseMessage);

                if (string.IsNullOrEmpty(responsestring))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return View("SendLink");
                }
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                    IList<KitDetails> objReport = new List<KitDetails>();                    
                    objReport = JsonSerializer.Deserialize<IList<KitDetails>>(resultValue);
                    TempData["UserDtls"] = objReport;
                    return View("AddKitData");
                }
                else
                {
                    ViewBag.data = "Failed to send link";
                    return View("AddKitData");
                }
            }

            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return View("AddKitData"); }           

        }
        public IActionResult UploadKitData()
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") {  return RedirectToAction("Login", "Login"); }
            return View();
        }
        public IActionResult AdminView()
        {

            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1") {  return RedirectToAction("Login", "Login"); }
            JsonRes objResponse = new JsonRes();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                HttpResponseMessage responseMessage = _clientService.GetAdminDashboardData(token);
                using(HttpResponseMessage response = responseMessage)
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                        ViewBag.data = objResponse.StatusCode + "|" + objResponse.Status;
                        IList<RegistrationRequest> objReport = new List<RegistrationRequest>();
                        objReport = JsonSerializer.Deserialize<IList<RegistrationRequest>>(resultValue);
                        TempData["UserDtls"] = objReport;
                        return View("UserDetails");
                    }
                    else
                    {
                        ViewBag.data = "Failed to send link";
                        return View("UserDetails");
                    }
                }
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return View("UserDetails"); }

        }
        [HttpGet]
        public JsonResult AssignUserKit()
        {

            JsonRes objResponse = new JsonRes();
            OnboardKycEkycModel objRequest = new OnboardKycEkycModel();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.GetAsync(OnboardConstants.GetKitData).Result;
                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(resultValue)) { return Json(0);}
                IList<KitDetails> objReport = new List<KitDetails>();
                objReport = JsonSerializer.Deserialize<IList<KitDetails>>(resultValue);
                if (objReport.Count == 0){ return Json(0);}
                else { return Json(objReport); }
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(0); }
            
        }
        [HttpPost]
        public JsonResult SetKIT(string KitNo, string CustomerId)
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1")  {  return Json(0); }
            JsonRes objResponse = new JsonRes();
            KitMappingModel objRequest = new KitMappingModel();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);               
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                objRequest.KitReferenceNumber = KitNo;
                objRequest.CustomerId = CustomerId;
                var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.SetKitNo, stringContent).Result;
                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(resultValue)) { return Json(0); }
                if (responseMessage.IsSuccessStatusCode) {  return Json(1);  }
                else { return Json(0); }
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(0); }           
        }
        [HttpPost]
        public IActionResult AddSingleKitData(AddKitCardDetails request)
        { 
            StatusResponseModel statusResponseModel = new StatusResponseModel();
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1")
            {
                statusResponseModel.statuscode = "020";
                statusResponseModel.statusdesc = "UnAuthorized User";
                return Json(statusResponseModel);
            }
            if (request.KitReferenceNumber.Length != 12)
            {
                statusResponseModel.statuscode = "001";
                statusResponseModel.statusdesc = "Invalid Reference Number";
                return Json(statusResponseModel);
            }
            if (request.CardNumber.Length != 4)
            {
                statusResponseModel.statuscode = "001";
                statusResponseModel.statusdesc = "Invalid Card Number";
                return Json(statusResponseModel);
            }
            try
            {
                if (request == null)  {  return Json(0);  }
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                using (HttpResponseMessage responseMessage = _clientService.AddKidSingle(request, token))
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string result = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                        return Json(result);
                    }
                    return Json(statusResponseModel);
                }
            }
            catch (Exception ex)
            {
                statusResponseModel.statuscode = "001";
                statusResponseModel.statusdesc = "Un Expected Error";
                utility.ErrorLog(errorFolder, ex.Message.ToString());
                return Json(statusResponseModel);
            }
        }
        [HttpPost]
        public JsonResult AddBulkKitData(IFormFile file)
        {
            StatusResponseModel status_response_model = new StatusResponseModel();

            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1")
            {
                status_response_model.statuscode = "020";
                status_response_model.statusdesc = "UnAuthorized User";
                return Json(status_response_model);
            }
            if (file == null)
            {
                status_response_model.statuscode = "001";
                status_response_model.statusdesc = "File is Empty";
                return Json(0);
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var package = new ExcelPackage(stream))                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var worksheet = package.Workbook.Worksheets[0];
                        AddKitCardDetails addKitCardDetails = new AddKitCardDetails();
                        var dataTable = new DataTable(worksheet.Name);
                        foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                        {
                            dataTable.Columns.Add(firstRowCell.Value.ToString());
                        }
                        for (var rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                        {
                            var worksheetRow = worksheet.Cells[rowNumber, 1, rowNumber, 4 /*worksheet.Dimension.End.Column*/];
                            var dataRow = dataTable.NewRow();
                            foreach (var cell in worksheetRow)
                            {
                                dataRow[cell.Start.Column - 1] = cell.Value;
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                        string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                        using (HttpResponseMessage responseMessage = _clientService.AddKbulkKit(dataTable, token))
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                string result = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                                status_response_model = JsonSerializer.Deserialize<StatusResponseModel>(result);

                                return Json(status_response_model);
                            }                    
                        return Json(status_response_model);

                    }
                }

            }
            catch (Exception ex)
            {
                status_response_model.statuscode = "001";
                status_response_model.statusdesc = "Unable to read file.Please Double check the file datas";
                utility.ErrorLog(errorFolder, ex.Message.ToString());
                return Json(status_response_model);
            }

           



        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(
            string Status,
            string CustomerId,
            string Name1,
            string MobileNo,
            string EmailId,
            string Address1,
            string Address2,
            string Address3,
            string City1,
            string Pin1,
            string State1,
            string Country1,
            string AadhaarNo,
            string PanNo,
            string GstNo,
            string MiddleName,
            string LastName,
            string ShopName,
            string Title,
            string Gender,
            string ProofType,
            string DrivingLicense,
            string VoterId,
          string Dob
            )
        {
            string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
            if (user != "1")   {  return Json(1);  }

            ResponseModel resp = new ResponseModel();
            JsonRes objResponse = new JsonRes();
            RegistrationRequest objRequest = new RegistrationRequest();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            string lstrReturn = string.Empty;
            string lstrData = string.Empty;
            string lstrConsumerSecret = string.Empty;
            string lstrAPIKEY = string.Empty;
            try
            {

                objRequest.status = Status;
                objRequest.customerId = CustomerId;
                objRequest.firstName = Name1;
                objRequest.mobileNo = MobileNo;
                objRequest.emailAddress = EmailId;
                objRequest.addressProofNumber = AadhaarNo;
                objRequest.pan = PanNo;
                objRequest.gst = GstNo;
                objRequest.city = City1;
                objRequest.state = State1;
                objRequest.country = Country1;
                objRequest.pincode = Pin1;
                objRequest.address1 = Address1;
                objRequest.address2 = Address2;
                objRequest.address3 = Address3;
                objRequest.dob = Dob;
                if (objRequest.dob != null)
                {
                    string[] splitDateOfBirth = objRequest.dob.Split('-');
                    objRequest.dob = splitDateOfBirth[1] + "-" + splitDateOfBirth[2] + "-" + splitDateOfBirth[0];
                }
                objRequest.title = Title;
                objRequest.middleName = MiddleName;
                objRequest.lastName = LastName;
                objRequest.shopname = ShopName;
                objRequest.gender = Gender;
                objRequest.status = Status;
                objRequest.addressProofType = ProofType;
                objRequest.drivingLicense = DrivingLicense;
                objRequest.voterId = VoterId;
                objRequest.password = crypto.AES_ENCRYPT(utility.GenPassword(), COMMON.EMAILKEY);
                Thread.Sleep(100);
                objRequest.tpin = crypto.AES_ENCRYPT(utility.GetStan(), COMMON.EMAILKEY);
                objRequest.aesKey = utility.GetAESKEY();
                Thread.Sleep(100);
                objRequest.aesKey += utility.GetAESKEY();
                Thread.Sleep(100);
                objRequest.consumerkey = utility.GetStan() + utility.GetAlphaChar();//consumer key is called the api key
                Thread.Sleep(100);
                objRequest.consumersecret = utility.GetConsumerSecret();
                objRequest.vaccountid = " 9655" + objRequest.mobileNo + "PPI";
                objRequest.vifsccode = "UTIB0CCH274";
                objRequest.vbankname = "AXIS BANK";
                lstrData = JsonSerializer.Serialize(requestBody);
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
               // string strRequest = JsonSerializer.Serialize(objRequest);
               var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.UpdateStatus, stringContent).Result;
                string responsestring = JsonSerializer.Serialize(responseMessage);
                if (string.IsNullOrEmpty(responsestring))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return Json(0);
                }
                responseMessage.EnsureSuccessStatusCode();
                string stream = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                resp = JsonSerializer.Deserialize<ResponseModel>(stream);
                if (resp.statuscode == "000")
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    return Json(0);
                }
                else {return Json(1);}
            }
            catch (Exception ex) { return Json(1); }
        }

    }
}



