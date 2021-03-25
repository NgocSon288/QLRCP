using BTTH1.Models;
using BTTH1.Repository;
using BTTH1.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTTH1.Services
{
    public interface IOrderService
    {
        bool InsertRange(List<Order> entities, bool isOveride = true);

        bool Insert(Order entity);

        List<Order> GetAll();

        List<Order> GetAllByMemberID(Guid memberID);

        Order GetByID(Guid id);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Order entity);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _categoryMemberRepository;

        public OrderService()
        {
            this._categoryMemberRepository = new OrderRepository();
        }

        public bool DeleteAll()
        {
            return _categoryMemberRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryMemberRepository.DeleteByID(id);
        }

        public List<Order> GetAll()
        {
            return _categoryMemberRepository.GetAll();
        }

        public List<Order> GetAllByMemberID(Guid memberID)
        {
            return _categoryMemberRepository.GetAll().Where(o => o.MemberID == memberID).ToList();
        }

        public Order GetByID(Guid id)
        {
            return _categoryMemberRepository.GetByID(id);
        }

        public bool Insert(Order entity)
        {
            return _categoryMemberRepository.Insert(entity);
        }

        public bool InsertRange(List<Order> entities, bool isOveride = true)
        {
            return _categoryMemberRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Order entity)
        {
            return _categoryMemberRepository.Update(entity);
        }
    }
}