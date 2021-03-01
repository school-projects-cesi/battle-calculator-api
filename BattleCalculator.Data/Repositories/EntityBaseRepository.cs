using BattleCalculator.Data.Contexts;
using BattleCalculator.Data.Repositories.Abstract;
using BattleCalculator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BattleCalculator.Data.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        protected ApplicationDbContext _context;

        public EntityBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public virtual async Task<int> CountAsync()
            => await _context.Set<T>().CountAsync();

        public virtual async Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.ToListAsync();
        }

        public virtual async Task<T> FindAsync(int id)
            => await _context.Set<T>().FindAsync(id);
        public virtual async Task<T> FindAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public virtual void Update(T entity)
            => _context.Set<T>().Update(entity);

        public virtual void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public virtual async Task CommitAsync()
            => await _context.SaveChangesAsync();
    }
}
