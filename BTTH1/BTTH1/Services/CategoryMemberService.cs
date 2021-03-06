using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;

namespace BTTH1.Services
{
    public interface ICategoryMemberService
    {
        bool InsertRange(List<CategoryMember> entities, bool isOveride = true);

        bool Insert(CategoryMember entity);

        List<CategoryMember> GetAll();

        CategoryMember GetByID(Guid id);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(CategoryMember entity);
    }

    public class CategoryMemberService : ICategoryMemberService
    {
        private readonly ICategoryMemberRepository _categoryMemberRepository;

        public CategoryMemberService()
        {
            this._categoryMemberRepository = new CategoryMemberRepository();
        }

        public bool DeleteAll()
        {
            return _categoryMemberRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryMemberRepository.DeleteByID(id);
        }

        public List<CategoryMember> GetAll()
        {
            return _categoryMemberRepository.GetAll();
        }

        public CategoryMember GetByID(Guid id)
        {
            return _categoryMemberRepository.GetByID(id);
        }

        public bool Insert(CategoryMember entity)
        {
            return _categoryMemberRepository.Insert(entity);
        }

        public bool InsertRange(List<CategoryMember> entities, bool isOveride = true)
        {
            return _categoryMemberRepository.InsertRange(entities, isOveride);
        }

        public bool Update(CategoryMember entity)
        {
            return _categoryMemberRepository.Update(entity);
        }
    }
}