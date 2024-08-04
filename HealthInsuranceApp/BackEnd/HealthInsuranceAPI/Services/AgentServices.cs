using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Agents;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInsuranceAPI.Services
{
    public class AgentService : IAgentServices
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IClaimRepository _claimRepository;

        public AgentService(IAgentRepository agentRepository, IClaimRepository claimRepository)
        {
            _agentRepository = agentRepository;
            _claimRepository = claimRepository;
        }

        public void AddAgent(CreateAgentDTO agentDto)
        {
            var agent = new Agent
            {
                AgentID = Guid.NewGuid(),
                UserID = agentDto.UserID,
                Name = agentDto.Name,
                ContactNumber = agentDto.ContactNumber
            };

            _agentRepository.AddAgent(agent);
        }

        public AgentDTO GetAgentById(Guid agentId)
        {
            var agent = _agentRepository.GetAgent(agentId);
            if (agent == null) throw new Exception("Agent not found.");

            return new AgentDTO
            {
                AgentID = agent.AgentID,
                UserID = agent.UserID,
                Name = agent.Name,
                ContactNumber = agent.ContactNumber
            };
        }

        public void UpdateAgent(UpdateAgentDTO agentDto)
        {
            var agent = _agentRepository.GetAgent(agentDto.AgentID);
            if (agent == null) throw new Exception("Agent not found.");

            agent.Name = agentDto.Name;
            agent.ContactNumber = agentDto.ContactNumber;

            _agentRepository.UpdateAgent(agent);
        }

        public void DeleteAgent(Guid agentId)
        {
            _agentRepository.DeleteAgent(agentId);
        }

        public IEnumerable<AgentDTO> GetAllAgents()
        {
            var agents = _agentRepository.GetAllAgents();
            return agents.Select(agent => new AgentDTO
            {
                AgentID = agent.AgentID,
                UserID = agent.UserID,
                Name = agent.Name,
                ContactNumber = agent.ContactNumber
            }).ToList();
        }

        public void ApproveClaim(Guid claimId)
        {
            var claim = _claimRepository.GetClaim(claimId);
            if (claim == null) throw new Exception("Claim not found.");

            claim.ClaimStatus = ClaimStatus.Approved;
            _claimRepository.UpdateClaimStatus(claimId, ClaimStatus.Approved);
        }

        public void RejectClaim(Guid claimId)
        {
            var claim = _claimRepository.GetClaim(claimId);
            if (claim == null) throw new Exception("Claim not found.");

            claim.ClaimStatus = ClaimStatus.Rejected;
            _claimRepository.UpdateClaimStatus(claimId, ClaimStatus.Rejected);
        }
    }
}
