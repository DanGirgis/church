using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace church.Controllers
{
    public class PointController : Controller
    {
        Students st = new Students();
        churchEntities _context;
        public PointController()
        {
            _context = new churchEntities();
        }
        // GET: Points
        public ActionResult Index()
        {
            var List = _context.Points.ToList();
            return View(List);
        }
        public ActionResult Create()
        {
            ViewBag.ListStudent = _context.Students;
            ViewBag.listAction = _context.PDependedons;
            return View(new Points { PID = 0 });
        }
        [HttpPost]
        public ActionResult Create(Points _point, int? _sum)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", _point);
            }
            if (_point.PID > 0)
                _context.Entry(_point).State = System.Data.EntityState.Modified;
            else
                _context.Points.AddOrUpdate(_point);
            var sum = _context.Points.Sum(p => p.Score);
            _context.SaveChanges();
            return RedirectToAction("Index", sum);
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var Select = _context.Points.SingleOrDefault(P => P.PID == id);
        //    if (Select == null)
        //        HttpNotFound();
        //    else
        //        _context.Points.Remove(Select);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //public ActionResult SUM(int? _sum)
        //{
        //    var sum = _context.Points.Where(item => item.IDStudent == _sum).Sum(item => item.Score);
        //    return View("Index");
        //}
        public ActionResult Search(string search)
        {
            var students = _context.Students.Where(s => s.Name_student.Contains(search)).ToList();
            List<Points> points = new List<Points>();
            var dbpoints = _context.Points.ToList();
            foreach (var student in students)
            {
                var dbp = dbpoints.Where(p => p.IDStudent == student.ID_student).ToList();
                points.AddRange(dbp);
            }
            return View("Index", points);
        }
        [HttpGet]
        public JsonResult GetStudnetPointsSum(int id)
        {
            var st = _context.Students.FirstOrDefault(s => s.ID_student == id);
            var studentTotalScore = _context.Points.Where(p => p.IDStudent == id).Sum(p => p.Score);
            if (studentTotalScore.HasValue)
            {
                return Json(new { name = st.Name_student, score = studentTotalScore.Value }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { student = st, score = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var point = _context.Points.SingleOrDefault(p => p.PID == id);
            if(point != null)
            {
                _context.Points.Remove(point);
                _context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}