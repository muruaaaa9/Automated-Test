using System;

namespace Journey.Test.Support.Model
{
    public class PolicyDetails
    {
        public string MainDriverDescription { get; set; }
        public string CoverTypeDescription { get; set; }
        public string PaymentMethodDescription { get; set; }
        public DateTime CommencementDate { get; set; }
        public string NcdPeriodDescription { get; set; }
        public string NcdSourceId { get; set; }
        public string NcdSourceDescription { get; set; }
        public string VoluntaryExcess { get; set; }
        public string NamedDriverExperience { get; set; }
        public string NamedDriverExperienceYears { get; set; }

        public bool MainDriverRegisteredKeeperAndLegalOwner { get; set; }
        public string RegisteredKeeper { get; set; }
        public string RegisteredKeeperCompanyName { get; set; }
        public string RegisteredKeeperRelationship { get; set; }
        public string RegisteredKeeperOtherTitle { get; set; }
        public string RegisteredKeeperOtherFirstname { get; set; }
        public string RegisteredKeeperOtherSurname { get; set; }
        public DateTime RegisteredKeeperOtherDob { get; set; }
        public bool RegisteredKeeperOtherLiveAtSameAddress { get; set; }

        public string LegalOwner { get; set; }
        public string LegalOwnerCompanyName { get; set; }
        public string LegalOwnerRelationship { get; set; }
        public string LegalOwnerOtherTitle { get; set; }
        public string LegalOwnerOtherFirstname { get; set; }
        public string LegalOwnerOtherSurname { get; set; }
        public DateTime LegalOwnerOtherDob { get; set; }
        public bool LegalOwnerOtherLiveAtSameAddress { get; set; }
    }
}