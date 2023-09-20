using Newtonsoft.Json;
using PPICards.Models;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static PPICards.Models.OnboardingModel;
using Microsoft.AspNetCore.Mvc;
using PPICards.Helper;
using NuGet.Common;
using Newtonsoft.Json.Linq;
using System.Data;

namespace PPICards.API_Service
{
    public class APIClient : IAPIClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public APIClient(HttpClient client, IConfiguration configuration)
        {
            _configuration=configuration;
            _client = client;
            //Setting TLS 1.2 protocol
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _client.BaseAddress = new Uri(_configuration.GetSection("ApplicationSettings").GetSection("BaseUrl").Value);
            _client.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);

        }
        public async Task<HttpResponseMessage> Login(Dictionary<string, string> requestBody)
        {
            try
            {
                
                string clientId = _configuration.GetSection("ApplicationSettings").GetSection("ClientId").Value;
                string CliendSecrect = _configuration.GetSection("ApplicationSettings").GetSection("ClientSecrect").Value;
                var stringContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, OnboardConstants.ApplicationJson);
                _client.DefaultRequestHeaders.Add(ConstValues.ClientId, clientId);
                _client.DefaultRequestHeaders.Add(ConstValues.ClientSecrect, CliendSecrect);
                return await _client.PostAsync(OnboardConstants.LoginValidate, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage UnAuthorizedBlock(LoginModel objRequest)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.UnauthorizedBlock, stringContent).Result; ;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;

            }
        }
        public HttpResponseMessage GetUserInfo(LoginModel objRequest, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt()); ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; 
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.GetuserInfo, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SendOTP(LoginResponseModel loginResp, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var json = (JsonConvert.SerializeObject(loginResp));
                var stringContent = new StringContent(JsonConvert.SerializeObject(loginResp), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = _client.PostAsync(OnboardConstants.LoginOTP, stringContent).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public HttpResponseMessage GetAdminDashboardData(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                return  _client.GetAsync(OnboardConstants.GetUser).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FetchBalanceModel GetBalance(string entityId, string token)
        {
            FetchBalanceModel response = new FetchBalanceModel();
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                HttpResponseMessage responseMessage = _client.GetAsync(OnboardConstants.GetCustomerBalance + "?entityId=" + entityId).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    //objLog.WriteAppLog("Customer Wallet - Fetch Balance response -  :" + resultValue, lstrFolderName);
                    response = JsonConvert.DeserializeObject<FetchBalanceModel>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    //objLog.WriteAppLog("Customer Wallet - Fetch Balance response -  :" + resultValue, lstrFolderName);
                    response = JsonConvert.DeserializeObject<FetchBalanceModel>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                return response;
            }
            catch (Exception ex)
            {
                //throw ex;
                return response;

            }
        }
        public HttpResponseMessage FetchCustomerPreferences(Dictionary<String, String> FetchDetails, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var x = JsonConvert.SerializeObject(FetchDetails);
                var stringContent = new StringContent(JsonConvert.SerializeObject(FetchDetails), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.CustomerPreferences, stringContent).Result;

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;

            }

        }
        public HttpResponseMessage CustomerPreferance(Dictionary<String, String> preferanceDetails, string token)
        {
            FetchBalanceModel response = new FetchBalanceModel();
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //var stringContent = new StringContent(entityId, Encoding.UTF8, OnboardConstants.ApplicationJson);
                var stringContent = new StringContent(JsonConvert.SerializeObject(preferanceDetails), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.TrancationControl, stringContent).Result;

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;

            }
        }
        public HttpResponseMessage ReplaceCard(Dictionary<String, String> request,string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.ReplaceCard, stringContent).Result;

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;

            }
        }
        public HttpResponseMessage AvailableNewCard(string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.AvailableNewCards, stringContent).Result;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;

            }
        }

        
        public HttpResponseMessage AddKbulkKit(DataTable request, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                string test = JsonConvert.SerializeObject(request); // just check for result
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.AddBulkKitData, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HttpResponseMessage AddKidSingle(AddKitCardDetails request, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.AddKitData, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HttpResponseMessage CustomerTransactionLimit(TxnLimit limit,string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                //Setting TLS 1.2 protocol
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var json  = JsonConvert.SerializeObject(limit);

                var stringContent = new StringContent(JsonConvert.SerializeObject(limit), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.TrancationLimitControl, stringContent).Result;

            }
            catch (Exception ex)
            {
                //throw ex;
                return null;

            }
        }

        public async Task<HttpResponseMessage> LoadCardDetails(KITMap kitMap, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(kitMap), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(OnboardConstants.GetAssignedKitMappingDetailsByCustomer, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> LoadActivatedCardDetails(KITMap kitMap,string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(kitMap), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(OnboardConstants.GetAssignedKitMappingDetailsByCustomer, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> LoadPermanentBlockCardDetails(KITMap kitMap, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(kitMap), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(OnboardConstants.GetAssignedKitMappingDetailsByCustomer, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> GenerateOTP(ActivateCardDetail activateCard, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(activateCard), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(OnboardConstants.GetOTP, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> ActivateCard(ActivateCardDetail activateCard, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(activateCard), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(OnboardConstants.CardActivation, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> GetCardDetails(CardRequest cardRequest, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(cardRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(OnboardConstants.GetCardDetailsbyCustomer, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HttpResponseMessage> LoadData(PinRequest pinRequest,int type, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(pinRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return await _client.PostAsync(type == 1 ? OnboardConstants.SetCardPin : OnboardConstants.FetchCard, stringContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage ChangePassword(ResetPassword request, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.PassswordReset, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage PPITransactionReport(TransactionModel objRequest, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.TransactionReport, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HttpResponseMessage GetTransactionReports(TransactionRequestModel objRequest, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.TransactionReportByDate, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage CustomerWallet(WalletModel objRequest, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.Customerwallet, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage RaiseComplaint(RaiseComplaint request, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                //_client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.RaiseComplaint, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage GetLimit(GetLimitRequest objRequest, string token) 
        {

            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token.Decrypt());
                var stringContent = new StringContent(JsonConvert.SerializeObject(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                return _client.PostAsync(OnboardConstants.GetLimit, stringContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
