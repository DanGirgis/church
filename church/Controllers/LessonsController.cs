using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace church.Controllers
{
    public class LessonsController : Controller
    {
        churchEntities _context;
        public LessonsController()
        {
            _context = new churchEntities();
        }
        // GET: Lessons
        public ActionResult Index()
        {
            var List = _context.Lessons.ToList();
            return View(List);
        }
        public ActionResult Create()
        {
            ViewBag.ListTeacher = _context.Teachers;
            return View(new Lessons { ID_Lesson = 0 });
        }
        [HttpPost]
        public ActionResult Create(Lessons _lesson)
        {
            if (!ModelState.IsValid)
                return View("Create", _lesson);
            if (_lesson.ID_Lesson > 0)
                _context.Entry(_lesson).State = System.Data.EntityState.Modified;
            else
                _context.Lessons.Add(_lesson);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var _lesson = _context.Lessons.SingleOrDefault(l => l.ID_Lesson == id);
        //    if (_lesson == null)
        //        return HttpNotFound();
        //    else
        //        _context.Lessons.Remove(_lesson);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var _lesson = _context.Lessons.SingleOrDefault(l => l.ID_Lesson == id);
            if (_lesson == null)
                HttpNotFound();
            ViewBag.ListTeacher = _context.Teachers;
            return View("Create", _lesson);
        }
        public ActionResult Search(string search)
        {
            var dtch = _context.Lessons.Where(dt => dt.Name_Lesson.Contains(search));
            return View("Index", dtch);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var lesson = _context.Lessons.SingleOrDefault(l => l.ID_Lesson == id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}