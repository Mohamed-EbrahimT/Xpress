using System.Diagnostics;
using FinalProj.Data;
using FinalProj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProj.Controllers
{
    public class HomeController : Controller
    {

        private ECContext _context;

        public HomeController(ECContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var prod = _context.Products.ToList();
            return View(prod);
        }
        
        public IActionResult _Category()
        {
            var cat=_context.Categories.ToList();
            return PartialView(cat);
        }
        public IActionResult Error()
        {
            return View(); // For exception handling
        }




    }
}
