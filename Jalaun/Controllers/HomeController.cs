using BAL;
using DAL.ViewModel;
using Jalaun.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace Jalaun.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonLogics _commonLogic;

        public HomeController(
            ILogger<HomeController> logger,
            ICommonLogics commonLogic)
        {
            _logger = logger;
            _commonLogic = commonLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult FillDropdown([FromBody] dropdownBinderModel obj)
        
        {
            try
            {
                var data = _commonLogic.DropDownBind(obj);

                var list = data.AsEnumerable().Select(dr => new
                {
                    id = Convert.ToInt32(dr["Id"]),
                    value = dr["Value"].ToString()
                }).ToList();

                return Ok(new { status = true, data = list });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
        public ActionResult About_Us()
        {

            return View();
        }

        public ActionResult Contact_Us()
        {

            return View();
        }

    }
}
