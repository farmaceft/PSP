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
	}
}