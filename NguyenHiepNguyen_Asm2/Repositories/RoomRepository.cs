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
    public class RoomRepository : IRoomRepository
    {
        public void Add(RoomInformation room)
        {
            RoomDAO.Instance.Add(room);
        }

        public void Delete(RoomInformation room)
        {
            RoomDAO.Instance.Delete(room);
        }

        public List<RoomInformation> FindAllBy(RoomFilter filter)
        {
            if (filter != null)
            {
                return RoomDAO.Instance.FindAll(room => (filter.RoomId == null || room.RoomId.Equals(filter.RoomId)) &&
                                                              (filter.RoomNumber == null || room.RoomNumber.ToLower().Contains(filter.RoomNumber.ToLower())) &&
                                                              (filter.RoomDetailDescription == null || room.RoomDetailDescription.ToLower().Contains(filter.RoomDetailDescription.ToLower())) &&
                                                              (filter.RoomMaxCapacity == null || room.RoomMaxCapacity.Equals(filter.RoomMaxCapacity)) &&
                                                              (filter.RoomTypeId == null || room.RoomTypeId.Equals(filter.RoomTypeId)) &&
                                                              (filter.RoomStatus == null || room.RoomStatus.Equals(filter.RoomStatus)) &&
                                                              (filter.RoomPricePerDay == null || room.RoomPricePerDay.Equals(filter.RoomPricePerDay)));
            }
            return List();
        }

        public List<RoomInformation> List()
        {
            return RoomDAO.Instance.List();
        }

        public List<RoomType> ListRoomType()
        {
            return RoomDAO.Instance.ListRoomType();
        }

        public void Update(RoomInformation room)
        {
            RoomDAO.Instance.Update(room);
        }

        public RoomInformation FindOne(Expression<Func<RoomInformation, bool>> predicate)
        {
            return RoomDAO.Instance.FindOne(predicate);
        }
    }

    public class RoomFilter
    {
        public int? RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int? RoomTypeId { get; set; }
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }
    }
}
