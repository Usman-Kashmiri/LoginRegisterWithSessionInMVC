using LoginRegisterWithSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginRegisterWithSession.Controllers
{
    public class HomeController : Controller
    {
        UsersDatabaseEntities usersobj = new UsersDatabaseEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(UsersTbl user)
        {
            var u = usersobj.UsersTbls.Where(model => model.UserEmail == user.UserEmail && model.UserPassword == user.UserPassword).FirstOrDefault();
            if (user != null)
            {
                Session["Id"] = u.UserId.ToString();
                Session["userName"] = u.UserName.ToString();
                TempData["msg"] = "<script>alert('Login succefully...!')</script>";
                ModelState.Clear();
                return RedirectToAction("Dashboard");
            } else
            {
                ViewBag.errormsg = "<script>alert('Login failed...!')</script>";
                return View();
            }
        }

        // GET: Register
        public ActionResult Registration()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Registration(UsersTbl user)
        {
            if (ModelState.IsValid)
            {
                usersobj.UsersTbls.Add(user);
                int a = usersobj.SaveChanges();
                if (a > 0)
                {
                    ViewBag.msg = "<script>alert('User registered succefully...!')</script>";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                } else
                {
                    ViewBag.msg = "<script>alert('User registration failed...!')</script>";
                }
            }
            return View();
        }

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}