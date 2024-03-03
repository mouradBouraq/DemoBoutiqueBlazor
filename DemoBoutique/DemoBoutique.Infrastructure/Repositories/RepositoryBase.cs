using DemoBoutique.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Infrastructure.Repos
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContextDemoBoutique RepositoryContext { get; set; }
        protected RepositoryBase(DbContextDemoBoutique repositoryContext) => RepositoryContext = repositoryContext;
        public IQueryable<T> All() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> Where(Expression<Func<T, bool>> expression) => RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => await RepositoryContext.Set<T>().AnyAsync(expression);
        public void Create(T entity, bool saveChanges = true)
        {
            RepositoryContext.Set<T>().Add(entity);
            if (saveChanges) RepositoryContext.SaveChanges();
        }
        public void CreateRange(List<T> entities, bool saveChanges = true)
        {
            RepositoryContext.Set<T>().AddRange(entities);
            if (saveChanges) RepositoryContext.SaveChanges();
        }
        public void Update(T entity, bool saveChanges = true)
        {
            var initialEntity = RepositoryContext.Entry(entity);
            int Id = Convert.ToInt32(initialEntity.CurrentValues["Id"]);
            var param = Expression.Parameter(typeof(T), "entity");
            var filtre = DynamicExpressionParser.ParseLambda(new[] { param }, null, $"(entity.Id == {Id})");
            var entityFound = RepositoryContext.Set<T>().Where(filtre).FirstOrDefault();
            if (entityFound == null) { return; }
            RepositoryContext.Entry(entityFound).CurrentValues.SetValues(entity);
            if (saveChanges)
            {
                RepositoryContext.SaveChanges();
            }
        }
        public async Task UpdateAsync(T entity, bool saveChanges = true)
        {
            var initialEntity = RepositoryContext.Entry(entity);
            int Id = Convert.ToInt32(initialEntity.CurrentValues["Id"]);
            var param = Expression.Parameter(typeof(T), "entity");
            var filtre = DynamicExpressionParser.ParseLambda(new[] { param }, null, $"(entity.Id == {Id})");
            var entityFound = RepositoryContext.Set<T>().Where(filtre).FirstOrDefault();
            if (entityFound == null) { return; }
            RepositoryContext.Entry(entityFound).CurrentValues.SetValues(entity);
            if (saveChanges)
            {
                await RepositoryContext.SaveChangesAsync();
            }
        }
        public void DeleteRange(List<int> ids, bool saveChanges = true) { ids.ForEach(id => Delete(id, saveChanges)); }
        public async Task<T> CreateAsync(T entity, bool saveChanges = true)
        {
            var entit = await RepositoryContext.Set<T>().AddAsync(entity);
            if (saveChanges)
            {
                await RepositoryContext.SaveChangesAsync();
            }
            return entit.Entity;
        }
        public async Task CreateRangeAsync(List<T> entities, bool saveChanges = true)
        {
            await RepositoryContext.Set<T>().AddRangeAsync(entities);
            if (saveChanges)
            {
                await RepositoryContext.SaveChangesAsync();
            }
        }
        public async Task<List<T>> AllAsync()
        {
            return await All().ToListAsync();
        }
        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return await Where(expression).ToListAsync();
        }

        public async Task<T> WhereFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await Where(expression).FirstOrDefaultAsync();
        }
        public void Delete(int id, bool saveChanges = true)
        {
            var param = Expression.Parameter(typeof(T), "entity");
            var filtre = DynamicExpressionParser.ParseLambda(new[] { param }, null, $"(entity.Id == {id})");
            var entityFound = RepositoryContext.Set<T>().Where(filtre).FirstOrDefault();
            if (entityFound == null) { return; }
            RepositoryContext.Set<T>().Remove(entityFound);
            if (saveChanges)
            {
                RepositoryContext.SaveChanges();
            }
        }
     
        public T GetById(int id) => RepositoryContext.Set<T>().Find(id);

        public async Task<T> GetByIdAsync(int id)
        {
            var entitie = await RepositoryContext.Set<T>().FindAsync(id);
            return entitie;
        }

        public void Save()
        {
            RepositoryContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }
}
