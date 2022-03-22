using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace church.Controllers
{
    public class PDependedonController : Controller
    {
        churchEntities _context;
        public PDependedonController()
        {
            _context = new churchEntities();
        }
        // GET: PDependedon
        public ActionResult Index()
        {
            var list = _context.PDependedons.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PDependedon _pdepen)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", _pdepen);
            }
            _context.PDependedons.Add(_pdepen);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var _pdepen = _context.PDependedons.SingleOrDefault(pd => pd.PDID == id);
        //    if (_pdepen == null)
        //        HttpNotFound();
        //    _context.PDependedons.Remove(_pdepen);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool reuslt = false;
            var action = _context.PDependedons.SingleOrDefault(pd => pd.PDID == id);
            if (action != null)
            {
                _context.PDependedons.Remove(action);
                _context.SaveChanges();
                reuslt = true;
            }
            return Json(reuslt, JsonRequestBehavior.AllowGet);
        }
    }
}