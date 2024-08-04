using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Claims;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllClaims()
        {
            try
            {
                var claims = _claimService.GetAllClaims();
                return Ok(claims);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Get/{claimId}")]
        public IActionResult GetClaimById(Guid claimId)
        {
            try
            {
                var claim = _claimService.GetClaimById(claimId);
                if (claim == null)
                {
                    return NotFound(new { message = "Claim not found" });
                }
                return Ok(claim);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("RaiseClaim")]
        public IActionResult RaiseClaim([FromBody] CreateClaimDTO claimDto)
        {
            try
            {
                _claimService.RaiseClaim(claimDto);
                return Ok(new { message = "Claim raised successfully." });
            }
            catch (Exception ex)
            {
                // Log detailed error
                Console.WriteLine($"Error in RaiseClaim API: {ex.Message}");
                Console.WriteLine(ex.StackTrace); // Log stack trace

                // Return detailed error message for development
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpPut("Update/{claimId}")]
        public IActionResult UpdateClaim(Guid claimId, [FromBody] UpdateClaimDTO claimDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                claimDto.ClaimID = claimId;
                _claimService.UpdateClaim(claimDto);
                return Ok(new { message = "Claim updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete/{claimId}")]
        public IActionResult DeleteClaim(Guid claimId)
        {
            try
            {
                _claimService.DeleteClaim(claimId);
                return Ok(new { message = "Claim deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{claimId}/status")]
        public IActionResult UpdateClaimStatus(Guid claimId, [FromBody] string status)
        {
            try
            {
                _claimService.UpdateClaimStatus(claimId, status);
                return Ok(new { message = "Claim status updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("policy/{customerPolicyId}")]
        public IActionResult GetClaimsByCustomerPolicy(Guid customerPolicyId)
        {
            try
            {
                var claims = _claimService.GetClaimsByCustomerPolicy(customerPolicyId);
                return Ok(claims);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
