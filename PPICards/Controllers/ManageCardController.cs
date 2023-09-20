using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MYPAY.Models;
using Newtonsoft.Json;
using PPICards.API_Service;
using PPICards.Helper;
using PPICards.Models;
using System.Text;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;

namespace PPICards.Controllers
{
    [Authorize]
    public class ManageCardController : Controller
    {
        public const string errorFolder = "ManageCard";
        private readonly IAPIClient _clientService;
        public ManageCardController(IAPIClient clientServiceInstance) => (_clientService) = (clientServiceInstance);
        public IActionResult Index()
        {
            FetchCustomerPreferences();
            return View();
        }
        public IActionResult BlockCard() => View();
        public IActionResult ChangePIN() => View();
        public IActionResult ReplacementCard() => View();
        public IActionResult CardReplace(string oldKitNo)
        {
            try {
                var entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                HttpClient http = new HttpClient();
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                HttpResponseMessage responseMessage = _clientService.AvailableNewCard(token);
                String kitNumber = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                AvailabeNewcards objResponse = JsonConvert.DeserializeObject<AvailabeNewcards>(kitNumber);
                Dictionary<String, String> values = new Dictionary<String, String>();
                values.Add("entityId", entityId);
                values.Add("oldKitNo", oldKitNo);
                values.Add("newKitNo", objResponse.newKitNo);//Need to check
                using (HttpResponseMessage responseMessages = _clientService.ReplaceCard(values, token))
                    if (responseMessages.IsSuccessStatusCode)
                    {
                        responseMessages.EnsureSuccessStatusCode();
                        String result = responseMessages.Content.ReadAsStringAsync().Result.ToString();
                        return View("Index");
                    }
                return View("Index");
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return View("Index"); }            
        }
        [HttpPost]
        public IActionResult FetchCustomerPreferences()
        {
            try
            {
                var entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                HttpClient http = new HttpClient();
                Dictionary<String, String> values = new Dictionary<String, String>();
                values.Add("entityId", entityId);
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                using (HttpResponseMessage responseMessage = _clientService.FetchCustomerPreferences(values, token))
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        responseMessage.EnsureSuccessStatusCode();
                        string result = responseMessage.Content.ReadAsStringAsync().Result.ToString();                         
                        CustomerPreferencesResponse objResponse = JsonConvert.DeserializeObject<CustomerPreferencesResponse>(result);
                        ViewBag.ATM = ((objResponse.result.atm == "true") ? "Checked" : "");
                        ViewBag.POS = ((objResponse.result.pos == "true") ? "Checked" : "");
                        ViewBag.CONTACTLESS = ((objResponse.result.contactless == "true") ? "Checked" : "");
                        ViewBag.ECOM = ((objResponse.result.ecom == "true") ? "Checked" : "");

                        var KitReferenceNumber = HttpContext.Session.GetString(ConstValues.KitReferenceNumber);
                        GetLimitRequest value= new GetLimitRequest();
                        value.KitReferenceNumber = KitReferenceNumber;
                       
                        using (HttpResponseMessage responseItems = _clientService.GetLimit(value, token))                            
                        if (responseItems.IsSuccessStatusCode)
                        {
                            string objresult = responseItems.Content.ReadAsStringAsync().Result.ToString();
                            GetLimit objRes = JsonConvert.DeserializeObject<GetLimit>(objresult);

                            ViewBag.ATMLIMIT = objRes.ATM;
                            ViewBag.POSLIMIT = objRes.POS;
                            ViewBag.CONTACTLESSLIMIT = objRes.CONTACTLESS;
                            ViewBag.ECOMLIMIT = objRes.ECOM;

                        }
                            return View("Index");
                    }
                return View("Index");
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return View("Index");}           

        }
        [HttpPost]
        public IActionResult ManageCardDoestic(string Flag, string Status)
        {
            try {
                var entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                HttpClient http = new HttpClient();
                string status = ((Status == "true") ? "ALLOWED" : "NOTALLOWED");
                Dictionary<String, String> values = new Dictionary<String, String>();
                values.Add("entityId", entityId);
                values.Add("status", status);
                values.Add("type", Flag);
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                using (HttpResponseMessage responseMessage = _clientService.CustomerPreferance(values, token))
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        responseMessage.EnsureSuccessStatusCode();
                        String result = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                        return Json(result);
                    }
                return Json(1);
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(1); }           

        }
         
        [HttpPost]
        public IActionResult ManageCardLimitDoestic(string Flag, double Limit)
        {
            try
            {
                double value = Limit;
                int roundedValue = (int)Math.Round(value);
                string roundedValueString = roundedValue.ToString();
                bool containsNonNumeric = System.Text.RegularExpressions.Regex.IsMatch(roundedValueString, @"\D");
                if (containsNonNumeric)
                {
                    return Json(1);
                }
                 

                var entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                HttpClient http = new HttpClient();
                TxnLimit values = new TxnLimit();
                values.entityId = entityId;
                values.KitReferenceNumber = HttpContext.Session.GetString(ConstValues.KitReferenceNumber);
                values.txnType = Flag;
                values.dailyLimitValue = Limit.ToString();
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                using (HttpResponseMessage responseMessage = _clientService.CustomerTransactionLimit(values, token))
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        responseMessage.EnsureSuccessStatusCode();
                        String result = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                        return Json("Success");
                    }

                return Json("Failed");
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(1); };            
        }
        [HttpPost]
        public JsonResult LockUnlockCard(string CardNo, string Flag, string Reason)
        
        {
            JsonRes objResponse = new JsonRes();
            LockUnlock objRequest = new LockUnlock();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            try
            {
                if (string.IsNullOrEmpty(CardNo) || string.IsNullOrEmpty(Flag) || string.IsNullOrEmpty(Reason))
                {
                    return Json("Input is Empty");
                }
                if (CardNo.GetType() != typeof(string) || Flag.GetType() != typeof(string) || (Reason.GetType() != typeof(string)))
                {
                    return Json("Input is invalid");
                }
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();               
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);

                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                objRequest.cardReferenceId = CardNo;
                objRequest.flag = Flag;
                if (Reason == "1") { objRequest.reason = "Lost the card"; }
                if (Reason == "2") { objRequest.reason = "Card has been stolen"; }
                if (Reason == "3") { objRequest.reason = "Not yet received the card"; }
                objRequest.reason = Reason;
                objRequest.entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.CardLockUnlock, stringContent).Result;
                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(resultValue)) {  return Json(0); }
                if (responseMessage.IsSuccessStatusCode)
                {
                    CardActivationResult  deserializedResponse = JsonConvert.DeserializeObject<CardActivationResult>(resultValue);
                    if(deserializedResponse.result==null)
                    {
                        return Json(deserializedResponse.exception.detailMessage.ToString());
                    }                 

                  if (Flag == "L") { return Json(1); }
                  if (Flag == "UL") { return Json(2); }
                } if (Flag == "BL") { return Json(3); }
                  else { return Json(0); }
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(0); }
        }
        [HttpPost]
        public JsonResult CustomerWallet(string amount, string kitRefNo)
        {
            WalletModel objRequest = new WalletModel();
            ResponseModel resp = new();
            try
            {
                if (string.IsNullOrEmpty(amount) && string.IsNullOrEmpty(kitRefNo)) { return Json(1); }
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    objRequest.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    objRequest.cardReferenceId = kitRefNo;
                    objRequest.trnType = "wallettrn"; // need to remove
                    objRequest.orderId = "test";  // need to remove
                    objRequest.amount = amount;
                    using (HttpResponseMessage responseMessage = _clientService.CustomerWallet(objRequest, token))
                    {
                        resp = JsonConvert.DeserializeObject<ResponseModel>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                        if (resp is null) { return Json(1);}
                        if (responseMessage.IsSuccessStatusCode)
                        { if(resp.statuscode== "004") { return Json(2);}  
                          else { return Json(3); }                            
                        }
                        else { return Json(1); }
                    }
                }
                return Json(1);
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(1);}
        }
        [HttpPost]
        public JsonResult CustomerRefund(string amount)
        {
            WalletModel objRequest = new WalletModel();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();           
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                objRequest.fromEntityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                objRequest.amount = amount;
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.RefundCustomer, stringContent).Result;
                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(resultValue))  { return Json(1); }
                if (responseMessage.IsSuccessStatusCode) { return Json(0);}
                else {return Json(1);}
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(1);}
        }
    }
}
