using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        List<BookingReservation> List();
        void Add(BookingReservation booking);
        void Update(BookingReservation booking);
        void Delete(BookingReservation booking);
        List<BookingReservation> FindAllBy(BookingReservationFilter filter);
        BookingReservation FindOne(Expression<Func<BookingReservation, bool>> predicate);
    }
}
