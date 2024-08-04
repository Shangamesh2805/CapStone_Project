using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Agents;
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
    public class AgentController : ControllerBase
    {
        private readonly IAgentServices _agentService;

        public AgentController(IAgentServices agentService)
        {
            _agentService = agentService;
        }

        [Authorize]
        [HttpGet("GetAllAgents")]
        public IActionResult GetAllAgents()
        {
            try
            {
                var agents = _agentService.GetAllAgents();
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpPost("AddAgents")]
        public IActionResult AddAgent([FromBody] CreateAgentDTO agentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _agentService.AddAgent(agentDto);
                return Ok(new { message = "Agent added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpGet("GetAgentbyID/{agentId}")]
        public IActionResult GetAgentById(Guid agentId)
        {
            try
            {
                var agent = _agentService.GetAgentById(agentId);
                if (agent == null)
                {
                    return NotFound(new { message = "Agent not found." });
                }
                return Ok(agent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpPut("UpdateAgent/{agentId}")]
        public IActionResult UpdateAgent(Guid agentId, [FromBody] UpdateAgentDTO agentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _agentService.UpdateAgent(agentDto);
                return Ok(new { message = "Agent updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpDelete("DeleteAgent/{agentId}")]
        public IActionResult DeleteAgent(Guid agentId)
        {
            try
            {
                _agentService.DeleteAgent(agentId);
                return Ok(new { message = "Agent deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpPost("Claim-{claimId}/approve")]
        public IActionResult ApproveClaim(Guid claimId)
        {
            try
            {
                _agentService.ApproveClaim(claimId);
                return Ok(new { message = "Claim approved." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpPost("Claim-{claimId}/reject")]
        public IActionResult RejectClaim(Guid claimId)
        {
            try
            {
                _agentService.RejectClaim(claimId);
                return Ok(new { message = "Claim rejected." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
