using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplicationLab.Models;

namespace WebApplicationLab.Controllers
{
    public class PhonesController : Controller
    {
        private HelpDeskContext db = new HelpDeskContext();

        public ActionResult Index()
        {
            var phone = db.Phone.Include(p => p.Battery).Include(p => p.Camera).Include(p => p.Color).Include(p => p.Company).Include(p => p.OZU).Include(p => p.ScreenSize).Include(p => p.SystemOperation);
            return View(phone.ToList());
        }

        [HttpGet]
        public ActionResult Filtr()
        {            
            ViewBag.BatteryId = new SelectList(db.Battery, "Id", "Name");
            ViewBag.CameraId = new SelectList(db.Camera, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "Name");
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.OZUId = new SelectList(db.OZU, "Id", "Name");
            ViewBag.ScreenSizeId = new SelectList(db.ScreenSize, "Id", "Name");
            ViewBag.SystemOperationId = new SelectList(db.SystemOperation, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Filtr(Phone phone)
        {
            var phones = db.Phone.Include(p => p.Battery).Include(p => p.Camera).Include(p => p.Color).Include(p => p.Company).Include(p => p.OZU).Include(p => p.ScreenSize).Include(p => p.SystemOperation);

            if (phone.CompanyId != 0)
            {
                phones = phones.Where(p => p.CompanyId == phone.CompanyId);
            }

            if (phone.BatteryId != 0)
            {
                phones = phones.Where(p => p.BatteryId == phone.BatteryId);
            }

            if (phone.CameraId != 0)
            {
                phones = phones.Where(p => p.CameraId == phone.CameraId);
            }

            if (phone.ColorId != 0)
            {
                phones = phones.Where(p => p.ColorId == phone.ColorId);
            }

            if (phone.OZUId != 0)
            {
                phones = phones.Where(p => p.OZUId == phone.OZUId);
            }

            if (phone.ScreenSizeId != 0)
            {
                phones = phones.Where(p => p.ScreenSizeId == phone.ScreenSizeId);
            }

            if (phone.SystemOperationId != 0)
            {
                phones = phones.Where(p => p.SystemOperationId == phone.SystemOperationId);
            }

            return View("Index", phones.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Phone phone = db.Phone.Include(p => p.Battery).Include(p => p.Camera).Include(p => p.Color).Include(p => p.Company).Include(p => p.OZU).Include(p => p.ScreenSize).Include(p => p.SystemOperation).Where(p => p.Id == id).First();

            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        public ActionResult Create()
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.BatteryId = new SelectList(db.Battery, "Id", "Name");
                ViewBag.CameraId = new SelectList(db.Camera, "Id", "Name");
                ViewBag.ColorId = new SelectList(db.Color, "Id", "Name");
                ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
                ViewBag.OZUId = new SelectList(db.OZU, "Id", "Name");
                ViewBag.ScreenSizeId = new SelectList(db.ScreenSize, "Id", "Name");
                ViewBag.SystemOperationId = new SelectList(db.SystemOperation, "Id", "Name");

                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CompanyId,Price,Image,Description,SystemOperationId,ScreenSizeId,Date,OZUId,ColorId,CameraId,BatteryId,Memory")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phone.Add(phone);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BatteryId = new SelectList(db.Battery, "Id", "Name");
            ViewBag.CameraId = new SelectList(db.Camera, "Id", "Name");
            ViewBag.ColorId = new SelectList(db.Color, "Id", "Name");
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name");
            ViewBag.OZUId = new SelectList(db.OZU, "Id", "Name");
            ViewBag.ScreenSizeId = new SelectList(db.ScreenSize, "Id", "Name");
            ViewBag.SystemOperationId = new SelectList(db.SystemOperation, "Id", "Name");

            return View("Create");
        }

        [HttpPost]
        public JsonResult Upload()
        {
            string fileName = "";
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    fileName = Path.GetRandomFileName().Replace('.','_') + Path.GetExtension(upload.FileName);

                    upload.SaveAs(Server.MapPath("~/Images/" + fileName));
                }
            }
            return Json("/Images/" + fileName);
        }

        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Phone phone = db.Phone.Find(id);

                if (phone == null)
                {
                    return HttpNotFound();
                }

                ViewBag.BatteryId = new SelectList(db.Battery, "Id", "Name", phone.BatteryId);
                ViewBag.CameraId = new SelectList(db.Camera, "Id", "Name", phone.CameraId);
                ViewBag.ColorId = new SelectList(db.Color, "Id", "Name", phone.ColorId);
                ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", phone.CompanyId);
                ViewBag.OZUId = new SelectList(db.OZU, "Id", "Name", phone.OZUId);
                ViewBag.ScreenSizeId = new SelectList(db.ScreenSize, "Id", "Name", phone.ScreenSizeId);
                ViewBag.SystemOperationId = new SelectList(db.SystemOperation, "Id", "Name", phone.SystemOperationId);

                return View(phone);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CompanyId,Price,Image,Description,SystemOperationId,ScreenSizeId,Date,OZUId,ColorId,CameraId,BatteryId,Memory")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BatteryId = new SelectList(db.Battery, "Id", "Name", phone.BatteryId);
            ViewBag.CameraId = new SelectList(db.Camera, "Id", "Name", phone.CameraId);
            ViewBag.ColorId = new SelectList(db.Color, "Id", "Name", phone.ColorId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Name", phone.CompanyId);
            ViewBag.OZUId = new SelectList(db.OZU, "Id", "Name", phone.OZUId);
            ViewBag.ScreenSizeId = new SelectList(db.ScreenSize, "Id", "Name", phone.ScreenSizeId);
            ViewBag.SystemOperationId = new SelectList(db.SystemOperation, "Id", "Name", phone.SystemOperationId);

            return View(phone);
        }

        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Phone phone = db.Phone.Include(p => p.Battery).Include(p => p.Camera).Include(p => p.Color).Include(p => p.Company).Include(p => p.OZU).Include(p => p.ScreenSize).Include(p => p.SystemOperation).Where(p => p.Id == id).First();

                if (phone == null)
                {
                    return HttpNotFound();
                }

                return View(phone);
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phone.Find(id);
            db.Phone.Remove(phone);
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