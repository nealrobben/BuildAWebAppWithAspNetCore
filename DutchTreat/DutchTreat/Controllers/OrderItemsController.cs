using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);

            if (order != null)
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));

            _logger.LogError("Could not find order items for order");

            return NotFound("Could not find order items for order");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetOrderById(orderId);

            var item = order?.Items.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
            }

            _logger.LogError("Could not find order items for order");

            return NotFound("Could not find order items for order");
        }
    }
}