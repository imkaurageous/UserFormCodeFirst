using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using UserFormCodeFirst.Models;

namespace UserFormCodeFirst.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        //POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                model.Password = Crypto.HashPassword(model.Password);
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        //POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username,string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index","Home");
            }
            ViewBag.ErrorMessage = "Invalid Username or Password";
            return View();
        }
        //GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}