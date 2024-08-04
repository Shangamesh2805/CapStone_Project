using System.Runtime.Serialization;

namespace HealthInsuranceAPI.Exceptions
{
    [Serializable]
    public class UnknownErrorException : Exception
    {
        private string msg;

        public UnknownErrorException()
        {
            msg = "An Error Occured while performing ";
        }
        public override string Message => msg;
    }
}