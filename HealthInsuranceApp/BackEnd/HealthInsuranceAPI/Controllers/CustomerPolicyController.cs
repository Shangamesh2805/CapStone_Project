using HealthInsuranceAPI.Models.DTOs.Claims;
using HealthInsuranceAPI.Models.DTOs.CustomerPolicy;
using HealthInsuranceAPI.Models.DTOs.Payments;
using HealthInsuranceAPI.Models.DTOs.Renewal;
using HealthInsuranceAPI.Models.DTOs.Revival;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerPolicyController : ControllerBase
    {
        private readonly ICustomerPolicyService _customerPolicyService;

        public CustomerPolicyController(ICustomerPolicyService customerPolicyService)
        {
            _customerPolicyService = customerPolicyService;
        }

        [Authorize]
        [HttpGet("Get")]
        public IActionResult GetAllCustomerPolicies()
        {
            try
            {
                var policies = _customerPolicyService.GetAllCustomerPolicies();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Get/{customerPolicyId}")]
        public IActionResult GetCustomerPolicyById(Guid customerPolicyId)
        {
            try
            {
                var policy = _customerPolicyService.GetCustomerPolicyById(customerPolicyId);
                if (policy == null)
                {
                    return NotFound(new { message = "Customer policy not found" });
                }
                return Ok(policy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetByCustomer")]
        public IActionResult GetCustomerPolicyByCustomerId()
        {
            try
            {
                // Retrieve the UserId from the current user's claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                var userId = Guid.Parse(userIdClaim.Value);

                // Retrieve the customer details using the userId
                var customer = _customerPolicyService.GetCustomerByUserId(userId);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found" });
                }

                var policies = _customerPolicyService.GetCustomerPolicyByCustomerId(customer.CustomerID);
                if (policies == null || !policies.Any())
                {
                    return NotFound(new { message = "Customer policies not found" });
                }

                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpPost("ApplyPolicy")]
        public IActionResult CreateCustomerPolicy([FromBody] CreateCustomerPolicyDTO customerPolicyDto)
        {
            try
            {
                // Retrieve the UserId from the current user's claims
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                var userId = Guid.Parse(userIdClaim.Value);

                _customerPolicyService.CreateCustomerPolicy(customerPolicyDto, userId);
                return Ok(new { message = "Customer policy created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Update/{customerPolicyId}")]
        public IActionResult UpdateCustomerPolicy(Guid customerPolicyId, [FromBody] UpdateCustomerPolicyDTO customerPolicyDto)
        {
            try
            {
                customerPolicyDto.CustomerPolicyID = customerPolicyId;
                _customerPolicyService.UpdateCustomerPolicy(customerPolicyDto);
                return Ok(new { message = "Customer policy updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete/{customerPolicyId}")]
        public IActionResult DeleteCustomerPolicy(Guid customerPolicyId)
        {
            try
            {
                _customerPolicyService.DeleteCustomerPolicy(customerPolicyId);
                return Ok(new { message = "Customer policy deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("{customerPolicyId}/check-renewal-eligibility")]
        public IActionResult CheckRenewalEligibility(Guid customerPolicyId)
        {
            try
            {
                _customerPolicyService.CheckRenewalEligibility(customerPolicyId);
                return Ok(new { message = "Renewal eligibility checked successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
