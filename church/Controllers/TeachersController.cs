using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace church.Controllers
{
    public class TeachersController : Controller
    {
        churchEntities _context;
        public TeachersController()
        {
            _context = new churchEntities();
        }
        // GET: Teachers
        public ActionResult Index()
        {
            var list = _context.Teachers.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            //var Lisiplace = _context.Places.ToList();
            ViewBag.Placeslist = _context.Places;
            return View(new Teachers { ID_teacher = 0 });
        }
        [HttpPost]
        public ActionResult Create(Teachers _teachers)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", _teachers);
            }
            if (_teachers.ID_teacher > 0)
                _context.Entry(_teachers).State = System.Data.EntityState.Modified;
            else
                _context.Teachers.Add(_teachers);



            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var _teachers = _context.Teachers.SingleOrDefault(t => t.ID_teacher == id);
            if (_teachers == null)
                HttpNotFound();
            ViewBag.Placeslist = _context.Places;
            return View("Create", _teachers);
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var _Teachers = _context.Teachers.SingleOrDefault(p => p.ID_teacher == id);
        //    if (_Teachers == null)
        //        HttpNotFound();
        //    _context.Teachers.Remove(_Teachers);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        public ActionResult Search(string search)
        {
            var ch = _context.Teachers.Where(tr => tr.Name_teacher.Contains(search));
            return View("Index", ch);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var tea = _context.Teachers.SingleOrDefault(t => t.ID_teacher == id);
            if (tea != null)
            {
                _context.Teachers.Remove(tea);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

