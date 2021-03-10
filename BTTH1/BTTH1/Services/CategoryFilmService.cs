using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;

namespace BTTH1.Services
{
    public interface ICategoryFilmService
    {
        bool InsertRange(List<CategoryFilm> entities, bool isOveride = true);

        bool Insert(CategoryFilm entity);

        List<CategoryFilm> GetAll();

        CategoryFilm GetByID(Guid id);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(CategoryFilm entity);
    }

    public class CategoryFilmService : ICategoryFilmService
    {
        private readonly ICategoryFilmRepository _categoryFilmRepository;

        public CategoryFilmService()
        {
            this._categoryFilmRepository = new CategoryFilmRepository();
        }

        public bool DeleteAll()
        {
            return _categoryFilmRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryFilmRepository.DeleteByID(id);
        }

        public List<CategoryFilm> GetAll()
        {
            return _categoryFilmRepository.GetAll();
        }

        public CategoryFilm GetByID(Guid id)
        {
            return _categoryFilmRepository.GetByID(id);
        }

        public bool Insert(CategoryFilm entity)
        {
            return _categoryFilmRepository.Insert(entity);
        }

        public bool InsertRange(List<CategoryFilm> entities, bool isOveride = true)
        {
            return _categoryFilmRepository.InsertRange(entities, isOveride);
        }

        public bool Update(CategoryFilm entity)
        {
            return _categoryFilmRepository.Update(entity);
        }
    }
}