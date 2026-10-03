using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dbSet;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (result == null) return false;

                _dbSet.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreateAt = DateTime.UtcNow;
                _dbSet.Add(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public async Task<bool> ExistAsync(Guid id)
            => await _dbSet.AnyAsync(x => x.Id.Equals(id));

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));
                if (result == null) return null;
                
                entity.UpdateAt = DateTime.UtcNow;
                entity.CreateAt = result.CreateAt;

                _context.Entry(result).CurrentValues.SetValues(entity);                
                await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }
    }
}
