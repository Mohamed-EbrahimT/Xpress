using FinalProj.Data;
using FinalProj.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProj.ViewModels;


namespace FinalProj.Controllers
{
    public class Login_SignupController : Controller
    {
        private ECContext _contxet;

        public Login_SignupController(ECContext contxet)
        {
            _contxet = contxet;
        }

        private readonly ILogger<Login_SignupController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}


        public IActionResult CreateAccount()
        {
            ViewBag.UserRoleId = new SelectList(new List<string> { "Customer (1)", "Seller (2)" });

            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(RegisterViewModel reg)
        {
            // ViewBag.UserRoleId = new SelectList(new List<string> { "Customer (1)", "Seller (2)" });

            // Extract the role ID from the selected string
            int roleId = 0;
            if (reg.UserRoleId == "Customer (1)")
            {
                roleId = 1;
            }
            else if (reg.UserRoleId == "Seller (2)")
            {
                roleId = 2;
            }
            if (ModelState.IsValid)
            {
                User newuser = new User()
                {
                    Email = reg.Email,
                    FirstName = reg.FirstName,
                    LastName = reg.LastName,
                    Phone = reg.Phone,
                    Password = reg.Password,
                    DateOfBirth = reg.DateOfBirth,
                    Age = reg.Age,
                    UserRoleId = roleId
                };

                _contxet.Users.Add(newuser);
                _contxet.SaveChanges();
            }
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel log)
        {
            if (ModelState.IsValid)
            {
                var check = _contxet.Users.FirstOrDefault(m => m.Email == log.Email && m.Password == log.Password);
                if (check != null)
                {
                    HttpContext.Session.SetInt32("UserId", check.UserId);
                    HttpContext.Session.SetString("UserEmail", check.Email);

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View(log);
                }
            }
            return View(log);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
