using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Foundation.DTO;
using Paylocity.Models;

namespace Paylocity.Controllers
{
    public class EmployeeController : Controller
    {
        private ServiceHelper _ServiceHelper;
        public EmployeeController(ServiceHelper helper)
        {
            _ServiceHelper = helper;
        }
        public async Task<IActionResult> Index()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.employees  = await _ServiceHelper.GetAsync<List<Employee>>("Employee");

            //Calculate Employer Expenses
            foreach (var data in model.employees.Where(x => x.isEmployerPackagePaid).ToList())
            {
                //Calculate Employer's total Salary Expense
                model.TotalSalaryExpense += data.AnnualSalary;

                //Calculate Employer's cost per Employee
                decimal totalCost = 1000;
                if (data.FirstName.Trim().ToLower().StartsWith('a'))
                    totalCost = totalCost * 90 / 100;

                model.TotalPaidPackageExpense += totalCost;
                foreach (var person in data.Dependents)
                {
                    //Calculate
                    decimal dependentCost = 500;
                    if (person.Name.Trim().ToLower().StartsWith('a'))
                        dependentCost = dependentCost * 90 / 100;

                    model.TotalPaidPackageExpense += dependentCost;
                }
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id = 0)
        {
            EmployeeViewModel model = new EmployeeViewModel();

            if (id > 0) //Update existing
                model.employee = await _ServiceHelper.GetAsync<Employee>($"Employee/{id}");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel model)
        {
            model.employee.Dependents = model.employee.Dependents.Where(x => !string.IsNullOrWhiteSpace(x.Name)).ToList();

            if (!model.employee.isEmployerPackagePaid)
            {
                model.employee.AnnualPackageExpense = 0;
                model.employee.NetPay = model.employee.AnnualSalary;
            }

            if (model.employee.EmployeeID > 0)
                model.employee = await _ServiceHelper.PutAsync<Employee>($"Employee/{model.employee.EmployeeID}", model.employee);
            else
                model.employee = await _ServiceHelper.PostAsync<Employee>("Employee", model.employee);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CalculateEmployerExpenses(EmployeeViewModel model)
        {
            decimal totalCost = 1000;

            if (model.employee.FirstName.Trim().ToLower().StartsWith('a'))
                totalCost = totalCost * 90 / 100;
            
            foreach (var person in model.employee.Dependents)
            {
                decimal cost = 500;
                if (person.Name.Trim().ToLower().StartsWith('a'))
                    cost = cost * 90 / 100;

                totalCost += cost;
            }

            model.employee.AnnualPackageExpense = totalCost;
            model.employee.NetPay = model.employee.AnnualSalary - totalCost;
            model.PackageExpensePerPaycheck = totalCost / 26;
            model.NetPayPerPaycheck = model.employee.NetPay / 26;
            return Ok(model);
        }
    }
}
