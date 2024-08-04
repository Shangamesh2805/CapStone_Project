using HealthInsuranceAPI.Models.DTOs.Payment;
using HealthInsuranceAPI.Models.DTOs.Payments;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("renewal")]
        public IActionResult ProcessRenewalPayment(Guid renewalId, [FromBody] RenewalPaymentDTO paymentDto)
        {
            try
            {
                _paymentService.ProcessRenewalPayment(renewalId, paymentDto);
                return Ok(new { message = "Renewal payment processed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("new-application/{customerPolicyId}")]
        public IActionResult ProcessNewApplicationPayment(Guid customerPolicyId, [FromBody] PaymentDTO paymentDto)
        {
            try
            {
                _paymentService.ProcessNewApplicationPayment(customerPolicyId, paymentDto);
                return Ok(new { message = "New application payment processed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
