
using BAL.Services;
using DAL.Repo;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            DataTable dt = _repo.FarmerRegistration(model);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Message"].ToString() == "1")
                {
                    TempData["Message"] = "आपका आवेदन सफलतापूर्वक प्राप्त कर लिया गया है।";
                }
                else
                {
                    TempData["Message"] = "आपका आवेदन पहले ही जमा किया जा चुका है।";
                }
                return RedirectToAction("Registration");
            }
            return View();
        }


        [HttpGet]
        public IActionResult GetAreaTypesByTehsil(int tahsilId)
        {
            List<BlockModel> blockList = _bal.SelectBlockList(tahsilId);
            return Json(blockList);
        }

        [HttpGet]
        public IActionResult GetVillageByBlock(int BlockId)
        {
            List<Village> villageList = _bal.GetVillageList(BlockId);
            return Json(villageList);
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
    }
}
