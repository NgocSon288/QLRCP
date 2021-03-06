using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;

namespace BTTH1.Services
{
    public interface IMemberService
    {
        bool InsertRange(List<Member> entities, bool isOveride = true);

        bool Insert(Member entity);

        List<Member> GetAll();

        Member GetByID(Guid id); 

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Member entity);
    }

    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _categoryMemberRepository;


        public MemberService()
        {
            this._categoryMemberRepository = new MemberRepository();
        }

        public bool DeleteAll()
        {
            return _categoryMemberRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryMemberRepository.DeleteByID(id);
        }

        public List<Member> GetAll()
        {
            return _categoryMemberRepository.GetAll();
        }

        public Member GetByID(Guid id)
        {
            return _categoryMemberRepository.GetByID(id);
        }

        public bool Insert(Member entity)
        {
            return _categoryMemberRepository.Insert(entity);
        }

        public bool InsertRange(List<Member> entities, bool isOveride = true)
        {
            return _categoryMemberRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Member entity)
        {
            return _categoryMemberRepository.Update(entity);
        }
    }
}