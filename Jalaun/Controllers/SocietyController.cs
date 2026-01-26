using BAL.Common;
using BAL.Services;
using DAL.MasterData;
using DAL.ViewModel;
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
    }
}
