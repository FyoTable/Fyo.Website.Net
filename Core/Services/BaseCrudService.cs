using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Fyo.Interfaces;
using Fyo.Models;

namespace Fyo.Services {
    public abstract class BaseCrudService<T> : SimpleCrudService<T> where T : BaseModel
    {
        public BaseCrudService(DataContext context) : base(context) {
            
        }

        public override T Create(T newEntity)
        {
            newEntity.ModifiedDate = DateTime.UtcNow;
            
            DbSet.Add(newEntity);
            _context.SaveChanges();

            return newEntity;
        }

        public override void Delete(T deletedEntity)
        {
            deletedEntity.IsDeleted = true;
            deletedEntity.ModifiedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public override T Get(long id)
        {
            return DbSet.FirstOrDefault(entity => entity.ID == id);
        }

        public override IQueryable<T> GetAll(){
            return DbSet.Where(x => !x.IsDeleted);
        }

        public override IQueryable<T> GetAll(string[] properties){
            var query = DbSet.Where(x => !x.IsDeleted);

            foreach(var navProperty in properties){
                query.Include(navProperty);
            }

            return query;
        }

        public override T Update(T updatedEntity)
        {
            updatedEntity.ModifiedDate = DateTime.UtcNow;

            _context.SaveChanges();

            return updatedEntity;
        }
    }

    public abstract class SimpleCrudService<T> : ICrudService<T> where T : class
    {
        protected readonly DataContext _context;
        protected DbSet<T> DbSet;

        public SimpleCrudService(DataContext context){
            _context = context;
            DbSet = context.Set<T>();
        }

        public virtual T Create(T newEntity)
        {
            DbSet.Add(newEntity);
            _context.SaveChanges();

            return newEntity;
        }

        public virtual void Delete(T deletedEntity)
        {
            _context.SaveChanges();
        }

        public virtual T Get(long id)
        {
            return DbSet.FirstOrDefault();
        }

        public virtual IQueryable<T> GetAll(){
            return DbSet;
        }

        public virtual IQueryable<T> GetAll(string[] properties){
            var query = DbSet;

            foreach(var navProperty in properties){
                query.Include(navProperty);
            }

            return query;
        }

        public virtual T Update(T updatedEntity)
        {
            _context.SaveChangesAsync();

            return updatedEntity;
        }
    }
}