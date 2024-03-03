using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces.IServices
{
    public interface IServiceBase<T>
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> ListAsync();
        Task<T> GetById(int id);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
    }
}
