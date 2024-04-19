using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void Add(BookingDetail bookingDetail)
        {
            BookingDetailDAO.Instance.Add(bookingDetail);
        }

        public void Delete(BookingDetail bookingDetail)
        {
            BookingDetailDAO.Instance.Delete(bookingDetail);
        }

        public List<BookingDetail> FindAllBy(BookingDetailFilter filter)
        {
            if (filter != null)
            {
                return BookingDetailDAO.Instance.FindAll(booking => (filter.BookingReservationId == null || booking.BookingReservationId.Equals(filter.BookingReservationId)) &&
                                                              (filter.RoomId == null || booking.RoomId.Equals(filter.RoomId)) &&
                                                              (filter.StartDate == null || booking.StartDate == filter.StartDate) &&
                                                              (filter.EndDate == null || booking.EndDate == filter.EndDate) &&
                                                              (filter.ActualPrice == null || booking.ActualPrice.Equals(filter.ActualPrice)));
            }
            return List();
        }

        public List<BookingDetail> List()
        {
            return BookingDetailDAO.Instance.List();
        }

        public List<RoomInformation> GetAvailableRoom()
        {
            return BookingDetailDAO.Instance.GetAvailableRoom();
        }

        public void Update(BookingDetail bookingDetail)
        {
            BookingDetailDAO.Instance.Update(bookingDetail);
        }

        public BookingDetail FindOne(Expression<Func<BookingDetail, bool>> predicate)
        {
            return BookingDetailDAO.Instance.FindOne(predicate);
        }


    }

    public class BookingDetailFilter
    {
        public int? BookingReservationId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ActualPrice { get; set; }
    }
}
