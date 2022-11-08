using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repository;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {

        private readonly PedidosContext _Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(PedidosContext Context)
        {
            _Context = Context;
            _entities = _Context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _Context.Set<TEntity>().FindAsync(id);
        }
        

        public async Task<bool> Insert(TEntity entity)
        {
            await _entities.AddAsync(entity);
            int changes = await _Context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(TEntity entity)
        {
            _entities.Update(entity);
            int changes = await _Context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Delete(int id)
        {
            TEntity entity = await GetById(id);
            _Context.Remove(entity);
            int changes = await _Context.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _Context.Set<TEntity>().ToListAsync();
        }

    }
}
