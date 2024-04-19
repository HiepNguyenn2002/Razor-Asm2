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
    public class BookingReservationDAO
    {
        private static BookingReservationDAO instance = null;
        private static readonly object instanceLock = new object();

        private BookingReservationDAO()
        {
        }

        public static BookingReservationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                    return instance;
                }

            }
        }

        public void Add(BookingReservation booking)
        {
            try
            {
                BookingReservation p = FindOne(item => item.BookingReservationId.Equals(booking.BookingReservationId));
                if (p == null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        if(!hotelContext.Customers.Any(c => c.CustomerId.Equals(booking.CustomerId) && c.CustomerId != 0))
                        {
                            throw new Exception("The customer is not exist");
                        }
                        if(booking.BookingDate < DateTime.Now)
                        {
                            throw new Exception("Cannot book for a previous date");
                        }
                        hotelContext.BookingReservations.Add(booking);
                        hotelContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The booking is already exist");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(BookingReservation booking)
        {
            try
            {
                BookingReservation p = FindOne(item => item.BookingReservationId.Equals(booking.BookingReservationId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        if (!hotelContext.BookingDetails.Any(b => b.BookingReservationId.Equals(booking.BookingReservationId)))
                        {
                            hotelContext.BookingReservations.Remove(booking);
                        }
                        else
                        {
                            booking.BookingStatus = 0;
                            hotelContext.Entry<BookingReservation>(booking).State = EntityState.Modified;
                        }
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

        public BookingReservation FindOne(Expression<Func<BookingReservation, bool>> predicate)
        {
            BookingReservation booking = null;
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    booking = hotelContext.BookingReservations.Include(f => f.Customer).Include(f => f.BookingDetails).SingleOrDefault(predicate);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return booking;
        }

        public List<BookingReservation> FindAll(Expression<Func<BookingReservation, bool>> predicate)
        {
            List<BookingReservation> bookings = new List<BookingReservation>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    bookings = hotelContext.BookingReservations.Where(predicate).Include(f => f.Customer).Include(f => f.BookingDetails).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookings;
        }

        public List<BookingReservation> List()
        {
            List<BookingReservation> bookings = new List<BookingReservation>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    bookings = hotelContext.BookingReservations.Include(f => f.BookingDetails).Include(f => f.Customer).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return bookings;
        }

        public void Update(BookingReservation booking)
        {
            try
            {
                BookingReservation p = FindOne(item => item.BookingReservationId.Equals(booking.BookingReservationId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        if (!hotelContext.Customers.Any(c => c.CustomerId.Equals(booking.CustomerId) && c.CustomerId != 0))
                        {
                            throw new Exception("The customer is not exist");
                        }
                        if (booking.BookingDate < DateTime.Now)
                        {
                            throw new Exception("Cannot book for a previous date");
                        }
                        hotelContext.Entry<BookingReservation>(booking).State = EntityState.Modified;
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

    }
}
