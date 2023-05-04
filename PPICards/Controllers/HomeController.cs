using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MYPAY.Models;
using Newtonsoft.Json;
using PPICards.API_Service;
using PPICards.Helper;
using PPICards.Models;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace PPICards.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public const string errorFolder = "Home";
        private readonly ILogger<HomeController> _logger;    
        private readonly IAPIClient _clientService;
        public HomeController(IMemoryCache memoryCache, ILogger<HomeController> logger,
            IAPIClient clientServiceInstance) =>
        (_logger, _clientService) = (logger, clientServiceInstance);

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        public IActionResult Index()
        {
            try
            {
                FetchBalanceModel response = new FetchBalanceModel();
                if (HttpContext.Session.GetString(ConstValues.EntityId) != null)
                {
                    var entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                    string token= HttpContext.Session.GetString(ConstValues.JwtValue);
                    FetchBalanceModel balance = _clientService.GetBalance(entityId,token);
                    if (balance.result is not null)
                    {
                        var balanc1e = balance.result.Select(item => item.balance).FirstOrDefault();
                        ViewBag.Balance = balanc1e;
                    }
                    else { ViewBag.Balance = 00.00; }
                    if (entityId != null)
                        ViewBag.entityflag = 1;
                    else
                        ViewBag.entityflag = 0;
                }
            }
            catch (Exception ex)  { throw ex; }
            return View();
        }       
        [HttpPost]
        public async Task<IActionResult> LoadCardDetails()
        {
            try
            {
                ListUserKitDetails listUserKitDetails = new ListUserKitDetails();
                KITMap kitMap = new();
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    kitMap.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    using (HttpResponseMessage responseMessage = await _clientService.LoadCardDetails(kitMap, token))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            responseMessage.EnsureSuccessStatusCode();
                            listUserKitDetails.UserKitDetails = JsonConvert.DeserializeObject<List<UserKitDetails>>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                            //get activated card by status  
                            var dobResul = (from r in listUserKitDetails.UserKitDetails.AsEnumerable()
                                            where (r.CardStatus == "3")
                                            select r).ToList();
                            if (dobResul.Count > 0)
                            {
                                listUserKitDetails.CardModel = await GetCardDetails(dobResul.FirstOrDefault()?.DOB);
                            }

                            // get only assigned kit numbers
                            var result = (from r in listUserKitDetails.UserKitDetails.AsEnumerable()
                                          where (r.CardStatus == "2")
                                          select r).ToList();
                            listUserKitDetails.UserKitDetails = result;
                        }
                    }
                    return Json(new { data = listUserKitDetails });
                }
                else
                {  return Json(null); }

            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); throw ex;  return Json(null);  }
           
        }
        [HttpPost]
        public async Task<IActionResult> LoadActivatedCardDetails()
        {
            try
            {
                ListUserKitDetails listUserKitDetails = new();
                KITMap kitMap = new();
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    kitMap.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    using (HttpResponseMessage responseMessage = await _clientService.LoadActivatedCardDetails(kitMap, token))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            responseMessage.EnsureSuccessStatusCode();
                            listUserKitDetails.UserKitDetails = JsonConvert.DeserializeObject<List<UserKitDetails>>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                            //get activated card by status  
                            var dobResul = (from r in listUserKitDetails.UserKitDetails.AsEnumerable()
                                            where (r.CardStatus == "3")
                                            select r).ToList();
                            listUserKitDetails.UserKitDetails = dobResul;
                        }
                    }
                }
                return Json(new { data = listUserKitDetails });
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(null); }
        }
        public async Task<IActionResult> LoadPermanentBlockCardDetails()
        {
            try
            {
                ListUserKitDetails listUserKitDetails = new ListUserKitDetails();
                KITMap kitMap = new KITMap();
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    kitMap.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    using (HttpResponseMessage responseMessage = await _clientService.LoadPermanentBlockCardDetails(kitMap, token))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            responseMessage.EnsureSuccessStatusCode();
                            listUserKitDetails.UserKitDetails = JsonConvert.DeserializeObject<List<UserKitDetails>>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                            //get activated card by status  
                            var dobResul = (from r in listUserKitDetails.UserKitDetails.AsEnumerable()
                                            where (r.CardStatus == "5")
                                            select r).ToList();
                            listUserKitDetails.UserKitDetails = dobResul;
                        }
                    }
                }
                return Json(new { data = listUserKitDetails });
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); throw ex;  return Json(null);  }
        }
        [HttpPost]
        public async Task<IActionResult> GenerateOTP(string CardNumber)
        {
            try
            {
                ActivateCardDetail activateCardDetail = new ActivateCardDetail();
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    string CustomerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    activateCardDetail.customerId = CustomerId;
                    activateCardDetail.kitReferenceNumber = CardNumber;
                    if (string.IsNullOrEmpty(CustomerId) && string.IsNullOrEmpty(CardNumber))
                    return Json(string.Empty);

                    using (HttpResponseMessage responseMessage = await _clientService.GenerateOTP(activateCardDetail, token))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            string EmailId = HttpContext.Session.GetString(ConstValues.SessionEmailId);
                            return Json(EmailId);
                        }
                        else  {  return Json(string.Empty);  }
                    }
                }

                return Json(string.Empty);
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(null);  }
        }
        [HttpPost]
        public async Task<IActionResult> ActivateCard(string CardNumber, string OTP)
        {
            try
            {
                DisplayModel displayModel = new DisplayModel();
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    if (!string.IsNullOrEmpty(OTP))
                    {
                        ActivateCardDetail activateCardDetail = new ActivateCardDetail();
                        string CustomerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                        string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                        activateCardDetail.customerId = CustomerId;
                        activateCardDetail.kitReferenceNumber = CardNumber;
                        activateCardDetail.otp = OTP;
                        //LoginResponseModel loginResp = new LoginResponseModel();
                        //string activaterequest = JsonConvert.SerializeObject(activateCardDetail);
                        //var stringContent = new StringContent(JsonConvert.SerializeObject(activateCardDetail), Encoding.UTF8, OnboardConstants.ApplicationJson);
                        using (HttpResponseMessage responseMessage = await _clientService.ActivateCard(activateCardDetail, token))
                        {
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                string stream = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                                displayModel.IsSuccess = true;
                            }
                            else
                            { displayModel.IsSuccess = false;  }
                        }

                    }
                    else
                    {
                        displayModel.IsSuccess = false;
                        displayModel.ErrorMessage = "Invalid OTP !!!";
                    }
                }               
                return Json(displayModel);
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(string.Empty);  }
        }
        public async Task<CardModel> GetCardDetails(DateTime? dateTime)
        {
            try
            {
                CardModel cardModel = new CardModel();
                if (HttpContext.Session.GetString(ConstValues.EntityId) != null)
                {
                    if (dateTime == null)
                    {
                        cardModel.CardNumber = string.Empty.ConvertCardNumber(1);
                        cardModel.CardExpiryDate = string.Empty.ConvertExpiryDate(1);
                        cardModel.CVV = string.Empty.ConvertCVV(1);
                        cardModel.KitNo = string.Empty;

                    }
                    cardModel.dob = dateTime == null ? DateTime.MinValue : dateTime;
                    string entityid = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    CardRequest cardRequest = new CardRequest { entityId = entityid };
                    using (HttpResponseMessage responseMessage = await _clientService.GetCardDetails(cardRequest, token))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            var cardDetails = JsonConvert.DeserializeObject<CardResponse>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                            //HttpContext.Session.SetString(ConstValues.SessionDOB, (userKitDetails.FirstOrDefault().DOB.Month + "/" + userKitDetails.FirstOrDefault().DOB.Year).encrypt());
                            if (cardDetails != null && cardDetails.result != null)
                            {
                                int index = -1;
                                if (cardDetails.result.cardStatusList.Any(p => p == "ALLOCATED"))
                                {
                                    index = cardDetails.result.cardStatusList.ToList().IndexOf(cardDetails.result.cardStatusList.Where(p => p == "ALLOCATED").FirstOrDefault());
                                    HttpContext.Session.SetString(ConstValues.CardStatus, "Active");
                                }
                                else
                                {
                                    index = cardDetails.result.cardStatusList.ToList().IndexOf(cardDetails.result.cardStatusList.Where(p => p == "LOCKED").FirstOrDefault());
                                    HttpContext.Session.SetString(ConstValues.CardStatus, "Blocked");
                                }
                                if (index > -1)
                                {
                                    var cardnumber = !string.IsNullOrEmpty(cardDetails.result.cardList[index]) ?
                                        cardDetails.result.cardList[index] : string.Empty;
                                    var CardExpiryDate = !string.IsNullOrEmpty(cardDetails.result.expiryDateList[index]) ?
                                        cardDetails.result.expiryDateList[index] : string.Empty;
                                    var KitNo = !string.IsNullOrEmpty(cardDetails.result.kitList[index]) ?
                                        cardDetails.result.kitList[index] : string.Empty;
                                    cardModel.CardNumber = cardnumber;
                                    cardModel.CardExpiryDate = CardExpiryDate;
                                    cardModel.KitNo = KitNo;
                                    var carddetail = JsonConvert.SerializeObject(cardModel).ToString().encrypt();
                                    HttpContext.Session.SetString(ConstValues.CardDetails, carddetail);


                                    cardModel.CardNumber = cardnumber.ConvertCardNumber(1);
                                    cardModel.CardExpiryDate = CardExpiryDate.ConvertExpiryDate(1);
                                    cardModel.CVV = cardModel.CVV.ConvertCVV(1);
                                }
                                else
                                {
                                    var carddetail = JsonConvert.SerializeObject(cardModel).ToString().encrypt();
                                    HttpContext.Session.SetString(ConstValues.CardDetails, carddetail);
                                }
                            }
                            else
                            {
                                cardModel.CardNumber = string.Empty.ConvertCardNumber(1);
                                cardModel.CardExpiryDate = string.Empty.ConvertExpiryDate(1);
                                cardModel.CVV = string.Empty.ConvertCVV(1);
                                cardModel.KitNo = string.Empty;
                                var carddetail = JsonConvert.SerializeObject(cardModel).ToString().encrypt();
                                HttpContext.Session.SetString(ConstValues.CardDetails, carddetail);
                            }

                        }
                    }
                }
                return cardModel;
            }
            catch (Exception ex) {utility.ErrorLog(errorFolder, ex.Message.ToString()); return null;}        
        }
        private async Task<string> GetCVV(DateTime? dob, string CardExpiryDate, string KitNo, string entityid)
        {
            try {
                string mm = CardExpiryDate.Substring(0, 2);
                string yy = CardExpiryDate.Substring(CardExpiryDate.Length - 2, 2);
                CVVRequest cVVRequest = new CVVRequest
                {
                    entityId = entityid.Decrypt(),
                    kitNo = KitNo,
                    expiryDate = yy + mm,
                    dob = (dob?.Day.ToString("00") + dob?.Month.ToString("00") + dob?.Year.ToString("0000"))
                };
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                string returnvalue = string.Empty;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                var stringContent = new StringContent(JsonConvert.SerializeObject(cVVRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                using (HttpResponseMessage responseMessage = await http.PostAsync(OnboardConstants.GetCardCVVDetails, stringContent))
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var cardDetails = JsonConvert.DeserializeObject<CardCVVModelResponse>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                        if (cardDetails != null && cardDetails.result != null)
                        {
                            returnvalue = cardDetails.result.cvv;
                        }

                    }
                }
                return returnvalue;
            }
            catch (Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString()); return null;}
            

        }
        [HttpPost]
        public IActionResult Loadcvv(int type)
        {
            try
            {
                var carddetail = HttpContext.Session.GetString(ConstValues.CardDetails)?.Decrypt();
                CardModel? cardmodel = !string.IsNullOrEmpty(carddetail) ? JsonConvert.DeserializeObject<CardModel>(carddetail) : null;
                CardModel cardModel = new CardModel();
                if (cardmodel != null)
                {
                    cardModel.CardNumber = cardmodel.CardNumber.ConvertCardNumber(type);
                    cardModel.CardExpiryDate = cardmodel.CardExpiryDate.ConvertExpiryDate(type);
                    cardModel.CVV = cardmodel.CVV.ConvertCVV(type);
                    cardModel.KitNo = string.Empty;
                }
                else
                {
                    cardModel.CardNumber = string.Empty.ConvertCardNumber(type);
                    cardModel.CardExpiryDate = string.Empty.ConvertExpiryDate(type);
                    cardModel.CVV = string.Empty.ConvertCVV(type);
                    cardModel.KitNo = string.Empty;
                }

                return Json(cardModel);
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return null; }
        }
        [HttpGet]
        public async Task<IActionResult> LoadData(int type)
        {
           try
            {
                string retunvalue = string.Empty;
                if (HttpContext.Session.GetString(ConstValues.EntityId) != null)
                {
                    string entityid = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    var carddetail = HttpContext.Session.GetString(ConstValues.CardDetails)?.Decrypt();
                    CardModel? cardmodel = !string.IsNullOrEmpty(carddetail) ? JsonConvert.DeserializeObject<CardModel>(carddetail) : null;
                    if (cardmodel != null && cardmodel.dob != null && cardmodel.dob != DateTime.MinValue)
                    {
                        PinRequest pinRequest = new PinRequest
                        {
                            dob = (cardmodel.dob?.Day.ToString("00") + cardmodel.dob?.Month.ToString("00") + cardmodel.dob?.Year.ToString("0000")),
                            entityId = entityid,
                            kitNo = cardmodel.KitNo

                        };
                        string returnvalue = string.Empty;
                        //var stringContent = new StringContent(JsonConvert.SerializeObject(pinRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                        using (HttpResponseMessage responseMessage = await _clientService.LoadData(pinRequest, type, token))
                        {
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                var cardDetails = JsonConvert.DeserializeObject<PinResponse>(responseMessage.Content.ReadAsStringAsync().Result.ToString());
                                if (cardDetails != null && cardDetails.result != null)
                                {
                                    retunvalue = cardDetails.result;
                                }

                            }
                        }
                    }
                }
                return Json(retunvalue);
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return null; }
           
        }
    }
}
