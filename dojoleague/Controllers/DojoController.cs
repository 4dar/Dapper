using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojoleague.Models;
using dojoleague.Factory;

namespace dojoleague.Controllers
{
    public class DojoController : Controller
    {
        private readonly DojoFactory dojoFactory;
        public DojoController(DojoFactory _dojofactory)
        {
            dojoFactory = _dojofactory;
        }


        [HttpGet]
        [Route("dojos")]
        public IActionResult Index()
        {
            ViewBag.allDojos = dojoFactory.FindAll();
            return View("dojos");
        }


        [HttpPost]
        [Route("createDojo")]
        public IActionResult createDojo(Dojo dojo)
        {
            if(ModelState.IsValid)
            {
                dojoFactory.Add(dojo);
                return RedirectToAction("Index");
            }
            return View("newtrail");
        } 

        [HttpGet]
        [Route("dojos/{id}")]  
        public IActionResult viewDojo(int id)
        {
            ViewBag.dojo = dojoFactory.FindByID(id);
            return View("dojoinfo");
        }
    }
}