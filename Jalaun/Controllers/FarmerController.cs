using BAL;
using BAL.MasterData;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Jalaun.Controllers
{
    public class FarmerController : Controller
    {
        private readonly IMastersData _data;
        private readonly IRegistration _bal;
        public FarmerController(IMastersData data,IRegistration bal)
        {
            this._data = data;
            this._bal  = bal;
        }
        public IActionResult Registration()
        {
            DataSet ds = _data.SelectTehsil();

            var tehsilList = new List<TehsilModel>();
            var farmerCategoryList = new List<FarmerCategoryModel>();

            // Tehsil List
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tehsilList.Add(new TehsilModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    TehsilName = row["TehsilName"].ToString()
                });
            }

            // Farmer Category List
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                farmerCategoryList.Add(new FarmerCategoryModel
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryNameHindi = row["CategoryNameHindi"].ToString()
                });
            }

            //  Single Model Creation
            RegisterViewModel model = new RegisterViewModel
            {
                TehsilList = tehsilList,
                FarmerCategoryList = farmerCategoryList,
                DOB = null
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Registration(RegisterViewModel model)
        {
            DataTable dt=_bal.FarmerRegistration(model);
            if(dt.Rows.Count > 0)
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
        public IActionResult TempForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAreaTypesByTehsil(int tahsilId)
        {
            DataTable dt = _data.SelectBlock(tahsilId);
            var blockList = new List<BlockModel>();

            foreach (DataRow row in dt.Rows)
            {
                blockList.Add(new BlockModel
                {
                    BlockId = Convert.ToInt32(row["BlockId"]),
                    BlockName = row["BlockName"].ToString() 
                });
            }
            return Json(blockList);
        }

        [HttpGet]
        public IActionResult GetVillageByBlock(int BlockId)
        {
            DataTable dt = _data.SelectVillageMaster(BlockId);
            var blockList = new List<VillageModel>();

            foreach (DataRow row in dt.Rows)
            {
                blockList.Add(new VillageModel
                {
                    VillageId = Convert.ToInt32(row["VillageId"]),
                    VillageName = row["VillageName"].ToString()
                });
            }
            return Json(blockList);
        }

    }
}
