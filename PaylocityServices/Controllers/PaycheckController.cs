using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaycheckController : ControllerBase
    {
        [Route("{selectionID}"), HttpGet]
        [Produces(typeof(Paycheck))]
        public async Task<IActionResult> Get()
        {

            return Ok();
        }
    }
}
