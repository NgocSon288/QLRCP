using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTTH1.Services
{
    public interface IFilmService
    {
        bool InsertRange(List<Film> entities, bool isOveride = true);

        bool Insert(Film entity);

        List<Film> GetAll();

        Film GetByID(Guid id);

        List<Film> GetByTopCount(int count);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Film entity);
    }

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService()
        {
            this._filmRepository = new FilmRepository();
        }

        public bool DeleteAll()
        {
            return _filmRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _filmRepository.DeleteByID(id);
        }

        public List<Film> GetAll()
        {
            return _filmRepository.GetAll().Where(f=>f.Status).ToList();
        }

        public Film GetByID(Guid id)
        {
            return _filmRepository.GetByID(id);
        }

        public List<Film> GetByTopCount(int count)
        {
            try
            {
                //return _filmRepository.GetAll().Where(f=>f.DateShow > DateTime.Now).Take(count).ToList();
                return _filmRepository.GetAll().Take(count).ToList();
            }
            catch (Exception)
            {
                return _filmRepository.GetAll();
            }
        }

        public bool Insert(Film entity)
        {
            return _filmRepository.Insert(entity);
        }

        public bool InsertRange(List<Film> entities, bool isOveride = true)
        {
            return _filmRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Film entity)
        {
            return _filmRepository.Update(entity);
        }
    }
}
