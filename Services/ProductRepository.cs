﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Core_WebApp.Services
{
    public class ProductRepository : IRepository<Product, int>
    {
        private readonly AppDbContext ctx;
        public ProductRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            var res = await ctx.Products.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.Products.FindAsync(id);
            if (res != null)
            {
                ctx.Products.Remove(res);
                await ctx.SaveChangesAsync(); // commit transactions
                return true;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await ctx.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAync()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var res = await ctx.Products.FindAsync(id);
            if (res != null)
            {
                res.ProductId = entity.ProductId;
                res.ProductName = entity.ProductName;
                res.Manufacturer = entity.Manufacturer;
                res.Description = entity.Description;
                res.Price = entity.Price;
                res.Category.CategoryRowId = entity.Category.CategoryRowId;
                await ctx.SaveChangesAsync(); // commit transactions
                return res;
            }
            return res;
        }
    }
}
