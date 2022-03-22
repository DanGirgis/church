using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace church.Controllers
{
    public class PlacesController : Controller
    {
        churchEntities _context;
        public PlacesController()
        {
            _context = new churchEntities();
        }
        // GET: Places
        public ActionResult Index()
        {
            var listPlace = _context.Places.ToList();
            return View(listPlace);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Places _places)
        {
            _context.Places.Add(_places);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var _place = _context.Places.SingleOrDefault(p => p.ID_Place == id);
        //    if (_place == null)
        //        HttpNotFound();
        //    else
        //        _context.Places.Remove(_place);

        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool reuslt = false;
            var place = _context.Places.SingleOrDefault(p => p.ID_Place == id);
            if(place != null)
            {
                _context.Places.Remove(place);
                _context.SaveChanges();
                reuslt = true;
            }
            return Json(reuslt, JsonRequestBehavior.AllowGet);
        }

    }
}