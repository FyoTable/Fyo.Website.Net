using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Fyo.Interfaces;
using Fyo.Models;

namespace Fyo.Services {
    public abstract class BaseCrudService<T> : ICrudService<T> where T : BaseModel
    {
        private readonly DataContext _context;
        protected DbSet<T> DbSet;

        public BaseCrudService(DataContext context){
            _context = context;
            DbSet = context.Set<T>();
        }

        public virtual T Create(T newEntity)
        {
            newEntity.ModifiedDate = DateTime.UtcNow;
            
            DbSet.Add(newEntity);
            _context.SaveChanges();

            return newEntity;
        }

        public virtual void Delete(T deletedEntity)
        {
            deletedEntity.IsDeleted = true;
            deletedEntity.ModifiedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public virtual T Get(long id)
        {
            return DbSet.FirstOrDefault(entity => entity.ID == id);
        }

        public virtual IQueryable<T> GetAll(){
            return DbSet.Where(x => !x.IsDeleted);
        }

        public virtual IQueryable<T> GetAll(string[] properties){
            var query = DbSet.Where(x => !x.IsDeleted);

            foreach(var navProperty in properties){
                query.Include(navProperty);
            }

            return query;
        }

        public virtual T Update(T updatedEntity)
        {
            updatedEntity.ModifiedDate = DateTime.UtcNow;

            _context.SaveChangesAsync();

            return updatedEntity;
        }
    }
}