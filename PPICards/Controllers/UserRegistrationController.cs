using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult UserRegistrationView() => View();
        public IActionResult RegisterUser()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegistrationRequest objRequest)
        {
            ResponseModel resp = new ResponseModel();
            string DateOfBirth = string.Empty;
            HttpClient objPost = new HttpClient();
            JsonRes objResponse = new JsonRes();
            // sendLink response = new sendLink();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            try
            {
                string token = HttpContext.Session.GetString(ConstValues.JwtValue).Decrypt();
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.BaseUrl);            
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(ConstValues.Authorization, ConstValues.Bearer + " " + token);

                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);


                if (objRequest.dob != null)
                {
                    string[] splitDateOfBirth = objRequest.dob.Split('-');
                    objRequest.dob = splitDateOfBirth[1] + "-" + splitDateOfBirth[2] + "-" + splitDateOfBirth[0];
                }

                string request = JsonSerializer.Serialize(objRequest);

                string jsonData = JsonSerializer.Serialize(objRequest);

                var stringContent = new StringContent(JsonSerializer.Serialize(jsonData), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.UserRegistration, stringContent).Result;
                string responsestring = JsonSerializer.Serialize(responseMessage);
                //string responsestring = "sdfsdfsdfsd";

                if (string.IsNullOrEmpty(responsestring))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return View("UserRegistrationView");
                }
                responseMessage.EnsureSuccessStatusCode();
                string stream = responseMessage.Content.ReadAsStringAsync().Result.ToString();

                resp = JsonSerializer.Deserialize<ResponseModel>(stream);
                if (resp.statuscode == "000")
                {
                    ViewBag.Register = resp.statuscode;

                    return View("UserRegistrationView");

                }

                else
                {
                    ViewBag.Failed = resp.statuscode;

                    return View("UserRegistrationView");
                }
            }

            catch (Exception ex)
            {
                return View("UserRegistrationView");
            }
            finally
            {
                objRequest = null;
            }

        }




    }
}
