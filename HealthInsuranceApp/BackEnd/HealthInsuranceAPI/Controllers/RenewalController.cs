using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models.DTOs.Payments;
using HealthInsuranceAPI.Models.DTOs.Renewal;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenewalController : ControllerBase
    {
        private readonly IRenewalService _renewalService;

        public RenewalController(IRenewalService renewalService)
        {
            _renewalService = renewalService;
        }

        [Authorize]
        [HttpPost("add/{customerPolicyId}")]
        public IActionResult AddRenewal(Guid customerPolicyId)
        {
            try
            {
                var renewalResponse = _renewalService.AddRenewal(customerPolicyId);
                return Ok(new
                {
                    message = "Renewal added successfully.",
                    RenewalID = renewalResponse.RenewalID,
                    RenewalAmount = renewalResponse.RenewalAmount
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }





            [Authorize]
        [HttpGet("{renewalId}")]
        public IActionResult GetRenewalById(Guid renewalId)
        {
            try
            {
                var renewal = _renewalService.GetRenewalById(renewalId);
                if (renewal == null)
                {
                    return NotFound(new { message = "Renewal not found" });
                }
                return Ok(renewal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("details/{renewalId}")]
        public IActionResult GetRenewalDetails(Guid renewalId)
        {
            try
            {
                var renewal = _renewalService.GetRenewalById(renewalId);
                if (renewal == null)
                {
                    return NotFound(new { message = "Renewal not found" });
                }
                return Ok(renewal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{renewalId}")]
        public IActionResult UpdateRenewal(Guid renewalId, [FromBody] UpdateRenewalDTO renewalDto)
        {
            try
            {
                renewalDto.RenewalID = renewalId;
                _renewalService.UpdateRenewal(renewalDto);
                return Ok(new { message = "Renewal updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpDelete("{renewalId}")]
        public IActionResult DeleteRenewal(Guid renewalId)
        {
            try
            {
                _renewalService.DeleteRenewal(renewalId);
                return Ok(new { message = "Renewal deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllRenewals()
        {
            try
            {
                var renewals = _renewalService.GetAllRenewals();
                return Ok(renewals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
