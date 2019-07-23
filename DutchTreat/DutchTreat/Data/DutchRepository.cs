using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }

    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts called");

                return _ctx.Products.OrderBy(x => x.Title).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Faied to get all products: {e}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products.Where(x => x.Category == category).OrderBy(x => x.Title).ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
