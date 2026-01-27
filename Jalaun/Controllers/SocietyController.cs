using BAL.Common;
using BAL.Services;
using DAL.MasterData;
using DAL.ViewModel;
using Jalaun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jalaun.Controllers
{
    [Authorize(Roles = Constant.संस्था)]
    public class SocietyController : Controller
    {
        private readonly IMasterDataBAL _bal;
        private readonly IMastersData _data;

        public SocietyController(IMasterDataBAL bal, IMastersData data)
        {
            this._bal = bal;
            this._data = data;
        }
        [HttpGet]
        public IActionResult Index()
        {
            FertilizerDemandVM model = new FertilizerDemandVM();
            var result = _data.GetSocietyDashboardData(Convert.ToInt64(SessionHelper.UserMobile));

            model.DashboardDT = result.Tables[0];
            //model.FertilizerDemandVillageWise = BAL.Common.DataTableExtensions.ToList<FertilizerDemandVM>(result.Tables[1]);
            model.FertilizerDemandFertilizerWise = BAL.Common.DataTableExtensions.ToList<FertilizerDemandVM>(result.Tables[2]);

            return View(model);
        }


        [HttpGet]
        public IActionResult StockEntry()
        {
            FertilizerStockVM model = new();
            
            
            return View(model);
        }
        [HttpPost]
        public IActionResult StockEntry(FertilizerStockVM model)
        {
            

            if (!ModelState.IsValid)
            {
               
                return View(model);
            }

            var res = _data.SaveFertilizerStock(model, Convert.ToInt32(SessionHelper.UserId));

            TempData["Success"] = model.StockID == 0
                ? "Stock added successfully"
                : "Stock updated successfully";

            return RedirectToAction("StockEntry");
        }

        [HttpGet]
        public IActionResult Distribute()
        {
            DistributeVM model = new();
            return View(model);
        }
        [HttpPost]
        public IActionResult Distribute(DistributeVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _data.DistributeFertilizer(model, Convert.ToInt32(SessionHelper.UserId));

            TempData["Success"] = "Fertilizer distributed successfully";
            return RedirectToAction("DemandList");

        }

    }
}
