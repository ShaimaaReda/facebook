using face.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

//using System.Collections.Generic.IEnumerable;


namespace face.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewUser(Users user, HttpPostedFileBase ImageFile)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            user.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            ImageFile.SaveAs(fileName);
            db.Users.Add(user);

            //db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public ActionResult singIn()
        {

            return View();
        }


        [HttpPost]

        public ActionResult singIn(Users user)
        {
            var obj = db.Users.Where(z => z.Email.Equals(user.Email) &&  z.password.Equals(user.password)).FirstOrDefault();
           
            if (obj == null)
            {
                ViewBag.loginErrormassage = "Wrong username or password";
                return View(user);
            }
            else
            {
                Session["id"] = obj.Id;
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {

           // Users user1 = new Users();

            var obj = db.Users.Where(z => z.Id==id).FirstOrDefault();
            return View(obj);

        }
        [HttpPost]
        public ActionResult Edit(Users user, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(z => z.Id == user.Id).FirstOrDefault();

                obj.City = user.City;
                obj.FirstName = user.FirstName;
                obj.LastName = user.LastName;
                obj.Country = user.Country;
                obj.password = user.password;
                obj.ConfirmPassword = user.ConfirmPassword;
                obj.Email = user.Email;
                obj.mobile = user.mobile;
                obj.ImagePath = user.ImagePath;

                ////////////image//////////////
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                obj.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.SaveChanges();
                return RedirectToAction("Index", "Home"); // or whatever
            }
            return View(user);

        }

    }
}