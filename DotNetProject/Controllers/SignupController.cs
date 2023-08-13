using DotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetProject.Controllers
{
    public class SignupController : Controller
    {
        private readonly DotNetProjectContext _context;
        public SignupController(DotNetProjectContext context)
        {
            _context = context;
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
       
        public IActionResult Signup(UserCred userCred)
        {
            var result=_context.userCreds.FirstOrDefault(x=>x.Email == userCred.Email);
            if(result==null)
            {
                if(userCred.ConfirmPassword==userCred.Password)
            {
            _context.userCreds.Add(userCred);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");

            }
            else
            {

                ModelState.AddModelError("ConfirmPassword", "Password should be same");
            }

            }
            else
            {
                ModelState.AddModelError("email", "User Already Exist");
            }
           
            return View(userCred);
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(UserCred userCred)
        {
            var result = _context.userCreds.FirstOrDefault(x=>x.Email==userCred.Email && x.Password==userCred.Password);
            var admin = _context.adminCreds.FirstOrDefault(x => x.Email == userCred.Email && x.Password == userCred.Password);
            if(result!=null)
            {
               // ViewBag.isSucced = true;
                TempData["isSucced"] = true;
                return RedirectToAction("Index", "Home");


            }
            else if ( admin != null)
            {
                ViewBag.IsAdmin = true;
                return RedirectToAction("Index","Movies");
                

            }
            else
            {
                ModelState.AddModelError("Password", "Password InCorrect");

            }

            return View(userCred);
        }
        public IActionResult ForgotPW()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPW(UserCred userCred)
        {
            var result = _context.userCreds.FirstOrDefault(x => x.Email == userCred.Email);
            if(result!=null)
            {
                if(userCred.Password==userCred.ConfirmPassword)
                {
                    if(result.Password==userCred.Password)
                    {
                        ModelState.AddModelError("Password", "You cannot change to existing Password");
                    }
                    else
                    {
                     result.Password = userCred.Password;
                    _context.userCreds.Update(result);
                    _context.SaveChanges();
                    return RedirectToAction("Login","Signup");
                    }
               
             
                }
                else
                {
                ModelState.AddModelError("ConfirmPassword", "Password Should Be Same");

                }
            }
            else
            {
                ModelState.AddModelError("Email", "this User Doesnot Exist");

            }
            return View();
        }
    }
}
