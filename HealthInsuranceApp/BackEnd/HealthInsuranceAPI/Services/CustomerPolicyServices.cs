using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Claims;
using HealthInsuranceAPI.Models.DTOs.CustomerPolicy;
using HealthInsuranceAPI.Models.DTOs.Payments;
using HealthInsuranceAPI.Models.DTOs.Renewal;
using HealthInsuranceAPI.Models.DTOs.Revival;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInsuranceAPI.Services
{

    public class CustomerPolicyService : ICustomerPolicyService
    {
        private readonly ICustomerPolicyRepository _customerPolicyRepository;
        private readonly IClaimRepository _claimRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRenewalRepository _renewalRepository;
        private readonly IRevivalRepository _revivalRepository;
        private readonly IInsurancePolicyRepository _policyRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerPolicyService(
        ICustomerPolicyRepository customerPolicyRepository,
        IClaimRepository claimRepository,
        IPaymentRepository paymentRepository,
        IRenewalRepository renewalRepository,
        IRevivalRepository revivalRepository,
        IInsurancePolicyRepository policyRepository,
        ICustomerRepository customerRepository)
        {
            _customerPolicyRepository = customerPolicyRepository;
            _claimRepository = claimRepository;
            _paymentRepository = paymentRepository;
            _renewalRepository = renewalRepository;
            _revivalRepository = revivalRepository;
            _policyRepository = policyRepository;
            _customerRepository = customerRepository;
        }
       
        
    
    
    public CustomerPolicyDTO GetCustomerPolicyById(Guid customerPolicyId)
        {
            var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(customerPolicyId);
            if (customerPolicy == null)
                throw new Exception("Customer policy not found");

            return new CustomerPolicyDTO
            {
                CustomerPolicyID = customerPolicy.CustomerPolicyID,
                CustomerID = customerPolicy.CustomerID,
                PolicyID = customerPolicy.PolicyID,
                Status = customerPolicy.Status,
                DiscountEligibility = customerPolicy.DiscountEligibility,
                Claims = customerPolicy.Claims.Select(c => new ClaimDTO
                {
                    ClaimID = c.ClaimID,
                    CustomerPolicyID = c.CustomerPolicyID,
                    ClaimAmount = c.ClaimAmount,
                    ClaimDate = c.ClaimDate,
                    ClaimStatus = c.ClaimStatus.ToString(),
                    Reason = c.Reason
                }).ToList(),
                Payments = customerPolicy.Payments.Select(p => new PaymentDTO
                {
                    PaymentID = p.PaymentID,
                    Amount = p.PaymentAmount,
                    Date = p.PaymentDate,
                    PaymentType = p.PaymentType
                }).ToList(),
                Renewals = customerPolicy.Renewals.Select(r => new RenewalDTO
                {
                    RenewalID = r.RenewalID,
                    RenewalDate = r.RenewalDate,
                    RenewalAmount = r.RenewalAmount,
                    DiscountApplied = r.DiscountApplied
                }).ToList(),
                Revivals = customerPolicy.Revivals.Select(r => new RevivalDTO
                {
                    RevivalID = r.RevivalID,
                    RevivalDate = r.RevivalDate,
                    Reason = r.Reason,
                    IsApproved = r.IsApproved
                }).ToList()
            };
        }
        public void CreateCustomerPolicy(CreateCustomerPolicyDTO customerPolicyDto, Guid userId)
        {
            
            var customer = _customerRepository.GetCustomerByUserId(userId);
            if (customer == null)
                throw new Exception("Customer not found.");

           
            var policy = _policyRepository.GetPolicyById(customerPolicyDto.PolicyID);
            if (policy == null)
                throw new Exception("Policy not found.");

            
            var startDate = policy.StartDate;
            var expiryDate = policy.EndDate;


            var customerPolicy = new CustomerPolicy
            {
                CustomerPolicyID = Guid.NewGuid(),
                CustomerID = customer.CustomerID,
                PolicyID = customerPolicyDto.PolicyID,
                Status = customerPolicyDto.Status,
                DiscountEligibility = false,  
                StartDate = startDate,
                ExpiryDate = expiryDate
            };

            _customerPolicyRepository.AddCustomerPolicy(customerPolicy);
        }



        public void UpdateCustomerPolicy(UpdateCustomerPolicyDTO customerPolicyDto)
        {
            var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(customerPolicyDto.CustomerPolicyID);
            if (customerPolicy == null)
                throw new Exception("Customer policy not found");

            customerPolicy.Status = customerPolicyDto.Status;

            _customerPolicyRepository.UpdateCustomerPolicy(customerPolicy);
        }

        public void DeleteCustomerPolicy(Guid customerPolicyId)
        {
            _customerPolicyRepository.DeleteCustomerPolicy(customerPolicyId);
        }

        public void CheckRenewalEligibility(Guid customerPolicyId)
        {
            var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(customerPolicyId);
            if (customerPolicy == null)
                throw new Exception("Customer policy not found");

            var latestRenewal = customerPolicy.Renewals.OrderByDescending(r => r.RenewalDate).FirstOrDefault();
            var latestRevival = customerPolicy.Revivals.OrderByDescending(r => r.RevivalDate).FirstOrDefault();

            if (latestRenewal != null && latestRenewal.RenewalDate.AddDays(30) >= DateTime.Now)
            {
                throw new Exception("Policy is within the renewal grace period.");
            }

            if (latestRevival != null && latestRevival.IsApproved && latestRevival.RevivalDate.AddDays(30) >= DateTime.Now)
            {
                customerPolicy.Status = PolicyStatus.Active;
                _customerPolicyRepository.UpdateCustomerPolicy(customerPolicy);
                return;
            }

            throw new Exception("Renewal not possible. Please initiate the revival process.");
        }

        public IEnumerable<CustomerPolicyDTO> GetAllCustomerPolicies()
        {
            var customerPolicies = _customerPolicyRepository.GetAllCustomerPolicies();

            if (customerPolicies == null)
            {
                throw new Exception("No customer policies found.");
            }

            return customerPolicies.Select(cp => new CustomerPolicyDTO
            {
                CustomerPolicyID = cp.CustomerPolicyID,
                CustomerID = cp.CustomerID,
                PolicyID = cp.PolicyID,
                Status = cp.Status,
                DiscountEligibility = cp.DiscountEligibility,
                Claims = cp.Claims?.Select(c => new ClaimDTO
                {
                    ClaimID = c.ClaimID,
                    CustomerPolicyID = c.CustomerPolicyID,
                    ClaimAmount = c.ClaimAmount,
                    ClaimDate = c.ClaimDate,
                    ClaimStatus = c.ClaimStatus.ToString(),
                    Reason = c.Reason
                }).ToList() ?? new List<ClaimDTO>(),
                Payments = cp.Payments?.Select(p => new PaymentDTO
                {
                    PaymentID = p.PaymentID,
                    Amount = p.PaymentAmount,
                    Date = p.PaymentDate,
                    PaymentType = p.PaymentType
                }).ToList() ?? new List<PaymentDTO>(),
                Renewals = cp.Renewals?.Select(r => new RenewalDTO
                {
                    RenewalID = r.RenewalID,
                    RenewalDate = r.RenewalDate,
                    RenewalAmount = r.RenewalAmount,
                    DiscountApplied = r.DiscountApplied
                }).ToList() ?? new List<RenewalDTO>(),
                Revivals = cp.Revivals?.Select(r => new RevivalDTO
                {
                    RevivalID = r.RevivalID,
                    RevivalDate = r.RevivalDate,
                    Reason = r.Reason,
                    IsApproved = r.IsApproved
                }).ToList() ?? new List<RevivalDTO>()
            }).ToList();
        }

        public IEnumerable<CustomerPolicyDTO> GetCustomerPolicyByCustomerId(Guid customerId)
        {
             
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null)
                throw new Exception("Customer not found.");

             
            var customerPolicies = _customerPolicyRepository.GetCustomerPoliciesByCustomerId(customerId);

            if (customerPolicies == null || !customerPolicies.Any())
                throw new Exception("No customer policies found for this customer.");

             
            return customerPolicies.Select(cp => new CustomerPolicyDTO
            {
                CustomerPolicyID = cp.CustomerPolicyID,
                CustomerID = cp.CustomerID,
                PolicyID = cp.PolicyID,
                Status = cp.Status,
                DiscountEligibility = cp.DiscountEligibility,
                PolicyName = cp.InsurancePolicy?.PolicyName ?? "Unknown Policy",
                PolicyNumber = cp.InsurancePolicy?.PolicyNumber ?? "Unknown Number",
                PolicyType = cp.InsurancePolicy?.PolicyType ?? "Unknown Type",
                CoverageAmount = cp.InsurancePolicy?.CoverageAmount ?? 0,
                PremiumAmount = cp.InsurancePolicy?.PremiumAmount ?? 0,
                StartDate = cp.StartDate,
                ExpiryDate = cp.ExpiryDate,
                Claims = cp.Claims?.Select(c => new ClaimDTO
                {
                    ClaimID = c.ClaimID,
                    CustomerPolicyID = c.CustomerPolicyID,
                    ClaimAmount = c.ClaimAmount,
                    ClaimDate = c.ClaimDate,
                    ClaimStatus = c.ClaimStatus.ToString(),
                    Reason = c.Reason
                }).ToList() ?? new List<ClaimDTO>(),
                Payments = cp.Payments?.Select(p => new PaymentDTO
                {
                    PaymentID = p.PaymentID,
                    Amount = p.PaymentAmount,
                    Date = p.PaymentDate,
                    PaymentType = p.PaymentType
                }).ToList() ?? new List<PaymentDTO>(),
                Renewals = cp.Renewals?.Select(r => new RenewalDTO
                {
                    RenewalID = r.RenewalID,
                    RenewalDate = r.RenewalDate,
                    RenewalAmount = r.RenewalAmount,
                    DiscountApplied = r.DiscountApplied
                }).ToList() ?? new List<RenewalDTO>(),
                Revivals = cp.Revivals?.Select(r => new RevivalDTO
                {
                    RevivalID = r.RevivalID,
                    RevivalDate = r.RevivalDate,
                    Reason = r.Reason,
                    IsApproved = r.IsApproved
                }).ToList() ?? new List<RevivalDTO>()
            }).ToList();
        }


        public Customer GetCustomerByUserId(Guid userId)
        {
            return _customerRepository.GetCustomerByUserId(userId);
        }

        CustomerPolicyDTO ICustomerPolicyService.GetCustomerPolicyDetails(Guid customerPolicyId)
        {
            var customerPolicy = _customerPolicyRepository.GetCustomerPolicyDetails(customerPolicyId);
            if (customerPolicy == null)
                throw new Exception("Customer policy not found");

            return new CustomerPolicyDTO
            {
                CustomerPolicyID = customerPolicy.CustomerPolicyID,
                CustomerID = customerPolicy.CustomerID,
                PolicyID = customerPolicy.PolicyID,

                DiscountEligibility = customerPolicy.DiscountEligibility,

                StartDate = customerPolicy.StartDate,
                ExpiryDate = customerPolicy.ExpiryDate,
                Claims = customerPolicy.Claims?.Select(c => new ClaimDTO
                {
                    ClaimID = c.ClaimID,
                    CustomerPolicyID = c.CustomerPolicyID,
                    ClaimAmount = c.ClaimAmount,
                    ClaimDate = c.ClaimDate,
                    ClaimStatus = c.ClaimStatus.ToString(),
                    Reason = c.Reason
                }).ToList() ?? new List<ClaimDTO>(),
                Payments = customerPolicy.Payments?.Select(p => new PaymentDTO
                {
                    PaymentID = p.PaymentID,
                    Amount = p.PaymentAmount,
                    Date = p.PaymentDate,
                    PaymentType = p.PaymentType
                }).ToList() ?? new List<PaymentDTO>(),
                Renewals = customerPolicy.Renewals?.Select(r => new RenewalDTO
                {
                    RenewalID = r.RenewalID,
                    RenewalDate = r.RenewalDate,
                    RenewalAmount = r.RenewalAmount,
                    DiscountApplied = r.DiscountApplied
                }).ToList() ?? new List<RenewalDTO>(),
                Revivals = customerPolicy.Revivals?.Select(r => new RevivalDTO
                {
                    RevivalID = r.RevivalID,
                    RevivalDate = r.RevivalDate,
                    Reason = r.Reason,
                    IsApproved = r.IsApproved
                }).ToList() ?? new List<RevivalDTO>()
            };
        }
    
    }
}
