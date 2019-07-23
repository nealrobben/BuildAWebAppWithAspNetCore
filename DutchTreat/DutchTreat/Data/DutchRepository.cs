using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItems);
        Order GetOrderById(string userName, int id);

        bool SaveAll();
        void AddEntity(object model);
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

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                _logger.LogInformation("GetAllOrders called");

                if (includeItems)
                {
                    return _ctx.Orders.Include(x => x.Items).ThenInclude(x => x.Product).OrderBy(x => x.OrderNumber)
                        .ToList();
                }
                else
                {
                    return _ctx.Orders.OrderBy(x => x.OrderNumber)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Faied to get all orders: {e}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItems)
        {
            try
            {
                _logger.LogInformation("GetAllOrders called");

                if (includeItems)
                {
                    return _ctx.Orders.Where(x => x.User.UserName == userName).Include(x => x.Items)
                        .ThenInclude(x => x.Product).OrderBy(x => x.OrderNumber)
                        .ToList();
                }
                else
                {
                    return _ctx.Orders.OrderBy(x => x.OrderNumber)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Faied to get all orders: {e}");
                return null;
            }
        }

        public Order GetOrderById(string userName, int id)
        {
            try
            {
                _logger.LogInformation("GetAllOrders called");

                return _ctx.Orders.Where(x => x.Id == id && x.User.UserName == userName).Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Faied to get all orders: {e}");
                return null;
            }
        }
    }
}
