using System.Runtime.Serialization;

namespace HealthInsuranceAPI.Exceptions
{
    [Serializable]
    public class AgentNotFoundException : Exception
    {
        private string msg;
        public AgentNotFoundException()
        {
            msg = "No Agent Found with given AgentID ";
        }
        public override string Message => msg;

    }
}