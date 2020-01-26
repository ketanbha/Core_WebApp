﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.Services
{
    /// <summary>
    /// The repository interface i.e. use to define standard repository methods for DAL
    /// Generic Constraints <TEntity, in TPk> where TEntity : class
    /// Async method for performing DAL operations
    /// </summary>
    public interface IRepository<TEntity, in TPk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAync();
        Task<TEntity> GetAsync(TPk id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TPk id, TEntity entity);
        Task<bool> DeleteAsync(TPk id);
    }
}
