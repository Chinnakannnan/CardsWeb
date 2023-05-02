using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPICards.Models;
using System.Text;
using System.Text.Json;
using static PPICards.Models.OnboardingModel;

namespace PPICards.Controllers
{
    [Authorize]
    public class SendingLink : Controller
    {
        public IActionResult SendLink()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendLink(sendLink objRequest)
        {

            HttpClient objPost = new HttpClient();
            JsonRes objResponse = new JsonRes();
            sendLink response = new sendLink();
            Dictionary<string, string> requestBody = new Dictionary<string, string>();
            try
            {
                //these values are static 
                objRequest.mapUsercode = "23423";//crypto.AES_DECRYPT(HttpContext.Session.GetString("custid").ToString(), COMMON.KEY);
                objRequest.supUsertype = "wqweqwe";// crypto.AES_DECRYPT(HttpContext.Session.GetString("custusertype").ToString(), COMMON.KEY);




                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(OnboardConstants.DMTURL);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.TryAddWithoutValidation(OnboardConstants.ContentType, OnboardConstants.ApplicationJson);



                var stringContent = new StringContent(JsonSerializer.Serialize(objRequest), Encoding.UTF8, OnboardConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(OnboardConstants.SendLinkURL, stringContent).Result;
                string responsestring = JsonSerializer.Serialize(responseMessage);

                if (string.IsNullOrEmpty(responsestring))
                {
                    ViewBag.data = ResponseCode.Invalid_Response + "|" + ResponseMsg.Invalid_Response;
                    return View("SendLink");
                }
                if (responseMessage.IsSuccessStatusCode)
                {
                    objResponse.StatusCode = "000";
                    ViewBag.data = objResponse.StatusCode + "|" + " Check your Email ID for Link ";

                    return View("SendLink");

                }

                else
                {
                    ViewBag.data = "Failed to send link";

                    return View("SendLink");
                }
            }

            catch (Exception ex)
            {
                return View("SendLink");
            }
            finally
            {
                objRequest = null;
            }

        }















    }
}
