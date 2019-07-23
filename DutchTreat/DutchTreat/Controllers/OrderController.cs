﻿using System;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api")]
    public class OrderController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductController> _logger;

        public OrderController(IDutchRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get products {e}");
                return BadRequest("Failed to get products");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);

                if (order != null)
                    return Ok(order);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get order {e}");
                return BadRequest("Failed to get order");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order model)
        {
            try
            {
                _repository.AddEntity(model);

                if (_repository.SaveAll())
                    return Created($"/api/orders/{model.Id}", model);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add order {e}");
            }

            return BadRequest("Failed to add order");
        }
    }
}