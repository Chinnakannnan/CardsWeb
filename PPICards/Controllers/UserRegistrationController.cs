using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MYPAY.Models;
using PPICards.Helper;
using PPICards.Models;
using System.Text;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;

namespace PPICards.Controllers
{
    [Authorize]
    public class UserRegistrationController : Controller
    {
        public const string errorFolder = "UserRegistration";
        public IActionResult UserRegistrationView() => View();
        public IActionResult RegisterUser() => View();    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegistrationRequest objRequest)
        {
            try
            {
                var token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                var http = new HttpClient {BaseAddress = new Uri(OnboardConstants.BaseUrl)};
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                if (objRequest.dob != null)
                {
                    var splitDateOfBirth = objRequest.dob.Split('-');
                    objRequest.dob = $"{splitDateOfBirth[1]}-{splitDateOfBirth[2]}-{splitDateOfBirth[0]}";
                }
                var requestJson = JsonSerializer.Serialize(objRequest);
                var stringContent = new StringContent(requestJson, Encoding.UTF8, OnboardConstants.ApplicationJson);
                var responseMessage = http.PostAsync(OnboardConstants.UserRegistration, stringContent).GetAwaiter().GetResult();
                if (!responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.data = $"{ResponseCode.Invalid_Response}|{ResponseMsg.Invalid_Response}";
                    return View("UserRegistrationView");
                }
                var responseJson = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var resp = JsonSerializer.Deserialize<ResponseModel>(responseJson);
                if (resp.statuscode == "000") { ViewBag.Register = resp.statuscode; return View("UserRegistrationView"); }
                else { ViewBag.Failed = resp.statuscode; return View("UserRegistrationView"); }
            }
            catch(Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return View("UserRegistrationView"); }
           
        }


    }
}
