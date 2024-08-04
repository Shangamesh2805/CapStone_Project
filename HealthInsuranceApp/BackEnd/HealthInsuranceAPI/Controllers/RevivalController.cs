using HealthInsuranceAPI.Models.DTOs.Revival;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevivalController : ControllerBase
    {
        private readonly IRevivalService _revivalService;

        public RevivalController(IRevivalService revivalService)
        {
            _revivalService = revivalService;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddRevival([FromBody] CreateRevivalDTO revivalDto)
        {
            try
            {
                _revivalService.AddRevival(revivalDto);
                return Ok(new { message = "Revival request added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{revivalId}")]
        public IActionResult GetRevivalById(Guid revivalId)
        {
            try
            {
                var revival = _revivalService.GetRevivalById(revivalId);
                if (revival == null)
                {
                    return NotFound(new { message = "Revival not found" });
                }
                return Ok(revival);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpPut("approve/{revivalId}")]
        public IActionResult ApproveRevival(Guid revivalId)
        {
            try
            {
                _revivalService.ApproveRevival(revivalId);
                return Ok(new { message = "Revival approved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Agent")]
        [HttpPut("reject/{revivalId}")]
        public IActionResult RejectRevival(Guid revivalId)
        {
            try
            {
                _revivalService.RejectRevival(revivalId);
                return Ok(new { message = "Revival rejected successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllRevivals()
        {
            try
            {
                var revivals = _revivalService.GetAllRevivals();
                return Ok(revivals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
