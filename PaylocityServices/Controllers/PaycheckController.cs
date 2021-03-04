using Foundation.DTO;
using Foundation.WebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private Data.DataContext _context;

        public PaycheckController(Data.DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Produces(typeof(List<Paycheck>))]
        public async Task<IActionResult> GetAll()
        {
            List<Data.Paycheck> model = _context.Paycheck.Where(x => x.isDeleted == false && x.IssueDate == null).Include(x => x.Employee).ToList();
            return Ok(model);
        }

        [Route("{paycheckID:int}"), HttpGet]
        [Produces(typeof(Paycheck))]
        public async Task<IActionResult> Get(int paycheckID)
        {
            Data.Paycheck model = _context.Paycheck.Include(x => x.Employee).FirstOrDefault(x => x.PaycheckID == paycheckID);
            return Ok(model);
        }

        [HttpPost]
        [Produces(typeof(Paycheck))]
        public async Task<IActionResult> Post([FromBody]Paycheck model)
        {
            Data.Paycheck data = new Data.Paycheck();
            data.SetDuplicateProperties(model);
            _context.Paycheck.Add(data);

            data.CreateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [Route("{paycheckID:int}"), HttpPut]
        [Produces(typeof(Paycheck))]
        public async Task<IActionResult> Put(int paycheckID, [FromBody]Paycheck model)
        {
            Data.Paycheck data = _context.Paycheck.FirstOrDefault(x => x.PaycheckID == paycheckID);

            if (data == null)
                return NotFound();

            data.SetDuplicateProperties(model);
            data.ChangeDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        /// <summary>
        /// Set isDelete to true
        /// </summary>
        /// <param name="selectionID"></param>
        /// <returns></returns>
        [Route("{paycheckID:int}"), HttpDelete]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Delete(int paycheckID)
        {
            Data.Paycheck data = _context.Paycheck.FirstOrDefault(x => x.PaycheckID == paycheckID);

            if (data == null)
                return NotFound();

            data.isDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(true);
        }
    }
}
