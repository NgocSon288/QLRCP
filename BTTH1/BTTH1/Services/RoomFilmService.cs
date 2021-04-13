using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTTH1.Services
{
    public interface IRoomFilmService
    {
        bool InsertRange(List<RoomFilm> entities, bool isOveride = true);

        bool Insert(RoomFilm entity);

        List<RoomFilm> GetAll();

        RoomFilm GetByID(Guid id);

        RoomFilm GetByMultiID(Guid roomID, Guid filmID);

        List<RoomFilm> GetAllByFilmID(Guid filmID);

        List<RoomFilm> GetByFilmID(Guid filmID);

        List<RoomFilm> GetByRoomID(Guid roomID);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(RoomFilm entity);

        bool UpdateRange(List<RoomFilm> entities);
    }

    public class RoomFilmService : IRoomFilmService
    {
        private readonly IRoomFilmRepository _categoryMemberRepository;

        public RoomFilmService()
        {
            this._categoryMemberRepository = new RoomFilmRepository();
        }

        public bool DeleteAll()
        {
            return _categoryMemberRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryMemberRepository.DeleteByID(id);
        }

        public List<RoomFilm> GetAll()
        {
            return _categoryMemberRepository.GetAll();
        }

        public List<RoomFilm> GetByFilmID(Guid filmID)
        {
            return GetAll().Where(rf => rf.DateShow >= DateTime.Now && rf.FilmID == filmID && rf.Status).ToList();
        }

        public List<RoomFilm> GetAllByFilmID(Guid filmID)
        {
            return GetAll().Where(rf => rf.FilmID == filmID && rf.Status).ToList();
        }

        public RoomFilm GetByID(Guid id)
        {
            return _categoryMemberRepository.GetByID(id);
        }

        public RoomFilm GetByMultiID(Guid roomID, Guid filmID)
        {
            return GetAll().FirstOrDefault(rf => rf.RoomID == roomID && rf.FilmID == filmID);
        }

        public bool Insert(RoomFilm entity)
        {
            return _categoryMemberRepository.Insert(entity);
        }

        public bool InsertRange(List<RoomFilm> entities, bool isOveride = true)
        {
            return _categoryMemberRepository.InsertRange(entities, isOveride);
        }

        public bool Update(RoomFilm entity)
        {
            return _categoryMemberRepository.Update(entity);
        }

        public bool UpdateRange(List<RoomFilm> entities)
        {
            try
            {
                entities.ForEach(rf => Update(rf));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<RoomFilm> GetByRoomID(Guid roomID)
        {
            return GetAll().Where(rf => rf.DateShow >= DateTime.Now && rf.RoomID == roomID && rf.Status).ToList();
        }
    }
}