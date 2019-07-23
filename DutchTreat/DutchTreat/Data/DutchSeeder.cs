using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext context, IHostingEnvironment env, UserManager<StoreUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            StoreUser User = await _userManager.FindByEmailAsync("shawn@dutchtreat.com");

            if (User == null)
            {
                var user = new StoreUser
                {
                    FirstName = "Shawn",
                    LasttName = "Wildermuth",
                    Email = "shawn@dutchtreat.com",
                    UserName = "shawn@dutchtreat.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0r!");
                if (result != IdentityResult.Success)
                    throw new InvalidOperationException("Could not create new user in seeder");
            }

            if (!_context.Products.Any())
            {
                //Need to create sample data
                var file = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json).ToList();

                var order = _context.Orders.FirstOrDefault(o => o.Id == 1);

                if (order != null)
                {
                    order.User = User;
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
