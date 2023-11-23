using LuxuryHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxuryHotel.Areas.Reception.Controllers
{
    public class BookingController : Controller
    {
        private dbDataContext _db = new dbDataContext();
        // GET: Reception/Booking
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBookList()
        {
            try
            {
                var booking = _db.BOOKINGs
                    .Select(r => new
                    {
                        BookingID = r.BookingID,
                        BookingDate = r.BookingDate,
                        CheckInDate = r.CheckInDate,
                        CheckOutDate = r.CheckOutDate, 
                        RoomTypeID = r.RoomTypeID,
                        PaymentStatus = r.PaymentStatus,
                        CustomerID = r.CustomerID
                    })
                    .ToList();

                return Json(new { code = 200, booking = booking, msg = "Lấy thông tin đặt phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Lấy thông tin đặt phòng thất bại: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult CreateBooking(int BookingID, DateTime BookingDate, DateTime CheckInDate, DateTime CheckOutDate, int RoomTypeID, string PaymentStatus, int CustomerID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tạo đối tượng ROOMTYPE mới và thiết lập giá trị
                    BOOKING booking = new BOOKING
                    {
                        // Thiết lập giá trị từ tham số

                        BookingDate = BookingDate,
                        CheckInDate = CheckInDate,
                        CheckOutDate = CheckOutDate,
                        RoomTypeID = RoomTypeID,
                        PaymentStatus = PaymentStatus,
                        CustomerID = CustomerID
                    };

                    // Thêm loại phòng mới vào database
                    _db.BOOKINGs.InsertOnSubmit(booking);
                    _db.SubmitChanges();

                    return Json(new { code = 200, msg = "Room Type created successfully." });
                }

                return Json(new { code = 400, msg = "Invalid data. Please check your inputs." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Edit(int BookingID, DateTime BookingDate, DateTime CheckInDate, DateTime CheckOutDate, int RoomTypeID, string PaymentStatus, int CustomerID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingbooking = _db.BOOKINGs.SingleOrDefault(r => r.BookingID == BookingID);

                    if (existingbooking != null)
                    {
                        existingbooking.BookingDate = BookingDate;

                        // Chuyển đổi giá trị từ chuỗi sang kiểu int
                        existingbooking.CheckInDate = CheckInDate;
                        existingbooking.CheckOutDate = CheckOutDate;
                        existingbooking.RoomTypeID = RoomTypeID;
                        existingbooking.PaymentStatus = PaymentStatus;
                        existingbooking.CustomerID = CustomerID;

                        _db.SubmitChanges();
                        return Json(new { code = 200, msg = "Booking updated successfully." });
                    }
                }

                return Json(new { code = 400, msg = "Invalid data. Please check your inputs." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = ex.Message });
            }
        }


        [HttpGet]
        public JsonResult GetBookingDetails(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Json(new { code = 400, msg = "Giá trị ID không hợp lệ" }, JsonRequestBehavior.AllowGet);
                }

                var booking = _db.BOOKINGs
                    .Where(r => r.BookingID == id)
                    .Select(r => new
                    {
                        BookingID = r.BookingID,
                        BookingDate = r.BookingDate,
                        CheckInDate = r.CheckInDate,
                        CheckOutDate = r.CheckOutDate,
                        RoomTypeID = r.RoomTypeID,
                        PaymentStatus = r.PaymentStatus,
                        CustomerID = r.CustomerID
                    })
                    .SingleOrDefault();

                if (booking != null)
                {
                    return Json(new { code = 200, booking = booking, msg = "Lấy thông tin đặt phòng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 404, msg = "Không tìm thấy thông tin đặt phòng" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in GetBookingTypeDetails: " + e.Message);
                return Json(new { code = 500, msg = "Lấy thông tin đặt phòng thất bại: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CheckAndDeleteBooking(int BookingID)
        {
            try
            {
                var booking = _db.BOOKINGs.SingleOrDefault(r => r.BookingID == BookingID);

                if (booking != null)
                { 
                    _db.BOOKINGs.DeleteOnSubmit(booking);
                    _db.SubmitChanges();
                    return Json(new { code = 200, msg = "Booking deleted successfully." });
                }
                else
                {
                    return Json(new { code = 404, msg = "Không tìm thấy đặt phòng để xóa." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Đã xảy ra lỗi khi xóa Booking: " + ex.Message });
            }
        }
    }
}