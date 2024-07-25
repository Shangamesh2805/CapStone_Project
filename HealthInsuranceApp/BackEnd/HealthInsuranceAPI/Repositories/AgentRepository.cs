using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public AgentRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public Agent GetAgent(int agentId)
        {
            if (agentId != null ) 
            {
                return _context.Agents.Find(agentId);
            }
            else
            {
                throw new AgentNotFoundException();
            }
            
        }

        public void AddAgent(Agent agent)
        {
            _context.Agents.Add(agent);
            _context.SaveChanges();
        }

        public void UpdateAgent(Agent agent)
        {
            _context.Agents.Update(agent);
            _context.SaveChanges();
        }

        public void DeleteAgent(int agentId)
        {
            var agent = GetAgent(agentId);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
                _context.SaveChanges();
            }
        }
    }
}
