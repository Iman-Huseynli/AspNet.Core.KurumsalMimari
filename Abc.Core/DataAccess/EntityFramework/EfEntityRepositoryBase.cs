using Abc.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Abc.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<T, TContext> : IEntityRepository<T>
        where T : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(T entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<T>().FirstOrDefault(filter);
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<T>().ToList() : context.Set<T>().Where(filter).ToList();
            }
        }

        public void Update(T entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
