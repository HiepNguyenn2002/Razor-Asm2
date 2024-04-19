using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomRepository
    {
        List<RoomInformation> List();
        void Add(RoomInformation room);
        void Update(RoomInformation room);
        void Delete(RoomInformation room);
        List<RoomType> ListRoomType();
        List<RoomInformation> FindAllBy(RoomFilter filter);
        RoomInformation FindOne(Expression<Func<RoomInformation, bool>> predicate);
    }
}
