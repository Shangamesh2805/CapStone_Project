
using System.Linq;
using HealthInsuranceAPI.Exceptions;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;

namespace HealthInsuranceApp.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public AgentRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Agent GetAgent(Guid agentId)
        {
            var agent = _context.Agents.Find(agentId);
            if (agent == null)
            {
                throw new NotFoundException($"Agent with ID {agentId} not found.");
            }
            return agent;
        }

        public void AddAgent(Agent agent)
        {
            if (_context.Agents.Any(a => a.UserID == agent.UserID))
            {
                throw new AlreadyExistsException($"Agent with UserID {agent.UserID} already exists.");
            }
            _context.Agents.Add(agent);
            _context.SaveChanges();
        }

        public void UpdateAgent(Agent agent)
        {
            var existingAgent = GetAgent(agent.AgentID);
            if (existingAgent == null)
            {
                throw new NotFoundException($"Agent with ID {agent.AgentID} not found.");
            }
            _context.Agents.Update(agent);
            _context.SaveChanges();
        }

        public void DeleteAgent(Guid agentId)
        {
            try
            {
                var agent = GetAgent(agentId);
                if (agent == null)
                {
                    throw new NotFoundException($"Agent with ID {agentId} not found.");
                }
                _context.Agents.Remove(agent);
                _context.SaveChanges();
            }
            catch(Exception) 
            {
                throw new UnknownErrorException();
            }
        }

        public IEnumerable<Agent> GetAllAgents()
        {
            return _context.Agents.ToList();
        }
    }
}
