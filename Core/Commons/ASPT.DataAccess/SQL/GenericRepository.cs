using ASPT.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASPT.DataAccess {
    public class GenericRepository<T> : IGenericRepository<T> where T:class,IIdentifiable {
        private ASPTContext context { get; set; }
        private DbSet<T> table { get; set; }
        public GenericRepository(ASPTContext context) {
            this.context = context;
            this.table = this.context.Set<T>();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync() {
            var tran =await this.context.Database.BeginTransactionAsync();
            return tran;
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> filter = null, int? skip = null, int? take = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "") {
            await Task.Delay(1);
            IQueryable<T> query = this.table;
            if (filter != null) {
                query = query.Where(filter);
            }
            foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }
            if (orderBy != null) {
                query = orderBy(query);
            }
            if (skip.HasValue) {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue) {
                query = query.Take(take.Value);
            }
            return query;
        }

        public async Task<T> GetByIdAsync(object id, string includeProperties = "") {
            T model = null;
            if (includeProperties == "") {
                return this.table.Find(id);
            }
            foreach(var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                model = this.table.Include(property).SingleOrDefault(x => x.Id == id);
            }
            return await Task.FromResult(model);
        }

        public async Task InsertAsync(T entity) {
            var add = await this.table.AddAsync(entity);
        }

        public bool Remove(object id) {
            T existing = this.table.Find(id);
            if (existing == null) {
                return false;
            }
            if (context.Entry(existing).State == EntityState.Detached) {
                table.Attach(existing);
            }
            table.Remove(existing);
            return true;
        }

        public void RemoveRange(IEnumerable<object> elements) {
            this.context.RemoveRange(elements);
        }

        public Task SaveAsync() {
            return this.context.SaveChangesAsync();
        }

        public void Update(T entity) {
            var entry = this.context.Entry(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> elements) {
            this.context.UpdateRange(elements);
        }
        
    }
}
