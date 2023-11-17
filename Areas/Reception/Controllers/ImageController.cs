using LuxuryHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxuryHotel.Areas.Reception.Controllers
{
    public class ImageController : Controller
    {
        // GET: Reception/Image
        private dbDataContext db = new dbDataContext();

        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult GetRoomImages()
        {
            try
            {
                var rooms = db.ROOMs
                    .Select(r => new
                    {
                       RoomID= r.RoomID,
                       RoomName= r.RoomName,

                    })
                    .ToList();
                return Json(new { code = 200, rooms = rooms, msg = "Lấy danh sách phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult GetUrlImagesByRoom(int roomID)
        {
            try
            {
                var images = (from ru in db.Images
                                 where ru.RoomID == roomID
                                 orderby (ru.OderID)
                                 select new
                                 {
                                     ImagePath = ru.ImagePath,
                                     
                                 }).ToList();

                return Json(new { code = 200, images = images, msg = "Lấy thông tin ImagePath thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Lấy thông tin ImagePath thất bại: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}