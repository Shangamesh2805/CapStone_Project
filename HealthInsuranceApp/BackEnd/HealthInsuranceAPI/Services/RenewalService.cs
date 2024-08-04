using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models.DTOs.Renewal;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthInsuranceAPI.Models.DTOs.Payments;

public class RenewalService : IRenewalService
{
    private readonly IRenewalRepository _renewalRepository;
    private readonly ICustomerPolicyRepository _customerPolicyRepository;
    private readonly IRevivalRepository _revivalRepository;

    public RenewalService(
        IRenewalRepository renewalRepository,
        ICustomerPolicyRepository customerPolicyRepository,
        IRevivalRepository revivalRepository)
    {
        _renewalRepository = renewalRepository;
        _customerPolicyRepository = customerPolicyRepository;
        _revivalRepository = revivalRepository;
    }

    public RenewalResponseDTO AddRenewal(Guid customerPolicyId)
    {
        var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(customerPolicyId);
        if (customerPolicy == null)
            throw new Exception("Customer policy not found");

        if (customerPolicy.InsurancePolicy == null)
            throw new Exception("Associated insurance policy not found");

        var today = DateTime.Now;
        var expiryDate = customerPolicy.ExpiryDate;
        var isWithinGracePeriod = today <= expiryDate.AddDays(30);

        var revivals = _revivalRepository.GetAllRevivals().OfType<Revival>();
        if (revivals == null)
            throw new Exception("Revivals list is null");

        var hasRevival = revivals.Any(r => r.CustomerPolicyID == customerPolicyId && r.IsApproved);

        if (!isWithinGracePeriod && !hasRevival)
        {
            throw new Exception("Renewal not possible. Please initiate the revival process.");
        }

        if (hasRevival)
        {
            expiryDate = today;
            customerPolicy.ExpiryDate = expiryDate.AddMonths(1); // Extend policy expiration date due to successful revival
            _customerPolicyRepository.UpdateCustomerPolicy(customerPolicy);
        }

        var renewalAmount = CalculateRenewalAmount(customerPolicy);
        var renewal = new Renewal
        {
            RenewalID = Guid.NewGuid(),
            CustomerPolicyID = customerPolicyId,
            RenewalDate = today,
            RenewalAmount = renewalAmount,
            DiscountApplied = CheckDiscountEligibility(customerPolicy),
            IsRenewed = false // Renewal is completed after payment
        };

        _renewalRepository.AddRenewal(renewal);

        return new RenewalResponseDTO
        {
            RenewalID = renewal.RenewalID,
            RenewalAmount = renewalAmount
        };
    }


    private decimal CalculateRenewalAmount(CustomerPolicy customerPolicy)
    {
        var renewalAmount = customerPolicy.InsurancePolicy.PremiumAmount * 0.1M;

        if (CheckDiscountEligibility(customerPolicy))
        {
            renewalAmount *= 0.9M;
        }

        return renewalAmount;
    }


    private bool CheckDiscountEligibility(CustomerPolicy customerPolicy)
    {
        var lastYear = DateTime.Now.Year - 1;
        return customerPolicy.Claims.All(c => c.ClaimDate.Year != lastYear);
    }

    public RenewalDTO GetRenewalById(Guid renewalId)
    {
        var renewal = _renewalRepository.GetRenewal(renewalId);
        if (renewal == null)
            throw new Exception("Renewal not found");

        var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(renewal.CustomerPolicyID);
        if (customerPolicy == null)
            throw new Exception("Customer policy not found");

        var renewalAmount = CalculateRenewalAmount(customerPolicy);

        return new RenewalDTO
        {
            RenewalID = renewal.RenewalID,
            CustomerPolicyID = renewal.CustomerPolicyID,
            RenewalDate = renewal.RenewalDate,
            RenewalAmount = renewalAmount,
            DiscountApplied = CheckDiscountEligibility(customerPolicy),
            IsRenewed = renewal.IsRenewed
        };
    }

    public void UpdateRenewal(UpdateRenewalDTO renewalDto)
    {
        var renewal = _renewalRepository.GetRenewal(renewalDto.RenewalID);
        if (renewal == null)
            throw new Exception("Renewal not found");

        renewal.RenewalDate = renewalDto.RenewalDate;
        renewal.RenewalAmount = renewalDto.RenewalAmount;
        renewal.DiscountApplied = renewalDto.DiscountApplied;
        renewal.IsRenewed = renewalDto.IsRenewed;

        _renewalRepository.UpdateRenewal(renewal);
    }

    public void DeleteRenewal(Guid renewalId)
    {
        _renewalRepository.DeleteRenewal(renewalId);
    }

    public void UpdateCustomerPolicyDates(Guid customerPolicyId)
    {
        var customerPolicy = _customerPolicyRepository.GetCustomerPolicy(customerPolicyId);
        if (customerPolicy == null)
            throw new Exception("Customer policy not found");

        var today = DateTime.UtcNow;
        customerPolicy.StartDate = today;
        customerPolicy.ExpiryDate = today.AddYears(1);

        _customerPolicyRepository.UpdateCustomerPolicy(customerPolicy);
    }


    public IEnumerable<RenewalDTO> GetAllRenewals()
    {
        return _renewalRepository.GetAllRenewals().Select(r => new RenewalDTO
        {
            RenewalID = r.RenewalID,
            CustomerPolicyID = r.CustomerPolicyID,
            RenewalDate = r.RenewalDate,
            RenewalAmount = r.RenewalAmount,
            DiscountApplied = r.DiscountApplied,
            IsRenewed = r.IsRenewed
        }).ToList();
    }

    
}