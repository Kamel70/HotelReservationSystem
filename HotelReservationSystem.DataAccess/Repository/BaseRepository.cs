using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.DataAccess.Models;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        HotelReservationDBContext _context;
        public BaseRepository(HotelReservationDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
        }

        public Task DeleteAsync(T obj)
        {
           _context.Set<T>().Remove(obj);
            return Task.CompletedTask;

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(T obj)
        {
            _context.Set<T>().Update(obj);
            return Task.CompletedTask;
        }
    }
}
