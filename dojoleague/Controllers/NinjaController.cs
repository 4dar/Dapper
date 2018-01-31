using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojoleague.Models;
using dojoleague.Factory;

namespace dojoleague.Controllers
{
    public class NinjaController : Controller
    {
        private readonly NinjaFactory ninjaFactory;
        public NinjaController(NinjaFactory _ninjafactory)
        {
            ninjaFactory = _ninjafactory;
        }


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.allNinjas = ninjaFactory.FindAll();
            return View("ninjas");
        }


        [HttpPost]
        [Route("registerNinja")]
        public IActionResult registerNinja(Ninja ninja)
        {
            if(ModelState.IsValid)
            {
                ninjaFactory.Add(ninja);
                return RedirectToAction("ninjas");
            }
            return View("ninjainfo");
        } 
        [HttpGet]
        [Route("ninjas/{id}")]  
        public IActionResult viewTrail(int id)
        {
            ViewBag.ninja = ninjaFactory.FindByID(id);
            return View("ninjainfo");
        }
    }
}