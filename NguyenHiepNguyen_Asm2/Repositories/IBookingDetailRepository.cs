using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> List();
        void Add(BookingDetail bookingDetail);
        void Update(BookingDetail bookingDetail);
        void Delete(BookingDetail bookingDetail);
        List<BookingDetail> FindAllBy(BookingDetailFilter filter);
        BookingDetail FindOne(Expression<Func<BookingDetail, bool>> predicate);
        List<RoomInformation> GetAvailableRoom();
    }
}
