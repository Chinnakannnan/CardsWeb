using PPICards.Models;
using System.Data;
using static PPICards.Models.OnboardingModel;

namespace PPICards.API_Service
{
    public interface IAPIClient
    {
        bool SendOTP(LoginResponseModel loginResp,string token);
        Task<HttpResponseMessage> Login(Dictionary<string, string> requestBody);
        HttpResponseMessage UnAuthorizedBlock(LoginModel objRequest);
        HttpResponseMessage GetUserInfo(LoginModel objRequest, string token);
        FetchBalanceModel GetBalance(string entityId, string token);
        HttpResponseMessage CustomerPreferance(Dictionary<String, String> preferanceDetails, string token);
        HttpResponseMessage CustomerTransactionLimit(TxnLimit limit, string token);
        HttpResponseMessage FetchCustomerPreferences(Dictionary<String, String> FetchDetails, string token);
        HttpResponseMessage ReplaceCard(Dictionary<String, String> request, string token);
        HttpResponseMessage AvailableNewCard(string token);
        HttpResponseMessage AddKidSingle(AddKitCardDetails request, string token);
        HttpResponseMessage AddKbulkKit(DataTable request, string token);
        HttpResponseMessage GetAdminDashboardData(string token);
        Task<HttpResponseMessage> LoadCardDetails(KITMap kitMap, string token);
        Task<HttpResponseMessage> LoadActivatedCardDetails(KITMap kitMap,string token);
        Task<HttpResponseMessage> LoadPermanentBlockCardDetails(KITMap kitMap, string token);
        Task<HttpResponseMessage> GenerateOTP(ActivateCardDetail activateCard, string token);
        Task<HttpResponseMessage> ActivateCard(ActivateCardDetail activateCard, string token);
        Task<HttpResponseMessage> GetCardDetails(CardRequest cardRequest, string token);
        Task<HttpResponseMessage> LoadData(PinRequest pinRequest, int type, string token);
        HttpResponseMessage ChangePassword(ResetPassword request, string token);
        HttpResponseMessage RaiseComplaint(RaiseComplaint request, string token);
        HttpResponseMessage PPITransactionReport(TransactionModel objRequest, string token);
        HttpResponseMessage GetTransactionReports(TransactionRequestModel objRequest, string token);
        HttpResponseMessage CustomerWallet(WalletModel objRequest, string token);
        HttpResponseMessage GetLimit(GetLimitRequest objRequest, string token);
    }
}
