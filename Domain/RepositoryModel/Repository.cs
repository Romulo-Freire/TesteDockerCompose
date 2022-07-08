using Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryModel
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(FirstDbContext context)
        {
            Context = context;
        }
    
        public FirstDbContext Context { get; private set; }
        protected DbSet<T> Set => Context.Set<T>();
        public IQueryable<T> Query => Set;

        public async Task<List<T>> Listar(
                Expression<Func<T, bool>> filter = null             
            )
        {
            IQueryable<T> query = Context.Set<T>();
            
            if (filter != null)
            {
                query.Where(filter);

                return await query.AsNoTracking()
                            .ToListAsync();  
            }

            return new List<T>();
        }

        public void Salvar(T entity)
        {
            var props = typeof(T)
                 .GetProperties()
                 .Where(prop => Attribute.IsDefined(prop,
                                     typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));

            var codeValue = (props.First().GetValue(entity).GetType().Name == "Int64" ? (long)props.First()
                .GetValue(entity) : (int)props.First().GetValue(entity));

            if (codeValue == 0)
            {
                this.Adicionar(entity);
            }
            else
            {
                this.Atualizar(entity);
            }
        }
        private async void Adicionar(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
               await Set.AddAsync(entity);
        }
        private void Atualizar(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                Set.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public void Apagar(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                Set.Attach(entity);
            Set.Remove(entity);
        }
    }

}
