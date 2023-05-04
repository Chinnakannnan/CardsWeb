using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using MYPAY.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Configuration;
using PPICards.API_Service;
using PPICards.Helper;
using PPICards.Models;
using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;
using static PPICards.Models.OnboardingModel;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;

namespace PPICards.Controllers
{
    public class LoginController : Controller
    {
        public const string errorFolder = "Login";
        private readonly IAPIClient _clientService;
        public LoginController(IAPIClient clientServiceInstance) => (_clientService) = (clientServiceInstance);
        public IActionResult Login() => View("Login");
        public IActionResult OTPValidateforLogin() => View();
        public IActionResult ForgotPassword() => View();
        static bool HasExpired(DateTime creationTime)
        {
            TimeSpan timeSinceCreation = DateTime.Now - creationTime;
            return timeSinceCreation.TotalMinutes >= 1;
        }       
        [HttpPost]
        public async Task<JsonResult> LoginView(string UserName, string Password)
        {
            LoginResponseModel loginResp = new LoginResponseModel();
            try
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetString(ConstValues.LoginAttempt)))
                {
                    HttpContext.Session.SetString(ConstValues.LoginAttempt, "0");
                }
                if (HttpContext.Session.GetString(ConstValues.LoginAttempt) == "3")
                {
                    LoginModel Values = new LoginModel();
                    Values.UserName = UserName;

                    using (HttpResponseMessage responseMessages = _clientService.UnAuthorizedBlock(Values))
                    {
                        responseMessages.EnsureSuccessStatusCode();
                        string stream = responseMessages.Content.ReadAsStringAsync().Result.ToString();
                        loginResp = JsonConvert.DeserializeObject<LoginResponseModel>(stream);
                        if (loginResp.StatusCode == "000")
                        {
                            loginResp.StatusCode = "001";
                            loginResp.StatusDesc = loginResp.StatusDesc;
                            HttpContext.Session.Remove(ConstValues.LoginAttempt);
                            return Json(loginResp);
                        }
                        else
                        {
                            loginResp.StatusCode = "001";
                            loginResp.StatusDesc = "Something Problem Please Reach Adminstrator";
                            return Json(loginResp);
                        }
                    }
                }
                HttpContext.Session.SetString(ConstValues.Username, UserName);
                Dictionary<string, string> requestBody = new Dictionary<string, string>();
                requestBody.Add(ConstValues.Username, UserName);
                requestBody.Add(ConstValues.Password, Password);
                using (HttpResponseMessage responseMessage = await _clientService.Login(requestBody))
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        responseMessage.EnsureSuccessStatusCode();
                        string stream = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                        loginResp = JsonConvert.DeserializeObject<LoginResponseModel>(stream);
                        if (loginResp.StatusCode == "000")
                        {
                            loginResp.MobileOTP = utility.GetStan();
                            DateTime otpCreationTime = DateTime.Now;
                            HttpContext.Session.SetString(ConstValues.SessionMobileNo, loginResp.MobileNo);
                            HttpContext.Session.SetString(ConstValues.SessionLoginOTP, loginResp.MobileOTP);
                            HttpContext.Session.SetString(ConstValues.Otptime, otpCreationTime.ToString());
                            _clientService.SendOTP(loginResp, loginResp.Token.encrypt());
                            if (!string.IsNullOrEmpty(loginResp.Token))
                            {
                                HttpContext.Session.SetString(ConstValues.JwtValue, loginResp.Token.encrypt());
                            }
                            HttpContext.Session.Remove(ConstValues.LoginAttempt);
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(HttpContext.Session.GetString(ConstValues.LoginAttempt)))
                            {
                                HttpContext.Session.SetString(ConstValues.LoginAttempt, (int.Parse(HttpContext.Session.GetString(ConstValues.LoginAttempt)) + 1).ToString());
                            }

                        }
                    }
                    return Json(loginResp);
                }
            }
            catch (Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json("1");}

          

        }
        [HttpPost]
        public async Task<IActionResult> ValidateOTP(string otp)
        {
            string ReceivedOTP = otp;
            string SentOTP = HttpContext.Session.GetString(ConstValues.SessionLoginOTP);
            string otpCreationTime = HttpContext.Session.GetString(ConstValues.Otptime);
            DateTime createdDateTime = DateTime.Parse(otpCreationTime);
            bool exp=HasExpired(createdDateTime);
            LoginResponseModel loginResps = new LoginResponseModel();
            try
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetString(ConstValues.OTPAttempt)))
                {
                    HttpContext.Session.SetString(ConstValues.OTPAttempt, "0");
                }
                if (HttpContext.Session.GetString(ConstValues.OTPAttempt) == "3")
                {
                    LoginModel Values = new LoginModel();
                    Values.UserName = HttpContext.Session.GetString(ConstValues.Username);

                    using (HttpResponseMessage responseMessages = _clientService.UnAuthorizedBlock(Values))
                    {
                        responseMessages.EnsureSuccessStatusCode();
                        string stream = responseMessages.Content.ReadAsStringAsync().Result.ToString();
                        loginResps = JsonConvert.DeserializeObject<LoginResponseModel>(stream);
                        if (loginResps.StatusCode == "000")
                        {
                            HttpContext.Session.Remove(ConstValues.OTPAttempt);
                            return Json("6");
                        }
                        else
                        {
                            return Json("5");
                        }
                    }
                }

                if (SentOTP != ReceivedOTP)
                {
                    if (!String.IsNullOrEmpty(HttpContext.Session.GetString(ConstValues.OTPAttempt)))
                    {
                        HttpContext.Session.SetString(ConstValues.OTPAttempt, (int.Parse(HttpContext.Session.GetString(ConstValues.OTPAttempt)) + 1).ToString());
                    }
                    return Json("1"); 
                }
                if (!exp)  
                {
                    string UserName = HttpContext.Session.GetString(ConstValues.Username);
                    string token = HttpContext.Session.GetString(ConstValues.JwtValue);
                    LoginModel values = new LoginModel();
                    values.UserName = UserName;
                    using (HttpResponseMessage responseMessage = _clientService.GetUserInfo(values, token))
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            responseMessage.EnsureSuccessStatusCode();
                            string stream = responseMessage.Content.ReadAsStringAsync().Result.ToString();
                            LoginResponseModel  loginResp = JsonConvert.DeserializeObject<LoginResponseModel>(stream);

                            if(loginResp.StatusCode!="000")
                            {
                                HttpContext.Session.Remove(ConstValues.OTPAttempt);
                                return Json("6");

                            }
                            else
                            {
                                ViewBag.Mobile = loginResp.MobileNo;
                                ViewBag.Email = loginResp.emailAddress;
                                HttpContext.Session.SetString(ConstValues.SessionEmailId, loginResp.emailAddress);
                                HttpContext.Session.SetString(ConstValues.SessionCustomerId, loginResp.CustomerId);
                                HttpContext.Session.SetString(ConstValues.SessionMobileNo, loginResp.MobileNo);
                                if (!string.IsNullOrEmpty(loginResp.UserType))
                                {
                                    HttpContext.Session.SetString(ConstValues.SessionUserType, loginResp.UserType);
                                }
                                if (!string.IsNullOrEmpty(loginResp.LoginName))
                                {
                                    string[] names = loginResp.LoginName.Split(' ');
                                    string firstName = names[0];
                                    HttpContext.Session.SetString(ConstValues.LoginName, loginResp.LoginName);
                                    HttpContext.Session.SetString(ConstValues.FirstName, firstName);

                                }
                                if (!string.IsNullOrEmpty(loginResp.vaccountid))
                                {
                                    HttpContext.Session.SetString(ConstValues.UserAccount, loginResp.vaccountid);
                                }
                                if (!string.IsNullOrEmpty(loginResp.EntityId))
                                {
                                    HttpContext.Session.SetString(ConstValues.EntityId, loginResp.EntityId.encrypt());
                                }

                                HttpContext.Session.Remove(ConstValues.OTPAttempt);
                            }                           
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(HttpContext.Session.GetString(ConstValues.OTPAttempt)))
                            {
                                HttpContext.Session.SetString(ConstValues.OTPAttempt, (int.Parse(HttpContext.Session.GetString(ConstValues.OTPAttempt)) + 1).ToString());
                            }

                        }
                    }
                    string user = HttpContext.Session.GetString(ConstValues.SessionUserType);
                    if (user == "1")
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, HttpContext.Session.GetString(ConstValues.LoginName)),
                         new Claim(ClaimTypes.Role, "admin") };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                           CookieAuthenticationDefaults.AuthenticationScheme,
                           new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = false });
                        return Json("admin");
                       
                    }
                    if (user == "4")
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name,HttpContext.Session.GetString(ConstValues.LoginName)),
                         new Claim(ClaimTypes.Role, "user") };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = false });
                        return Json("user");
                        
                    }
                    else {return Json("fail");}
                }
                else {return Json("2");}
            }
            catch (Exception ex){utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json("3");}
           
        
        }
        [HttpPost]
        public JsonResult ResentOTP()
        {
            HttpContext.Session.Remove(ConstValues.SessionLoginOTP);
            HttpContext.Session.Remove(ConstValues.Otptime);
            LoginResponseModel loginResp = new LoginResponseModel();
            loginResp.MobileOTP = utility.GetStan();
            loginResp.MobileNo = HttpContext.Session.GetString(ConstValues.SessionMobileNo);
            DateTime otpCreationTime = DateTime.Now;
            HttpContext.Session.SetString(ConstValues.SessionLoginOTP, loginResp.MobileOTP);
            HttpContext.Session.SetString(ConstValues.Otptime, otpCreationTime.ToString());
            string token = HttpContext.Session.GetString(ConstValues.JwtValue);
            try
            {
                _clientService.SendOTP(loginResp, token);
                return Json("1");
            }
            catch (Exception ex) {utility.ErrorLog(errorFolder, ex.Message.ToString()); return Json("2");}


        }
        [HttpPost]
        public async Task<JsonResult> signout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(string.Empty);
        }
    }
}
