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
    public class RoomDAO
    {
        private static RoomDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoomDAO() { }
        public static RoomDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomDAO();
                    }
                    return instance;
                }

            }
        }

        public void Add(RoomInformation room)
        {
            try
            {
                RoomInformation p = FindOne(item => item.RoomId.Equals(room.RoomId));
                if (p == null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        hotelContext.RoomInformations.Add(room);
                        hotelContext.SaveChanges();
                    }

                }
                else
                {
                    throw new Exception("The room is already exist");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(RoomInformation room)
        {
            try
            {
                RoomInformation p = FindOne(item => item.RoomId.Equals(room.RoomId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        if(!hotelContext.BookingDetails.Any(b => b.RoomId.Equals(room.RoomId)))
                        {
                            hotelContext.RoomInformations.Remove(room);
                        }
                        else
                        {
                            room.RoomStatus = 0;
                            hotelContext.Entry<RoomInformation>(room).State = EntityState.Modified;
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

        public RoomInformation FindOne(Expression<Func<RoomInformation, bool>> predicate)
        {
            RoomInformation room = null;
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    room = hotelContext.RoomInformations.Include(f => f.RoomType).SingleOrDefault(predicate);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return room;
        }

        public List<RoomInformation> FindAll(Expression<Func<RoomInformation, bool>> predicate)
        {
            List<RoomInformation> rooms = new List<RoomInformation>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    rooms = hotelContext.RoomInformations.Where(predicate).Include(f => f.RoomType).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return rooms;
        }

        public List<RoomInformation> List()
        {
            List<RoomInformation> rooms = new List<RoomInformation>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    rooms = hotelContext.RoomInformations.Include(f => f.RoomType).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return rooms;
        }

        public List<RoomType> ListRoomType()
        {
            List<RoomType> roomTypes = new List<RoomType>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    roomTypes = hotelContext.RoomTypes.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return roomTypes;
        }

        public void Update(RoomInformation room)
        {
            try
            {
                RoomInformation p = FindOne(item => item.RoomId.Equals(room.RoomId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        hotelContext.Entry<RoomInformation>(room).State = EntityState.Modified;
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
    }
}
