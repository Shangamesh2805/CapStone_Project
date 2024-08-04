namespace HealthInsuranceAPI.Exceptions
{


    public class ClaimInvalidException:Exception
    {
        private string msg;
        public ClaimInvalidException()
        {
            msg = "The Claim is not Valid ";
        }
        public override string Message => msg;

    }
}
