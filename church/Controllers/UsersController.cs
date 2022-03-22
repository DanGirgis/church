using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using church.ViewLogin;
namespace church.Controllers
{
    public class UsersController : Controller
    {
        Users u = new Users();
        churchEntities _context;
        public UsersController()
        {
            _context = new churchEntities();
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Users _users)
        {
            if (!ModelState.IsValid)
                return View("Create", _users);
            if (_context.Users.Where(u => u.Email == _users.Email).Any())
            {
                ModelState.AddModelError("Email", "This Email already Exists");
                return View("Create", _users);
            }
            _context.Users.Add(_users);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginView _user)
        {

            if (!ModelState.IsValid)
                return View("Login", _user);


            var Loginuser = _context.Users.Where
                (u => u.Email == _user.Email && u.Password == _user.Password && u.Active == true).
                FirstOrDefault();
            //if(u.Email != _user.Email && u.Password != _user.Password)
            //{

            //    ModelState.AddModelError("Email", "Email or password incorrect , please try with correct Email and password");
            //    return View("Login", _user);
            //}
            if (Loginuser == null)
            {

                ModelState.AddModelError("Email", "Email or password , please try with correct Email and password");
                return View("Login", _user);
            }
            else
            {
                Session["Email"] = Loginuser.Email;
                return RedirectToAction("Index", "Places");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}