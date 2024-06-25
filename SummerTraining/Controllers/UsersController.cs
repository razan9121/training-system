using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SummerTraining.Models;

namespace SummerTraining.Controllers
{
    public class UsersController : Controller
    {
        private SummerTrainingDBEntities db = new SummerTrainingDBEntities();

        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.EN_Name = HashPassword(user.EN_Name);

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                            // You can log the error details if needed
                            System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions that might occur
                    ModelState.AddModelError("", "An error occurred while registering the user. Please try again.");
                    // Log the exception if needed
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return View(user);
        }


        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(int user_id, string password)
        {
            string hashedPassword = HashPassword(password);
            var user = db.Users.FirstOrDefault(u => u.User_Id == user_id && u.EN_Name == hashedPassword);
            if (user != null)
            {
                // Set up user session or authentication cookie here
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        private string HashPassword(string password)
        {
            // Implement your password hashing logic here
            return password; // For simplicity, we return the same password (not secure in a real app)
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.User_Role);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Role_Id = new SelectList(db.User_Role, "Role_Id", "Role_Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,AR_Name,EN_Name,Mobile,Role_Id,Degree,Gender,Section,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role_Id = new SelectList(db.User_Role, "Role_Id", "Role_Name", user.Role_Id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role_Id = new SelectList(db.User_Role, "Role_Id", "Role_Name", user.Role_Id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,AR_Name,EN_Name,Mobile,Role_Id,Degree,Gender,Section,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role_Id = new SelectList(db.User_Role, "Role_Id", "Role_Name", user.Role_Id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
