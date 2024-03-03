using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        T GetById(int id);
        IQueryable<T> All();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        void Create(T entity, bool saveChanges = true);
        void CreateRange(List<T> entities, bool saveChanges = true);
        void Update(T entity, bool saveChanges = true);
        Task<T> GetByIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> AllAsync();
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression);
        Task<T> WhereFirstAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity, bool saveChanges = true);
        Task CreateRangeAsync(List<T> entities, bool saveChanges = true);
        public void Delete(int id, bool saveChanges = true);
        public void DeleteRange(List<int> ids, bool saveChanges = true);
        Task UpdateAsync(T entity, bool saveChanges = true);
    }
}
