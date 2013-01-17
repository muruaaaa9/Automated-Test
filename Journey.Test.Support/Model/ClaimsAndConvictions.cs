namespace Journey.Test.Support.Model
{
    public class ClaimsAndConvictions
    {
        public ClaimsAndConvictions(bool haveMotorClaim, bool haveMotorConviction, bool haveNonMotorConviction)
        {
            HasMotorClaims = haveMotorClaim;
            HasMotorConvictions = haveMotorConviction;
            HasNonMotorConvictions = haveNonMotorConviction;
        }

        public ClaimsAndConvictions()
        {
        }

        public bool HasMotorClaims { get; set; }
        public bool HasMotorConvictions { get; set; }
        public bool HasNonMotorConvictions { get; set; }
    }
}