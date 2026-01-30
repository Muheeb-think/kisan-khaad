
using Azure.Core;
using BAL;
using BAL.Services;
using DAL.ModelDTO;
using DAL.Repo;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using Utilities;
using Utility;
using static Azure.Core.HttpHeader;
using static System.Net.WebRequestMethods;

namespace Jalaun.Controllers
{
    public class FarmerController : Controller
    {
        private readonly IMasterDataBAL _bal;
        private readonly IRegistration _repo;
        private readonly IFarmerDataBAL _farmerbal;
        private readonly ICommonLogics _commonLogic;

        public FarmerController(IMasterDataBAL bal, IRegistration repo, IFarmerDataBAL farmerbal,ICommonLogics commonLogics)
        {
            this._repo = repo;
            this._bal = bal;
            this._farmerbal = farmerbal;
            this._commonLogic= commonLogics;
        }
        public IActionResult Registration()
        {
            RegisterViewModel model = _bal.SelectTehsilandFarmerCateogryList();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Registration(RegisterViewModel model)
        {
            RegisterViewModel data = _bal.SelectTehsilandFarmerCateogryList();
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            int result = _repo.FarmerRegistration(model);
            if (result > 0)
            {
                TempData["Message"] = "आपका पंजीकरण सफलतापूर्वक पूरा हो गया है। कृपया लॉगिन करें।";
                return RedirectToAction("Login", "Auth");
            }
            else if (result == -10)
            {
                TempData["Message"] = "आपका आवेदन पहले ही जमा किया जा चुका है।";
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                TempData["Message"] = "आपका आवेदन जमा नहीं हो सका।";
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult SearchFarmerByIdForAgriStack([FromBody] FarmerSearchRequest request)
        {
            try
            {
                request.Action = "AgriStackData";
                var data = _repo.FarmerDetailsbyId(request);
                if (data.Rows[0]["result"].ToString() == "-10")
                {
                    return Ok(new { status = true, data = data.Rows[0]["result"].ToString() });
                }
                var farmer = data.AsEnumerable().Select(dr => new
                {
                    FarmerName = dr["FarmerName"]?.ToString(),//       
                    DOB = dr["DOB"]?.ToString(),//
                    FatherOrHusbandName = dr["FatherOrHusbandName"]?.ToString(),//
                    AadharNo = dr["AadharNo"]?.ToString(),//
                    MobileNo = dr["MobileNo"]?.ToString(),//
                    TehsilId = dr["TehsilId"]?.ToString(),
                    TehsilNameHi = dr["TehsilNameHi"]?.ToString(),
                    BlockId = dr["BlockId"]?.ToString(),
                    Gender = dr["Gender"]?.ToString(),
                    BlockName = dr["BlockName"]?.ToString(),
                    VillageId = dr["VillageId"]?.ToString(),
                    VillageNameHi = dr["VillageNameHi"]?.ToString(),
                    Pincode = dr["Pincode"]?.ToString(),
                    FullAddresss = dr["FullAddresss"]?.ToString(),
                }).FirstOrDefault();
                return Ok(new { status = true, data = farmer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SearchKhasraNo([FromBody] FarmerSearchRequest request)
        {
            try
            {
                request.Action = "KhasraNo";
                var data = _repo.FarmerDetailsbyId(request);

                var farmer = data.AsEnumerable().Select(dr => new
                {
                    TotalArea = dr["TotalArea"]?.ToString(),//       
                    FarmerShareArea = dr["FarmerShareArea"]?.ToString(),//
                    KhatauniNo = dr["KhatauniNo"]?.ToString(),
                    FarmerNameLand = dr["FarmerName"].ToString()

                }).FirstOrDefault();
                return Ok(new { status = true, data = farmer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAreaTypesByTehsil(int tahsilId)
        {
            List<BlockModel> blockList = _bal.SelectBlockList(tahsilId);
            return Json(blockList);
        }

        [HttpGet]
        public IActionResult GetVillageByTehsil(int? tahsilId)
        {
            List<VillageModel> blockList = _bal.GetVillageByTehsil(tahsilId);
            return Json(blockList);
        }
        [HttpGet]
        public IActionResult GetVillageByBlock(int BlockId)
        {
            List<Village> villageList = _bal.GetVillageList(BlockId);
            return Json(villageList);
        }

        [HttpGet]
        public IActionResult GetOTP()
        {
            string OTP = GenrateRandomNumber.GenerateOTP();
            HttpContext.Session.SetString("OTP", OTP);
            return Json(OTP);
        }
        [HttpPost]
        public IActionResult VerifyOTP(string otp)
        {
            var sessionOtp = HttpContext.Session.GetString("OTP");
            return Json(otp == sessionOtp);
        }


        #region update farmer profile 
        [HttpGet]
        public IActionResult FarmerProfileUpdate()
        {
            try
            {
                UserProfileViewModel model = new UserProfileViewModel();
                model.TehsilList = _bal.SelectTehsilList();
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult FarmerProfileUpdate(UserProfileViewModel obj)
        {
            try
            {              
                int result = _farmerbal.UpdateCorrespondenceFarmerData(obj);
                if (result > 0)
                {
                    TempData["Message"] = "आपकी प्रोफ़ाइल सफलतापूर्वक अपडेट हो गई है।";
                }
                else
                {
                    TempData["Message"] = "प्रोफ़ाइल अपडेट नहीं हो पाई। कृपया पुनः प्रयास करें।";
                }
                UserProfileViewModel model = new UserProfileViewModel();
                model.TehsilList = _bal.SelectTehsilList();
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult GetCorrespondenceFarmerData()
        {
            try
            {
                List<FarmerProfile> profilelist = _farmerbal.SelectCorrespondenceFarmerData();
                return Ok(new { status = true, data = profilelist });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
        #endregion

        #region Farmer khad demands
        public IActionResult ApproveDemand()
        {
            try
            {
                string? action = "Success";
                List<FarmerFertilizerDemandDto> farmerFertilizer = new();
                farmerFertilizer = _farmerbal.GetFarmerFertilizerDemand(action);
                return View(farmerFertilizer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Farmerdashboard: " + ex.Message);
                return View("Error", ex);
            }
            finally
            {
                Console.WriteLine("Farmerdashboard action completed.");
            }
        }

        public IActionResult PendingFarmerFertilizerDemand()
        {
            try
            {
                string? action = "Pending";
                List<FarmerFertilizerDemandDto> farmerFertilizer = new();
                farmerFertilizer = _farmerbal.GetFarmerFertilizerDemand(action);
                return View(farmerFertilizer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Farmerdashboard: " + ex.Message);
                return View("Error", ex);
            }
            finally
            {
                Console.WriteLine("Farmerdashboard action completed.");
            }
        }
        #endregion

        #region dashboard
        [HttpGet]
        public IActionResult Farmerdashboard() 
        {
            try
            {
                FarmerDashboardDto dashboardDto = new();
                dashboardDto = _farmerbal.GetFarmerDashboard();
                return View(dashboardDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Farmerdashboard: " + ex.Message);
                return View("Error", ex);
            }
            finally
            {
                Console.WriteLine("Farmerdashboard action completed.");
            }
        }

        #endregion

        #region FarmerFertilizerDemand form 

        [HttpGet]
        public IActionResult FarmerFertilizerDemand()
        {
            dropdownBinderModel obj = new dropdownBinderModel();
            obj.Action = "GetSeason";
            DataTable dt = _commonLogic.DropDownBind(obj);
            ViewBag.SeasonId = Convert.ToInt32(dt.Rows[0]["id"]);
            ViewBag.SeasonName = dt.Rows[0]["value"].ToString();
            ViewBag.FarmerId = HttpContext.Session.GetString("FarmerId");
            return View();
        }
        #endregion

        #region Mukeem
        [HttpGet]
        public IActionResult FertilizerDemand()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FertilizerDemandNew()
        {
            dropdownBinderModel obj = new dropdownBinderModel();
            obj.Action = "GetSeason";
            DataTable dt = _commonLogic.DropDownBind(obj);
            ViewBag.SeasonId = Convert.ToInt32(dt.Rows[0]["id"]);
            ViewBag.SeasonName = dt.Rows[0]["value"].ToString();
            return View();
        }

        [HttpPost]
        public IActionResult SearchFarmerById([FromBody] FarmerSearchRequest request)
        {
            try
            {
                TempraryDemand temdemad = new TempraryDemand();
                request.Action = "FarmerDetailById";
                var data = _repo.FarmerDetailsbyId(request);

                temdemad.FarmerId = request.FarmerId;
                temdemad.Action = "Tempdetail";
                var Tempdetail = _repo.FertilizerDemand(temdemad);

                var farmer = data.AsEnumerable().Select(dr => new
                {
                    FarmerName = dr["FarmerName"]?.ToString(),
                    VillageNameHi = dr["VillageNameHi"]?.ToString(),
                    CropArea = dr["CropArea"]?.ToString()
                }).FirstOrDefault();

                return Ok(new { status = true, data = farmer, Tempdetail });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult FertilizerDetailsById([FromBody] FertilizerDetails obj)
        {
            try
            {
                obj.Action = "FertilizerListByCrop";
                var data = _repo.FertilizerDetailsById(obj);

                var fertilizerList = data.AsEnumerable().Select(dr => new
                {
                    FertilizerNameHindi = dr["FertilizerNameHindi"]?.ToString(),
                    FertilizerCode = dr["FertilizerCode"]?.ToString(),
                    DoseQty = Convert.ToDecimal(dr["Dose_per_hectare_kg"]),
                    Rate = Convert.ToDecimal(dr["FertilizerRatePerKg"])
                }).ToList();

                return Ok(new { status = true, data = fertilizerList });


            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }


        public IActionResult AddTempraryDemand([FromBody] TempraryDemand request)
        {
            var response = _repo.FertilizerDemand(request);
            return Ok(response);
        }
        #region calculate name percantage
        [HttpPost]
        public IActionResult GetFarmerNameMatch([FromBody] UserNameMatch obj)
        {
            double similarity = UserNameMatchBAL.NameSimilarity(obj.AgriStackName, obj.KhasraName);
            Console.WriteLine($"Name match: {similarity:F2}%");

            if (similarity >= 80)
            {
                Console.WriteLine("Proceed to next step.");
                return Json(new
                {
                    status = true,
                    data = similarity,
                });
            }
            else
            {
                Console.WriteLine("Popup: Names do not match enough.");
                return Json(new
                {
                    status = false,
                    msg = similarity,
                });
            }
        }
        #endregion
        #endregion
    }
}
