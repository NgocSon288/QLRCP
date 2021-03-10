using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTTH1.Services
{
    public interface IRoomService
    {
        bool InsertRange(List<Room> entities, bool isOveride = true);

        bool Insert(Room entity);

        List<Room> GetAll();

        Room GetByID(Guid id);

        List<Room> GetByListID(List<Guid> ids);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Room entity);
    }

    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _categoryMemberRepository;

        public RoomService()
        {
            this._categoryMemberRepository = new RoomRepository();
        }

        public bool DeleteAll()
        {
            return _categoryMemberRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryMemberRepository.DeleteByID(id);
        }

        public List<Room> GetAll()
        {
            return _categoryMemberRepository.GetAll();
        }

        public Room GetByID(Guid id)
        {
            return _categoryMemberRepository.GetByID(id);
        }

        public List<Room> GetByListID(List<Guid> ids)
        {
            return GetAll().Join(ids, o => o.ID, i => i, (room, id) => room).Where(r => r.SeatCount < r.SeatMax).ToList();
        }

        public bool Insert(Room entity)
        {
            return _categoryMemberRepository.Insert(entity);
        }

        public bool InsertRange(List<Room> entities, bool isOveride = true)
        {
            return _categoryMemberRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Room entity)
        {
            return _categoryMemberRepository.Update(entity);
        }
    }
}