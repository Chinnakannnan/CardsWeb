using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYPAY.Models;
using PPICards.API_Service;
using PPICards.Helper;
using PPICards.Models;
using System;
using System.Text;
using System.Text.Json;

namespace PPICards.Controllers
{
    [Authorize]
    public class ProfilePageController : Controller
    {
        public const string errorFolder = "ProfilePage";
        private readonly IAPIClient _clientService;
        public ProfilePageController(IAPIClient iAPIClientInstance) => (_clientService) = (iAPIClientInstance);
        public IActionResult ProfilePage() => View();

        [HttpPost]
        public JsonResult UpdateCustomer(string emailId)
        {
            LockUnlock objRequest = new LockUnlock();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);
                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);
                objRequest.customerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                objRequest.entityId = HttpContext.Session.GetString(ConstValues.EntityId).Decrypt();
                objRequest.emailID = emailId;
                var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.UpdateCustomer, stringContent).Result;
                var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(resultValue)){return Json(1);}
                if (responseMessage.IsSuccessStatusCode){return Json(0);}
                else {return Json(1);}
            }
            catch (Exception ex) { utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(1);}
        }
        [HttpPost]
        public JsonResult ChangePassword(ResetPassword request)
        {
            try
            { 
                ResetPassword values = new ResetPassword();
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    var CustomerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    values.CustomerId = CustomerId;
                    values.CurrentPassword = request.CurrentPassword;
                    values.NewPassword = request.NewPassword;
                    values.ConfirmPassword = request.ConfirmPassword;
                }
                if (request is null) {return Json(0);}
                using (HttpResponseMessage responseMessage = _clientService.ChangePassword(values, token))
                if (responseMessage.IsSuccessStatusCode)
                {
                    string result = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                    return Json(result);
                }
                return Json(0);
            }
            catch (Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(0);}
        }
        [HttpPost]
        public JsonResult RaiseComplaint(RaiseComplaint request)
        {
            try

            {
                if(request.Subject.GetType() != typeof(string) || request.Comment.GetType() != typeof(string))
                {
                    return Json("Input is invalid");
                }
                RaiseComplaint values = new RaiseComplaint();
                string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                if (HttpContext.Session.GetString(ConstValues.SessionCustomerId) != null)
                {
                    var CustomerId = HttpContext.Session.GetString(ConstValues.SessionCustomerId);
                    values.CustomerID = CustomerId;
                    values.Subject = request.Subject;
                    values.Comment = request.Comment; 
                }
                if (request is null) { return Json(0); }
                using (HttpResponseMessage responseMessage = _clientService.RaiseComplaint(values, token))
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string result = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                        return Json(result);
                    }
                return Json(0);
            }
            catch (Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json(0);}
        }
    }
}
