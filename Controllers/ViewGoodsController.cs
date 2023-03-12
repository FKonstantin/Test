using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestFisenko.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ViewGoodsController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}