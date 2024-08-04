using HealthInsuranceAPI.Models.DTOs.Policy;
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
    public class InsurancePolicyController : ControllerBase
    {
        private readonly IInsurancePolicy _insurancePolicyService;

        public InsurancePolicyController(IInsurancePolicy insurancePolicyService)
        {
            _insurancePolicyService = insurancePolicyService;
        }

        
        [HttpGet("GetAllPolicies")]
        public IActionResult GetAllPolicies()
        {
            try
            {
                var policies = _insurancePolicyService.GetAllPolicies();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Get/{policyId}")]
        public IActionResult GetPolicyById(Guid policyId)
        {
            try
            {
                var policy = _insurancePolicyService.GetPolicyById(policyId);
                if (policy == null)
                {
                    return NotFound(new { message = "Policy not found" });
                }
                return Ok(policy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Agent")]
        [HttpPost("AddPolicy")]
        public IActionResult AddPolicy([FromBody] CreatePolicyDTO policyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _insurancePolicyService.AddPolicy(policyDto);
                return Ok(new { message = "Policy added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Agent")]
        [HttpPut("Update/{policyId}")]
        public IActionResult UpdatePolicy(Guid policyId, [FromBody] UpdatePolicyDTO policyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                policyDto.PolicyID = policyId;
                _insurancePolicyService.UpdatePolicy(policyDto);
                return Ok(new { message = "Policy updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{policyId}")]
        public IActionResult DeletePolicy(Guid policyId)
        {
            try
            {
                _insurancePolicyService.DeletePolicy(policyId);
                return Ok(new { message = "Policy deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
