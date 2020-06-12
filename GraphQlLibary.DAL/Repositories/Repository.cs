using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GraphQlLibary.Domain.Interfaces.Repository;
using GraphQlLibary.DAL.Context;

namespace GraphQlLibary.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;

        public Repository(LibaryContext libaryContext)
        {
            _dbSet = libaryContext.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public T Get(string id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public bool IsExist(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
