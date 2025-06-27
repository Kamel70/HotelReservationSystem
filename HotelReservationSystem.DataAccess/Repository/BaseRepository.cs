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
        public async Task<T> AddAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return obj;
        }

        public void Delete(T obj)
        {
           _context.Set<T>().Remove(obj);

        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T obj)
        {
            _context.Set<T>().Update(obj);
            return obj;
        }
    }
}
