using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models.DTOs.Customers;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;

        public CustomerController(ICustomerServices customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Agent")]
        [HttpGet("{GetAllCustomers}")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetCustomerById/{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            try
            {
                var customer = _customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found" });
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("AddCustomer/")]
        public IActionResult AddCustomer([FromBody] CreateCustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _customerService.AddCustomer(customerDto);
                return Ok(new { message = "Customer added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Update/{customerId}")]
        public IActionResult UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                customerDto.CustomerID = customerId;
                _customerService.UpdateCustomer(customerDto);
                return Ok(new { message = "Customer updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpDelete("Delete/{customerId}")]
        public IActionResult DeleteCustomer(Guid customerId)
        {
            try
            {
                _customerService.DeleteCustomer(customerId);
                return Ok(new { message = "Customer deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
