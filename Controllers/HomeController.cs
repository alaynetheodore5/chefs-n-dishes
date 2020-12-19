using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context {get; set;}
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.Chefs = _context.Chefs.Include(c => c.CreatedDishes).ToList();
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(Chef newbie)
        {
            if(ModelState.IsValid)
            {
                if(_context.Chefs.Any( o => o.FirstName == newbie.FirstName))
                {
                    ModelState.AddModelError("FirstName","Choose a chef!!!");
                    ViewBag.Chefs = _context.Chefs.ToList();
                    return View("Index");
                }
                else
                {
                    _context.Chefs.Add(newbie);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("ChefId",newbie.ChefId);
                    return Redirect($"/dashboard/{newbie.ChefId}");
                }
            }
            else
            {
                ViewBag.Chefs = _context.Chefs.ToList();
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(int ChefId)
        {
            HttpContext.Session.SetInt32("ChefId", ChefId);
            return Redirect($"/dashboard/{ChefId}");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("/newdish")]
        public IActionResult NewDish()
        {
            return View("AddDish");
        }

        [HttpPost("/createdish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                int? chefId = HttpContext.Session.GetInt32("ChefId");
                newDish.ChefId = (int)chefId;
                _context.Dishes.Add(newDish);
                _context.SaveChanges();

                return Redirect($"/dashboard/{chefId}");
            }
            else
            {
                return View("AddDish");
            }
        }

        [HttpGet("dashboard/{chefId}")]
        public IActionResult Dashboard (int chefId)
        {   
            if(HttpContext.Session.GetInt32("ChefId") == null)
            {
                return RedirectToAction("Logout");
            }
            Chef ChefInDb = _context.Chefs.Include( c => c.CreatedDishes ).FirstOrDefault( c => c.ChefId == chefId);
            return View(ChefInDb);
        }

    }
}
