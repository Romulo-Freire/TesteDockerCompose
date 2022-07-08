using Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryModel
{
    public interface IRepository<T> where T : class 
    {
        FirstDbContext Context { get; }
        IQueryable<T> Query { get; }
        void Salvar(T entity);
        void Apagar(T entity);
        Task<List<T>> Listar(Expression<Func<T, bool>> filter = null);
    }
}
