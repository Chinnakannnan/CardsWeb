using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPICards.Models;
using System.Text;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;
using PPICards.Helper;
using PPICards.API_Service;
using MYPAY.Models;

namespace PPICards.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        public const string errorFolder = "Transaction";
        private readonly IAPIClient _clientService;
        public TransactionController(IAPIClient clientServiceInstance) => (_clientService) = (clientServiceInstance);
        public IActionResult Index()=> (View());

        public IActionResult Refund()=> (View());
        public IActionResult Topup() => (View());
        public IActionResult UPIReport()
        {
            GetStatus objRequest = new GetStatus();
            objRequest.fromdate = DateTime.Now.ToString("dd/MM/yyyy");
            objRequest.todate = DateTime.Now.ToString("dd/MM/yyyy");
            return View(objRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    using(HttpResponseMessage responseMessage = _clientService.PPITransactionReport(objRequest, token))
                    {
                        responsestring = JsonSerializer.Serialize(responseMessage);
                        
                        if (string.IsNullOrEmpty(responsestring))
                        {
                            ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                            return View("Index");
                        }
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            IList<PayoutHistoryDetails> objReport = new List<PayoutHistoryDetails>();
                            var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                            objReport = JsonSerializer.Deserialize<IList<PayoutHistoryDetails>>(resultValue);
                           
                            if (objReport.Count != 0)
                            { 
                                TempData["GetTransactionDetails"] = objReport;
                                if (objRequest.userType == "1"){ return RedirectToAction("Transactions", "Admin");}
                                else{ return View("Index"); }
                            }
                            else
                            {
                                PayoutHistoryDetails objReports = new PayoutHistoryDetails();
                                objReports.balance = "Null";
                                objReports.credit = "Null";
                                objReports.debit = "Null";
                                objReports.description = "Null";
                                objReports.balance = "Null";
                                objReports.openingBalance = "Null";
                                objReports.closingBalance = "Null";
                                objReports.transactionAmount = "Null";
       
                                 TempData["GetTransactionDetails"] = objReports;
                                if (objRequest.userType == "1") { return RedirectToAction("Transactions", "Admin"); }
                                else { return View("Index"); }
                            }
                        }

                    }
                    }
                return View("Index");
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

        [HttpPost]
        public IActionResult GetTransactionReports(string fromdate, string todate, string actiontype)
        {
            try
            {   
                TransactionDisplay transactionDisplay = new TransactionDisplay();
                transactionDisplay.transactionDisplayModels = new List<TransactionDisplayModel>();
                if (HttpContext.Session.GetString(ConstValues.EntityId) != null)
                {
                    string errormessage = string.Empty;
                    DateTime Fromdate = DateTime.MinValue;
                    DateTime Todate = DateTime.MinValue;
                    transactionDisplay.isError = fromdate.IsCheckValidFromTodate(todate, out errormessage, out Fromdate, out Todate);
                    transactionDisplay.errorMessage = errormessage;
                    if (actiontype != "filter")
                    {
                        transactionDisplay.isError = false;
                        Fromdate = DateTime.Now.AddDays(-30);
                        Todate = DateTime.Now;
                    }
                    if (!transactionDisplay.isError)
                    {
                        transactionDisplay.fromdate = Fromdate.ToString("MMM dd,yyyy");
                        transactionDisplay.todate = Todate.ToString("MMM dd,yyyy");
                        TransactionRequestModel transactionRequestModel = new TransactionRequestModel();
                        transactionRequestModel.entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                        transactionRequestModel.fromDate = Fromdate.Year.ToString("0000") + "-" + Fromdate.Month.ToString("00") + "-" + Fromdate.Day.ToString("00");
                        transactionRequestModel.toDate = Todate.Year.ToString("0000") + "-" + Todate.Month.ToString("00") + "-" + Todate.Day.ToString("00");
                        transactionRequestModel.pageNumber = "0";
                        transactionRequestModel.pageSize = "10000";
                        string token = HttpContext.Session.GetString(ConstValues.JwtValue);

                        string responsestring = string.Empty;
                        using (HttpResponseMessage responseMessage = _clientService.GetTransactionReports(transactionRequestModel, token))
                        {
                            if (responseMessage != null && responseMessage.IsSuccessStatusCode)
                            {
                                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(resultValue))
                                {
                                    var report = JsonSerializer.Deserialize<List<TransactionResponseModel>>(resultValue);

                                    if (report != null && report.Count > 0)
                                    {
                                        transactionDisplay.isError = false;

                                        transactionDisplay.transactionDisplayModels = (from x in report
                                                                                       select new TransactionDisplayModel
                                                                                       {
                                                                                           Balance = x.balance.ToString(".00"),
                                                                                           Credit = x.amount.ToString(".00"),
                                                                                           Debit = x.amount.ToString(".00"),
                                                                                           Description = x.type,
                                                                                           TransactionId = x.externalTransactionId,
                                                                                           time = x.time.DateTimeConverter(),
                                                                                           refno = x.txRef
                                                                                       }).ToList();
                                        transactionDisplay.transactionDisplayModels = new List<TransactionDisplayModel>(transactionDisplay.transactionDisplayModels.OrderByDescending(p => p.time).ToList());
                                        transactionDisplay.transactionDisplayModels.ForEach(x =>
                                        {
                                            x.TransactionOn = x.time.ToString("MMM dd,yyyy");
                                            if (x.Description == "CREDIT")
                                            {
                                                x.Debit = "";
                                            }
                                            else
                                            {
                                                x.Credit = "";
                                            }
                                        });
                                        if (actiontype != "filter")
                                        {
                                            transactionDisplay.transactionDisplayModels = transactionDisplay.transactionDisplayModels.Take(4).ToList();
                                        }
                                    }

                                }
                            }
                        }
                    }
                    return Json(transactionDisplay);
                }
               return Json(transactionDisplay);
            }
            catch(Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString());  return Json(null);}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomerWallet(WalletModel objRequest)
        {
            JsonRes objResponse = new JsonRes();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);                
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                objRequest.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                objRequest.cardReferenceId = "";
                objRequest.trnType = "wallettrn";
                objRequest.orderId = "";
                var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.Customerwallet, stringContent).Result;
                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(resultValue)) {return View("Index");}
                if (responseMessage.IsSuccessStatusCode){return View("Index");}
                else {return View("Index");}
            }
             catch(Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString()); return View("Index");}            

        }



    }
}
