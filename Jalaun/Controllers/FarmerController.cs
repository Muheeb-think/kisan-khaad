
using BAL.Services;
using DAL.Repo;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Utilities;
using static Azure.Core.HttpHeader;
using static System.Net.WebRequestMethods;

namespace Jalaun.Controllers
{
    public class FarmerController : Controller
    {
        private readonly IMasterDataBAL _bal;
        private readonly IRegistration _repo;

        public FarmerController(IMasterDataBAL bal, IRegistration repo)
        {
            this._repo = repo;
            this._bal = bal;
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

        #region Mukeem
        [HttpGet]
        public IActionResult FertilizerDemand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchFarmerById([FromBody] FarmerSearchRequest request)
        {
            try
            {
                request.Action = "FarmerDetailById";
                var data = _repo.FarmerDetailsbyId(request);

                var farmer = data.AsEnumerable().Select(dr => new
                {
                    FarmerName = dr["FarmerName"]?.ToString(),
                    VillageNameHi = dr["VillageNameHi"]?.ToString(),
                    CropArea = dr["CropArea"]?.ToString()
                }).FirstOrDefault();

                return Ok(new { status = true, data = farmer });
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
        #endregion

        #region update farmer profile 

        //public IActionResult FarmerProfileUpdate()
        //{
        //    UserProfileViewModel model = new UserProfileViewModel();
        //    model.TehsilList = _bal.SelectTehsilList();
        //    return View(model);
        //}
        #endregion
    }
}
