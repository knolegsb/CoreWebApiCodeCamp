using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class CampsController : Controller
    {
        //public object Get()
        //{
        //    return new { Name = "Shawn", FavoriteColor = "Blue" };
        //}

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new { Name = "Shawn", FavoriteColor = "Blue" });
        }
    }
}
