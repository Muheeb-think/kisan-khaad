
using BAL.Services;
using DAL.MasterData;
using DAL.ViewModel;
using Jalaun.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Jalaun.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMasterDataBAL _bal;
        private readonly IMastersData _data;
        public AdminController(IMasterDataBAL bal, IMastersData data)
        {
            this._bal = bal;
            this._data = data;
        }
        public IActionResult VillageMaster()
        {
            VillageViewMaster villageView = new VillageViewMaster();
            villageView.Villages = _bal.GetVillageList(0);
            villageView.BlockList = _bal.SelectBlockList(0);
            return View(villageView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VillageMaster(VillageViewMaster villageView)
        {
            if (!ModelState.IsValid)
            {
                villageView.Villages = _bal.GetVillageList(0);
                villageView.BlockList = _bal.SelectBlockList(0);
                return View(villageView);
            }

            // UPDATE
            if (villageView.Id > 0)
            {
                int result = _data.VillageUpdate(villageView);
                if (result > 0)
                {
                    TempData["InsertVillage"] = "गांव अपडेट किया गया है";
                }
                return RedirectToAction("VillageMaster");
            }
            // INSERT
            else
            {
                int result = _data.InsertVillage(villageView);
                if (result > 0)
                {
                    TempData["InsertVillage"] = $"{villageView.VillageNameHindi} जोड़ दिया गया है!";
                }
                return RedirectToAction("VillageMaster");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteVillage(VillageViewMaster obj)
        {
            if (obj.Id <= 0)
                return Json(new { success = false, message = "गलत ID है" });

            int result = _data.VillageDelete(obj);
            if (result > 0)
                return Json(new { success = true, message = "गांव हटा दिया गया है" });

            return Json(new { success = false, message = "Delete failed" });
        }

        [HttpGet]
        public IActionResult CropMaster()
        {
            CropViewModel cropViewModel = new CropViewModel();
            cropViewModel.CropModels = _bal.SelectCropList();
            cropViewModel.seasonModels = _bal.SelectSeasonList();
            return View(cropViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CropMaster(CropViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                obj.CropModels = _bal.SelectCropList();
                obj.seasonModels = _bal.SelectSeasonList();
                return View(obj);
            }

            // UPDATE
            if (obj.CropId > 0)
            {
                int result = _data.InsertUpdateCropMaster(obj);
                if (result > 0)
                {
                    TempData["InsertCrop"] = "फसल अपडेट किया गया है";
                }
                return RedirectToAction("CropMaster");
            }
            // INSERT
            else
            {
                int result = _data.InsertUpdateCropMaster(obj);
                if (result > 0)
                {
                    TempData["InsertCrop"] = $"{obj.CropNameHindi} जोड़ दिया गया है!";
                }
                return RedirectToAction("CropMaster");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCrop(CropViewModel obj)
        {
            if (obj.CropId <= 0)
                return Json(new { success = false, message = "गलत ID है" });

            int result = _data.CropDelete(obj);
            if (result > 0)
                return Json(new { success = true, message = "फसल हटाई गई" });

            return Json(new { success = false, message = "Delete failed" });
        }
        public IActionResult FertilizerMaster()
        {
            FertilizerViewModel fertilizer = new FertilizerViewModel();
            fertilizer.CropModels = _bal.SelectCropList();
            fertilizer.Fertilizers = _bal.SelectFertilizerList();
            return View(fertilizer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FertilizerMaster(FertilizerViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                FertilizerViewModel fertilizer = new FertilizerViewModel();
                fertilizer.CropModels = _bal.SelectCropList();
                fertilizer.Fertilizers = _bal.SelectFertilizerList();
                return View(fertilizer);
            }

            // UPDATE
            if (obj.FertilizerId > 0)
            {
                int result = _data.FertilizerInsertUpdate(obj);
                if (result > 0)
                {
                    TempData["InsertFertilizer"] = "उर्वरक अपडेट किया गया है";
                }
                return RedirectToAction("FertilizerMaster");
            }
            // INSERT
            else
            {
                int result = _data.FertilizerInsertUpdate(obj);
                if (result > 0)
                {
                    TempData["InsertFertilizer"] = $"{obj.FertilizerNameEnglish} जोड़ दिया गया है!";
                }
                return RedirectToAction("FertilizerMaster");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFertilizer(FertilizerViewModel obj)
        {
            if (obj.FertilizerId <= 0)
                return Json(new { success = false, message = "गलत ID है" });

            int result = _data.FertilizerDelete(obj);
            if (result > 0)
                return Json(new { success = true, message = "फसल हटाई गई" });

            return Json(new { success = false, message = "Delete failed" });
        }

        public IActionResult FertilizerCropMapping()
        {
            FertilizerCropMappingViewModel fertilizerCrop = new FertilizerCropMappingViewModel();
            fertilizerCrop.CropModels = _bal.SelectCropList();
            fertilizerCrop.Fertilizers = _bal.SelectFertilizerList();
            fertilizerCrop.FertilizerCropMappings = _bal.fertilizerCropMappingModelsList();
            return View(fertilizerCrop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FertilizerCropMapping(FertilizerCropMappingViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                FertilizerCropMappingViewModel fertilizerCrop = new FertilizerCropMappingViewModel();
                fertilizerCrop.CropModels = _bal.SelectCropList();
                fertilizerCrop.Fertilizers = _bal.SelectFertilizerList();
                fertilizerCrop.FertilizerCropMappings = _bal.fertilizerCropMappingModelsList();
                return View(fertilizerCrop);
            }

            // UPDATE
            if (obj.FertilizerId > 0)
            {
                int result = _data.InsertUpdateCropFertilizerMapping(obj);
                if (result > 0)
                {
                    TempData["InsertFertilizer"] = "फसल-उर्वरक मैपिंग अपडेट किया गया है";
                }
                return RedirectToAction("InsertFertilizerMapping");
            }
            // INSERT
            else
            {
                int result = _data.InsertUpdateCropFertilizerMapping(obj);
                if (result > 0)
                {
                    TempData["InsertFertilizerMapping"] = "फसल-उर्वरक मैपिंग सफलतापूर्वक अपडेट की गई है।";

                }
                return RedirectToAction("FertilizerMaster");
            }
        }

        #region Meheeb sir
        [HttpGet]
        public async Task<IActionResult> GetTehsilMaster()
        {
            List<TehsilViewModel> vm = new();
            var res = _data.SelectTehsil();
            vm = BAL.Common.DataTableExtensions.ToList<TehsilViewModel>(res.Tables[0]);
            return PartialView("/Views/Shared/_partialTehsilMaster.cshtml", vm);
        }
        [HttpGet]
        public async Task<IActionResult> TehsilMaster()
        {
            TehsilViewModel vm = new();
            //var response = await _api.GetTehsilList(TehsilId);
            //var data = (DataTable)response.Data;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTehsil(TehsilViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = _data.SaveTehsil(model);
                    if (response == 1)
                        TempData["msg"] = "Tehsil inserted successfully";
                    else
                    {
                        TempData["msg"] = "Error in saving Tehsil";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("TehsilMaster");
        }
        [HttpGet]
        public async Task<IActionResult> CreateSociety()
        {
            SocietyViewModel model = new();
            var res = _data.SelectTehsil();
            model.ddltehsil = BAL.Common.DataTableExtensions.ToList<TehsilViewModel>(res.Tables[0]);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateSociety(SocietyViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = _data.SelectTehsil();
                    model.ddltehsil = BAL.Common.DataTableExtensions.ToList<TehsilViewModel>(res.Tables[0]);
                    TempData["msg"] = "Society added successfully";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("CreateSociety");
            }

            return View(model);
        }
        //[HttpGet]
        //public ActionResult GetSociety(int? id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var res = _data.SelectTehsil();
        //            model.ddltehsil = BAL.Common.DataTableExtensions.ToList<TehsilViewModel>(res.Tables[0]);
        //            TempData["msg"] = "Society added successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        return RedirectToAction("CreateSociety");
        //    }

        //    return View(model);
        //}
        #endregion

    }
}
