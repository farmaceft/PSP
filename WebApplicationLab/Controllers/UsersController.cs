using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplicationLab.Models;

namespace WebApplicationLab.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private HelpDeskContext db = new HelpDeskContext();

        [HttpGet]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role).ToList();
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            //SelectList departments = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            //ViewBag.Departments = departments;
            SelectList roles = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            SelectList roles = new SelectList(db.Roles, "id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}