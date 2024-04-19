using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        private BookingDetailDAO()
        {
        }

        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }

            }
        }

        public void Add(BookingDetail bookingDetail)
        {
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    
                    if (!hotelContext.BookingReservations.Any(b => b.BookingReservationId.Equals(bookingDetail.BookingReservationId) && !b.BookingStatus.Equals(0)))
                    {
                        throw new Exception(bookingDetail.BookingReservationId.ToString());
                    }
                    RoomInformation room = hotelContext.RoomInformations.FirstOrDefault(b => b.RoomId.Equals(bookingDetail.RoomId));
                    if (room == null)
                    {
                        throw new Exception("The room is not exist");
                    }
                    else
                    {
                        if (room.RoomStatus.Equals(0))
                        {
                            throw new Exception("The room is deleted");
                        }
                        if (hotelContext.BookingDetails.Any(b => b.RoomId.Equals(bookingDetail.RoomId) && (bookingDetail.StartDate >= b.StartDate && bookingDetail.StartDate <= b.EndDate) ||
                                                                                (bookingDetail.EndDate >= b.StartDate && bookingDetail.EndDate <= b.EndDate) ||
                                                                                (bookingDetail.StartDate <= b.StartDate && bookingDetail.EndDate >= b.EndDate)))
                        {
                            throw new Exception("The room is booked by anyone");
                        }
                    }
                    hotelContext.BookingDetails.Add(bookingDetail);
                    hotelContext.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(BookingDetail booking)
        {
            try
            {
                BookingDetail p = FindOne(item => item.BookingReservationId.Equals(booking.BookingReservationId) && item.RoomId.Equals(booking.RoomId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        hotelContext.BookingDetails.Remove(booking);
                        hotelContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The room does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BookingDetail FindOne(Expression<Func<BookingDetail, bool>> predicate)
        {
            BookingDetail bookingDetail = null;
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    bookingDetail = hotelContext.BookingDetails.Include(f => f.Room).Include(f => f.BookingReservation).SingleOrDefault(predicate);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookingDetail;
        }

        public List<BookingDetail> FindAll(Expression<Func<BookingDetail, bool>> predicate)
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    bookingDetails = hotelContext.BookingDetails.Where(predicate).Include(f => f.Room).Include(f => f.BookingReservation).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookingDetails;
        }

        public List<BookingDetail> List()
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    bookingDetails = hotelContext.BookingDetails.Include(f => f.Room).Include(f => f.BookingReservation).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return bookingDetails;
        }

        public void Update(BookingDetail bookingDetail)
        {
            try
            {
                BookingDetail p = FindOne(item => item.BookingReservationId.Equals(bookingDetail.BookingReservationId) && item.RoomId.Equals(bookingDetail.RoomId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        hotelContext.Entry<BookingDetail>(bookingDetail).State = EntityState.Modified;
                        hotelContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The booking does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RoomInformation> GetAvailableRoom()
        {
            List<RoomInformation> listRooms = new List<RoomInformation>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    listRooms = hotelContext.RoomInformations.Where(f => f.RoomStatus != 0).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listRooms;
        }
    }
}
