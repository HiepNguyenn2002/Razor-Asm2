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
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void Add(BookingReservation booking)
        {
            BookingReservationDAO.Instance.Add(booking);
        }

        public void Delete(BookingReservation booking)
        {
            BookingReservationDAO.Instance.Delete(booking);
        }

        public List<BookingReservation> FindAllBy(BookingReservationFilter filter)
        {
            if (filter != null)
            {
                return BookingReservationDAO.Instance.FindAll(booking => (filter.BookingReservationID == null || booking.BookingReservationId.Equals(filter.BookingReservationID)) &&
                                                              (filter.BookingDate == null || booking.BookingDate == filter.BookingDate) &&
                                                              (filter.TotalPrice == null || booking.TotalPrice.Equals(filter.TotalPrice)) &&
                                                              (filter.CustomerID == null || booking.CustomerId.Equals(filter.CustomerID)) &&
                                                              (filter.BookingStatus == null || booking.BookingStatus.Equals(filter.BookingStatus)));
            }
            return List();
        }

        public List<BookingReservation> List()
        {
            return BookingReservationDAO.Instance.List();
        }

        public void Update(BookingReservation booking)
        {
            BookingReservationDAO.Instance.Update(booking);
        }

        public BookingReservation FindOne(Expression<Func<BookingReservation, bool>> predicate)
        {
            return BookingReservationDAO.Instance.FindOne(predicate);
        }
    }

    public class BookingReservationFilter
    {
        public int? BookingReservationID { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? CustomerID { get; set; }
        public byte? BookingStatus { get; set; }
    }
}

