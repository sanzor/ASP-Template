using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASPT.DataAccess {
    public interface IGenericRepository<T> {
        Task InsertAsync(T entity);
        bool Remove(object id);
        void Update(T entity);
        Task<T> GetByIdAsync(object id,string includeProperties="");
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                                     int? skip = null,
                                     int? take = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     string includeProperties = "");
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void UpdateRange(IEnumerable<T> elements);

        void RemoveRange(IEnumerable<object> elements);


    }
}
