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

            model.CreatedBy = Convert.ToInt32(SessionHelper.UserId);
            model.dtfertilizer = _data.GetfertilzerdemandByFarmer(Convert.ToInt32(SessionHelper.UserId), 0, 0, 2);
            return View(model);
        }

        [HttpPost]
        public IActionResult Save([FromBody] List<FertilizerStockVM> model)
        {
            bool status = false;
            foreach (var item in model)
            {
                var res = _data.SaveFertilizerStock(item, Convert.ToInt32(SessionHelper.UserId));
                status = res > 0 ? true : false;

            }

            return Json(new { success = status });
        }

        [HttpGet]
        public IActionResult Distribute()
        {
            DistributeVM model = new();
            model.dtfertilizer = _data.GetfertilzerdemandByFarmer(Convert.ToInt32(SessionHelper.UserId), 0, 0, 2);
            return View(model);
        }
        [HttpPost]
        public IActionResult Distribute(DistributeVM model)
        {
            model.SocietyId = Convert.ToInt32(SessionHelper.UserId);
            if (!ModelState.IsValid)
                return View(model);

            _data.DistributeFertilizer(model, Convert.ToInt32(SessionHelper.UserId));

            TempData["Success"] = "Fertilizer distributed successfully";
            return RedirectToAction("Distribute");

        }
        public ActionResult GetAvailableStock(int fertilizerId)
        {
            string value = "0";
            value = _data.GetAvailableStockBySociety(Convert.ToInt32(SessionHelper.UserId), fertilizerId);
            return Json(new { stock = value });
        }

        public ActionResult GetfertilzerdemandByFarmer(int fertilizerId, int farmerId)
        {
            DistributeVM model = new();
            var resultdt = _data.GetfertilzerdemandByFarmer(Convert.ToInt32(SessionHelper.UserId), fertilizerId, farmerId);
            if (resultdt != null && resultdt.Rows.Count > 0)
            {
                model = new();
                model.DemandDetailsId = Convert.ToInt32(resultdt.Rows[0]["DemandDetailsId"]);
                model.FertilizerNeed = Convert.ToDecimal(resultdt.Rows[0]["FertilizerNeed"]);
                model.ReceiveQty = Convert.ToDecimal(resultdt.Rows[0]["RecieveQty"]);
            }
            return Json(model);
        }

        public ActionResult GetDistributionListByFarmerAndFertilizer(int Farmerid, int FertilizerId)
        {
            var resultdt = _data.GetDistributionListByFarmerAndFertilizer(Farmerid, FertilizerId,
                Convert.ToInt32(SessionHelper.UserId), 0);
            var model = BAL.Common.DataTableExtensions.ToList<FarmerDemandReportViewModel>(resultdt);
            return PartialView("/Views/Shared/_fertilizerDistributionListBySociety.cshtml", resultdt);
        }
        public ActionResult GetDistributionListBySociety()
        {
            var resultdt = _data.GetDistributionListByFarmerAndFertilizer(0, 0,
                Convert.ToInt32(SessionHelper.UserId), 0);
            var model = BAL.Common.DataTableExtensions.ToList<FarmerDemandReportViewModel>(resultdt);
            return PartialView("/Views/Shared/_fertilizerDistributionListBySociety.cshtml", resultdt);
        }
        [HttpGet]
        public ActionResult GetfertilzerdemandDetailsByFarmer(int farmerId)
        {
            var resultdt = _data.GetfertilzerdemandByFarmer(Convert.ToInt32(SessionHelper.UserId), 0, farmerId, 1);

            return PartialView("/Views/Shared/_fertilizerDistributionListBySociety.cshtml", resultdt);

        }


    }
}
