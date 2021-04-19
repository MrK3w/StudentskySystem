using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolSystem.DataLayer.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicateExpression);


        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicateExpression);


        Task AddAsync(T entity);


        Task UpdateAsync(T entity, Expression<Func<T, bool>> predicateExpression);

        Task<IEnumerable<T>> Find();

        Task RemoveAsync(Expression<Func<T, bool>> predicateExpression);
    }
}
