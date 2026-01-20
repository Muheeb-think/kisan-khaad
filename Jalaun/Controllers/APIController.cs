
using DAL.SqlHeplers;
using DAL.ViewModel;
using Jalaun.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Jalaun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        //private readonly IMastersData _mastersData;
        
       

        //[HttpGet]
        //[Route("tehsil-list{Tehsilid:int}")]
        //public async Task<IActionResult> GetTehsil(int? Tehsilid)
        //{
        //    ApiResponse res = new ApiResponse();
        //    try
        //    {
        //        var data = _mastersData.SelectTehsil();

        //        res.Data = data;
        //        res.Status = true;
        //        res.StatusCode = (int)HttpStatusCode.OK;
        //        res.Message = "Tehsil list fetched successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Status = false;
        //        res.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        res.Message = ex.Message;
        //    }
        //    return Ok(res);

        //}
        //[HttpPost]
        //[Route("save-tehsil")]
        //public async Task<IActionResult> CreateTehsil(TehsilViewModel model)
        //{
        //    ApiResponse res = new ApiResponse();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var data = _mastersData.SaveTehsil(model);
        //            res.Data = data;
        //            res.Status = true;
        //            res.StatusCode = (int)HttpStatusCode.OK;
        //            res.Message = "Tehsil saved successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Status = false;
        //        res.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        res.Message = ex.Message;
        //    }
        //    return Ok(res);
        //}
    }
}
