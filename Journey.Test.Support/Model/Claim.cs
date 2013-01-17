using System;

namespace Journey.Test.Support.Model
{
    public class Claim
    {
        public static string MePolicyHolder = "Me (Policyholder)";
        public const string Accident = "A";
        public bool HasMotorClaims { get; set; }
        public string ClaimTypeDescription { get; set; }
        public string TheftTypeDescription { get; set; }
        public string ClaimMadeForDescription { get; set; }
        public string WhoWasDriving { get; set; }
        public string WhoWasAtFaultDescription { get; set; }
        public bool WasThereAnyInjuries { get; set; }
        public string TypeOfDamageDescription { get; set; }
        public bool ClaimMadeUnderDriverInsurance { get; set; }
        public bool NoClaimDiscountAffected { get; set; }
        public DateTime? ClaimDate { get; set; }
    }
}