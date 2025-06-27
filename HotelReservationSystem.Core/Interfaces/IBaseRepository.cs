using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIDAsync(int id);
        public Task AddAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task DeleteAsync(T obj);
        public Task SaveAsync();

    }
}
