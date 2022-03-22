using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity.Migrations;

namespace church.Controllers
{
    public class StudentsController : Controller
    {
        churchEntities _context;
        public StudentsController()
        {
            _context = new churchEntities();
        }
        // GET: Students
        public ActionResult Index()
        {
            var list = _context.Students.ToList();
            return View(list);
        }
        public ActionResult Create()
        {

            ViewBag.Placeslist = _context.Places;
            return View(new Students { ID_student = 0 });
        }
        [HttpPost]
        public ActionResult Create(Students _studnet, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
                return View("Create", _studnet);
            if (_studnet.ID_student > 0)
            {
                var student = _context.Students.FirstOrDefault(st => st.ID_student == _studnet.ID_student);
                _studnet.img = student.img;
            }
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/Uploades"), upload.FileName);
                upload.SaveAs(path);
                _studnet.img = upload.FileName;
            }
            _context.Students.AddOrUpdate(_studnet);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var _student = _context.Students.SingleOrDefault(s => s.ID_student == id);
        //    if (_student == null)
        //        return HttpNotFound();
        //    _context.Students.Remove(_student);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Edit(int? id)
        {
            Students st = new Students();
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var _Student = _context.Students.SingleOrDefault(s => s.ID_student == id);
            if (_Student == null)
                HttpNotFound();
            ViewBag.Placeslist = _context.Places;
            // string path = Path.Combine(Server.MapPath("~/Uploades/"), img);
            //  upload.SaveAs(path);
            //   st.img = upload.FileName;
            // _Student.Date_of_birth = _Student.Date_of_birth.ToString("yyyy-M")

            return View("Create", _Student);
        }
        public ActionResult Search(string search)
        {
            var ch = _context.Students.Where(st => st.Name_student.Contains(search));
            return View("Index", ch);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var student = _context.Students.FirstOrDefault(s => s.ID_student == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                result = true;
            }
            return Json (result, JsonRequestBehavior.AllowGet); 
        }
    }
}