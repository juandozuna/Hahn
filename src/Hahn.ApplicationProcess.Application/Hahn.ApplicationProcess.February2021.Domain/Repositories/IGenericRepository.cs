using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Repositories
{
    using Models;

    /// <summary>
    /// Represents the generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> 
        where T : BaseModel
    {
        /// <summary>
        /// Get's all of the entities
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Gets an entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> Get(int id);

        /// <summary>
        /// Inserts a new entity into the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> Insert(T entity);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity"></param>
        public Task Update(T entity);

        /// <summary>
        /// Removes an entity from Db
        /// </summary>
        /// <param name="entity"></param>
        public Task<T> Delete(T entity);

        /// <summary>
        /// Saves changes
        /// </summary>
        /// <returns></returns>
        public Task Save();
    }
}
