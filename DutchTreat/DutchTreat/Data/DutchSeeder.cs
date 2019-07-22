using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using DutchTreat.Data.Entities;
using Newtonsoft.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _env;

        public DutchSeeder(DutchContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                //Need to create sample data
                var file = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json).ToList();

                var order = _context.Orders.FirstOrDefault(o => o.Id == 1);

                if (order != null)
                {
                    order.Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _context.Products.AddRange(products);
                _context.SaveChanges();
            }
        }
    }
}
