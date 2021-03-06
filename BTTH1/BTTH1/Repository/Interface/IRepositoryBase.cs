using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTH1.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        bool InsertRange(List<T> entities, bool isOveride = true);

        bool Insert(T entity);

        List<T> GetAll();

        T GetByID(Guid id);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(T entity);
    }
}
