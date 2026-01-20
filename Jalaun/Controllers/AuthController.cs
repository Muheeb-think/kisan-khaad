using DAL.ModelDTO;
using DAL.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using Utilities;
using Utilities.EnumClasses;

namespace Jalaun.Controllers
{
    public class AuthController : Controller
    {
        private readonly IDataAccess _dataAccess;

        public AuthController(IDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }
        public IActionResult Login()
        {
            LoginDTO loginModel = new LoginDTO();
            //var seed = SecurityBAL.GetUniqueKey(8);
            //loginModel.SeedKey = seed;
            loginModel.CaptchaImg = GetCaptchaImage();
            return View(loginModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginDTO logindto)
        {
            try
            {
                LoginDTO loginModel = new LoginDTO();
                if (!Captcha.ValidateCaptchaCode(logindto.CaptchaCode, HttpContext))
                {
                    ModelState.AddModelError("CaptchaCode", "InValid Captcha");
                    loginModel.CaptchaImg = GetCaptchaImage();
                    return View(loginModel);
                }
                if (logindto.Mobile == null || logindto.Mobile == "")
                {
                    ModelState.AddModelError("Mobile", "Mobile number requried !!");
                    loginModel.CaptchaImg = GetCaptchaImage();
                    return View(loginModel);
                }
                if (logindto.Password == null || logindto.Password == "")
                {
                    ModelState.AddModelError("Password", "Password number requried !!");
                    loginModel.CaptchaImg = GetCaptchaImage();
                    return View(loginModel);
                }

                DataTable dt = _dataAccess.UserAuthenticate(logindto);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("CaptchaCode", "Invalid username or password");
                loginModel.CaptchaImg = GetCaptchaImage();
                return View(loginModel);
            }
            catch (Exception ex)
            {
                LoginDTO loginModel = new LoginDTO();
                ModelState.AddModelError("CaptchaCode", "An unexpected error occurred.");
                loginModel.CaptchaImg = GetCaptchaImage();
                return View(loginModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCaptchaImageRefresh()
        {
            try
            {
                var val = GetCaptchaImage();
                return Ok(val);
            }
            catch (Exception ex)
            {
                string[] St = ex.StackTrace.Split('\n');
                string logSt = St[St.Length - 1];
                //logger.Log(LogLevel.Critical, ex, ex.Message, logSt);
                return StatusCode(401);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #region Non Action 
        public string GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            string base64String = Convert.ToBase64String(result.CaptchaByteData, 0, result.CaptchaByteData.Length);
            var ImageUrl = "data:image/png;base64," + base64String;
            return ImageUrl;//new FileStreamResult(s, "image/png");
        }
        #endregion
    }
}
