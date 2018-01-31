using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using lostinthewoods.Models;
using lostinthewoods.Factory;

namespace lostinthewoods.Controllers
{
    public class TrailController : Controller
    {
        private readonly TrailFactory trailFactory;
        public TrailController(TrailFactory _trailfactory)
        {
            trailFactory = _trailfactory;
        }


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.allTrails = trailFactory.FindAll();
            return View();
        }


        [HttpGet]
        [Route("addTrail")]
        public IActionResult AddTrail()
        {
            
            return View("newtrail");
        }


        [HttpPost]
        [Route("createTrail")]
        public IActionResult createTrail(Trail trail)
        {
            if(ModelState.IsValid)
            {
                trailFactory.Add(trail);
                System.Console.WriteLine("Trail successfully added to DB");
                return RedirectToAction("Index");
            }
            System.Console.WriteLine("*/*/*/* Failed to create new trail */*/*/*");
            return View("newtrail");
        }

        [HttpGet]
        [Route("trails/{id}")]  
        public IActionResult viewTrail(int id)
        {
            ViewBag.trail = trailFactory.FindByID(id);
            return View("trailinfo");
        }
    }
}