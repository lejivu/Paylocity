using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Foundation.DTO;

namespace Paylocity.Controllers
{
    public class PaycheckController : Controller
    {
        private ServiceHelper _ServiceHelper;
        public PaycheckController(ServiceHelper helper)
        {
            _ServiceHelper = helper;
        }
        public async Task<IActionResult> Index()
        {
            List<Paycheck> model = await _ServiceHelper.GetAsync<List<Paycheck>>("Paycheck");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id = 0)
        {
            Paycheck model;

            if (id > 0)
                model = await _ServiceHelper.GetAsync<Paycheck>($"Paycheck/{id}");
            else
                model = new Paycheck();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Paycheck model)
        {
            if (model.PaycheckID > 0)
                model = await _ServiceHelper.PutAsync<Paycheck>($"Paycheck/{model.PaycheckID}", model);
            else
                model = await _ServiceHelper.PostAsync<Paycheck>("Paycheck", model);

            return View(model);
        }
    }
}
