using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SchoolSystem.DataLayer.Repositories.Interfaces;
using SchoolSystem.RelationalEngine;

namespace SchoolSystem.DataLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly OrmEntitySet<T> Context;

        protected GenericRepository(OrmEntitySet<T> context)
        {
            Context = context;
        }

        public async Task AddAsync(T entity)
        {
            Exception exception = null;
            await Task.Run(() =>
            {
                try
                {
                    Context.Insert(entity);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });
            if (exception != null)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<IEnumerable<T>> Find()
        {
            return await Task.Run(() => Context.Get());
        }

        public async  Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicateExpression)
        {
            return await Task.Run(() => Context.Where(predicateExpression).Get());
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicateExpression)
        {
            return await Task.Run(() => Context.Where(predicateExpression).Get().ToList().Count() != 0 
                ?Context.Where(predicateExpression).Get().ToList()[0] : null);
        }

        public async Task RemoveAsync(Expression<Func<T, bool>> predicateExpression)
        {
            await Task.Run(() =>
                {
                    Context.Where(predicateExpression).Delete();
                });
            }
        public async Task UpdateAsync(T entity, Expression<Func<T, bool>> predicateExpression)
        {
            Exception exception = null;
            await Task.Run(() =>
            {
                try
                {
                    Context.Where(predicateExpression).Update(entity);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });
            if (exception != null)
            {
                throw new Exception(exception.Message);
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => Context.Get());
        }
    }
}
