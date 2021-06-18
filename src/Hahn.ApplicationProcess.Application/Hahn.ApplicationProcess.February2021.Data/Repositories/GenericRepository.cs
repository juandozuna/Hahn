using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Repositories
{
    using Domain.Models;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        /// <summary>
        /// Represents the Db Context
        /// </summary>
        protected readonly HahnDbContext Context;

        /// <summary>
        /// Database entity associated to given type
        /// </summary>
        protected DbSet<T> Entities;

        /// <summary>
        /// Builds a new instance of the given repository
        /// </summary>
        /// <param name="context"></param>
        protected GenericRepository(HahnDbContext context)
        {
            this.Context = context;
            Entities = context.Set<T>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAll() => await Entities.ToListAsync();

        /// <inheritdoc />
        public async Task<T> Get(int id) => await Entities.SingleOrDefaultAsync(e => e.Id == id);

        /// <inheritdoc />
        public async Task<int> Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);

            await Context.SaveChangesAsync();

            return entity.Id;
        } 

        /// <inheritdoc />
        public async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<T> Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc />
        public Task Save() => Context.SaveChangesAsync();
    }
}
