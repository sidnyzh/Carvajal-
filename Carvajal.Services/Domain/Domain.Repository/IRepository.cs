using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> GetById(int id);

        //Task<TEntity> GetByName(string name);

        Task<bool> Insert(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<bool> Delete(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        

    }
}
