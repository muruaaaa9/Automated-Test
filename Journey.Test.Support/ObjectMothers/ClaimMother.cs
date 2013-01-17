using System;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class ClaimMother
    {
        public bool HasMotorClaims { get; set; }
        public int HowManyClaims { get; set; }
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

        public ClaimMother()
        {
        }

        public Claim Build()
        {
            ClaimTypeDescription = "Accident";
            TheftTypeDescription = "Theft - Accessories";
            ClaimMadeForDescription = "Fire Damage";
            WhoWasDriving = Claim.MePolicyHolder;
            WhoWasAtFaultDescription = "Both Parties";
            WasThereAnyInjuries = false;
            TypeOfDamageDescription = "No Damage";
            ClaimMadeUnderDriverInsurance = false;
            NoClaimDiscountAffected = false;
            ClaimDate = new DateTime(2012, 10, 1);
            return new Claim()
                       {
                           ClaimTypeDescription = ClaimTypeDescription,
                           TheftTypeDescription = TheftTypeDescription,
                           ClaimMadeForDescription = ClaimMadeForDescription,
                           WhoWasDriving = WhoWasDriving,
                           WhoWasAtFaultDescription = WhoWasAtFaultDescription,
                           WasThereAnyInjuries = WasThereAnyInjuries,
                           TypeOfDamageDescription = TypeOfDamageDescription,
                           ClaimMadeUnderDriverInsurance = ClaimMadeUnderDriverInsurance,
                           NoClaimDiscountAffected = NoClaimDiscountAffected,
                       };
        }

        public ClaimCollection BuildFromCSV(DataRecord data)
        {
            HowManyClaims = Convert.ToInt32(data["PROPOSERHOWMANYCLAIMS"]);
            var claims = GetProposerClaims(data);
            return claims;
        }

        private Claim BuildClaim( string claimTypeDescription, string whoWasDriving, string whoWasAtFaultDescription, string wasThereAnyInjuries, string claimDate, string typeOfDamageDescription, string claimMadeUnderDriverInsurance, string noClaimDiscountAffected, string theftTypeDescription, string otherTypeDescription)
        {
            ClaimTypeDescription = claimTypeDescription;
            ClaimDate = Extension.GetDateTime(claimDate);
            TypeOfDamageDescription = typeOfDamageDescription;
            ClaimMadeUnderDriverInsurance =  Convert.ToBoolean(claimMadeUnderDriverInsurance);
            GetRelevantQuestionsFilledUpBasedOnClaimType(whoWasDriving, whoWasAtFaultDescription, wasThereAnyInjuries, noClaimDiscountAffected, theftTypeDescription, otherTypeDescription);
            return new Claim()
            {
                ClaimTypeDescription = ClaimTypeDescription,
                WhoWasDriving = WhoWasDriving,
                WhoWasAtFaultDescription = WhoWasAtFaultDescription,
                WasThereAnyInjuries = WasThereAnyInjuries,
                ClaimDate = ClaimDate,
                TypeOfDamageDescription = TypeOfDamageDescription,
                ClaimMadeUnderDriverInsurance = ClaimMadeUnderDriverInsurance,
                NoClaimDiscountAffected = NoClaimDiscountAffected,
                TheftTypeDescription = TheftTypeDescription,
                ClaimMadeForDescription = ClaimMadeForDescription,
            };
        }

        private void GetRelevantQuestionsFilledUpBasedOnClaimType(string whoWasDriving, string whoWasAtFaultDescription,
                                                               string wasThereAnyInjuries, string noClaimDiscountAffected,
                                                               string theftTypeDescription, string otherTypeDescription)
        {
            if (ClaimTypeDescription.Trim().ToUpper().Equals(Constants.AccidentClaim))
            {
                WhoWasDriving = whoWasDriving;
                WhoWasAtFaultDescription = whoWasAtFaultDescription;
                WasThereAnyInjuries = Convert.ToBoolean(wasThereAnyInjuries);
                NoClaimDiscountAffected = Convert.ToBoolean(noClaimDiscountAffected);
            }
            if (ClaimTypeDescription.Trim().ToUpper().Equals(Constants.TheftClaim))
            {
                TheftTypeDescription = theftTypeDescription; 
            }
            if (ClaimTypeDescription.Trim().ToUpper().Equals(Constants.OtherClaim))
            {
                ClaimMadeForDescription = otherTypeDescription;
            }
        }

        private ClaimCollection GetProposerClaims(DataRecord data)
        {
            ClaimCollection claims = new ClaimCollection();
            for (int currentProposerClaim = 1; currentProposerClaim <= HowManyClaims; currentProposerClaim ++ )
            {
                claims.Add(BuildClaim(
                    data["PROPOSERCLAIM" + currentProposerClaim + "CLAIMEDFOR"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "WHOWASDRIVING"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "WHOWASATFAULT"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "ANYINJURIES"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "CLAIMDATE"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "TYPEOFDAMAGE"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "CLAIMMADEUNDERINSURANCE"], 
                    data["PROPOSERCLAIM" + currentProposerClaim + "NCDAFFECTED"],
                    data["PROPOSERCLAIM" + currentProposerClaim + "THEFTTYPEDESCRIPTION"],
                    data["PROPOSERCLAIM" + currentProposerClaim + "OTHERTYPEDESCRIPTION"]));   
            }
          return claims;
        }

        public ClaimCollection GetAdditionalDriverClaims(DataRecord data, string currentDriver)
        {
            ClaimCollection claims = new ClaimCollection();
            int additionalDriverHowManyClaims = Convert.ToInt32( data["ADD"+ currentDriver + "HOWMANYCLAIMS"]);
            for (int currentClaim = 1; currentClaim <= additionalDriverHowManyClaims; currentClaim ++ )
            {
                claims.Add(BuildClaim(
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "CLAIMEDFOR"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "WHOWASDRIVING"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "WHOWASATFAULT"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "ANYINJURIES"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "CLAIMDATE"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "TYPEOFDAMAGE"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "CLAIMMADEUNDERINSURANCE"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "NCDAFFECTED"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "THEFTTYPEDESCRIPTION"],
                        data["ADD" + currentDriver + "CLAIM" + currentClaim.ToString() + "OTHERTYPEDESCRIPTION"]));
            }

            return claims;
        }

         
    }

}