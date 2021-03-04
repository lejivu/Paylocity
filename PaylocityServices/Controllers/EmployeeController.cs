using Foundation.DTO;
using Foundation.WebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private Data.DataContext _context;

        public EmployeeController(Data.DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Produces(typeof(List<Employee>))]
        public async Task<IActionResult> GetAll()
        {
            List<Data.Employee> data = _context.Employee.Include(x => x.Dependents).Where(x => x.isDeleted == false).ToList();

            return Ok(data);
        }

        [Route("{employeeID:int}"), HttpGet]
        [Produces(typeof(Employee))]
        public async Task<IActionResult> Get(int employeeID)
        {
            Data.Employee model = _context.Employee.Include(x => x.Dependents).FirstOrDefault(x => x.EmployeeID == employeeID);
            return Ok(model);
        }

        [HttpPost]
        [Produces(typeof(Employee))]
        public async Task<IActionResult> Post([FromBody]Employee model)
        {
            Data.Employee data = new Data.Employee();
            data.SetDuplicateProperties(model);
            _context.Employee.Add(data);
            data.CreateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            if (model.Dependents.Count > 0)
            {
                foreach (var person in model.Dependents)
                {
                    Data.Dependent dependent = new Data.Dependent();
                    dependent.EmployeeID = data.EmployeeID;
                    dependent.Name = person.Name;
                    _context.Dependent.Add(dependent);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [Route("{EmployeeID:int}"), HttpPut]
        [Produces(typeof(Employee))]
        public async Task<IActionResult> Put(int EmployeeID, [FromBody] Employee model)
        {
            Data.Employee data = _context.Employee.FirstOrDefault(x => x.EmployeeID == EmployeeID);

            if (data == null)
                return NotFound();

            data.SetDuplicateProperties(model);

            await _context.SaveChangesAsync();

            foreach (var dependent in model.Dependents)
            {
                Data.Dependent isExisting = _context.Dependent.Where(x => x.Name == dependent.Name && x.EmployeeID == model.EmployeeID).FirstOrDefault();

                if (isExisting != null)
                {
                    //Set additional properties here
                    isExisting.Name = dependent.Name;
                } 
                else
                {
                    Data.Dependent d = new Data.Dependent();
                    d.EmployeeID = model.EmployeeID;
                    d.Name = dependent.Name;
                    _context.Dependent.Add(d);
                }

                await _context.SaveChangesAsync();
            }
            return Ok(data);
        }

        /// <summary>
        /// Set isDelete to true
        /// </summary>
        /// <param name="selectionID"></param>
        /// <returns></returns>
        [Route("{EmployeeID:int}"), HttpDelete]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Delete(int EmployeeID)
        {
            Data.Employee data = _context.Employee.FirstOrDefault(x => x.EmployeeID == EmployeeID);

            if (data == null)
                return NotFound();

            data.isDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(true);
        }
    }
}
