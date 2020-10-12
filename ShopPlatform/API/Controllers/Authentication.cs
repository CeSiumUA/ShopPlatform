using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShopPlatform.API.Controllers
{
    [ApiController]
    public class Authentication : ControllerBase
    {
        public Authentication()
        {

        }

        [HttpPost("api/authentication/register")]
        public async Task<IActionResult> Register([FromBody] )
        {

            return new JsonResult(null);
        }
    }
}
