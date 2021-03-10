using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTTH1.Services
{
    public interface IMemberService
    {
        bool InsertRange(List<Member> entities, bool isOveride = true);

        bool Insert(Member entity);

        List<Member> GetAll();

        Member GetByID(Guid id);

        Member GetByUsername(string username);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Member entity);
    }

    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;


        public MemberService()
        {
            this._memberRepository = new MemberRepository();
        }

        public bool DeleteAll()
        {
            return _memberRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _memberRepository.DeleteByID(id);
        }

        public List<Member> GetAll()
        {
            return _memberRepository.GetAll();
        }

        public Member GetByID(Guid id)
        {
            return _memberRepository.GetByID(id);
        }

        public Member GetByUsername(string username)
        {
            return GetAll().FirstOrDefault(m => m.Username == username);
        }

        public bool Insert(Member entity)
        {
            return _memberRepository.Insert(entity);
        }

        public bool InsertRange(List<Member> entities, bool isOveride = true)
        {
            return _memberRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Member entity)
        {
            return _memberRepository.Update(entity);
        }
    }
}