using Journey.Test.Support.Model;

namespace Journey.Test.Support.ObjectMothers
{
    public class RiskMother
    {
        public string ClientReference { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
        public VehicleUsage VehicleUsage { get; set; }
        public PersonDetails PersonDetails{get; set; }
        public Claim Claim { get; set; }
        public Conviction Conviction { get; set; }
        public DrivingHistory DrivingHistory { get; set; }
        public PolicyDetails PolicyDetails { get; set; }
        public ContactDetails ContactDetails { get; set; }

        public RiskMother()
        {
            ClientReference = "XYZ";
            VehicleDetails = new VehicleDetailsMother().Build();
            VehicleUsage = new VehicleUsageMother().Build();
            PersonDetails = new PersonalDetailsMother().Build();
            Claim = new ClaimMother().Build();
            Conviction = new ConvictionMother().Build();
            DrivingHistory = new DrivingHistoryMother().Build();
            PolicyDetails = new PolicyDetailsMother().Build();
            ContactDetails = new ContactDetailsMother().Build();
        }


        public Risk Build()
        {
            var risk = new Risk { ClientReference = ClientReference, VehicleDetails = VehicleDetails, VehicleUsage = VehicleUsage, PersonalDetails = PersonDetails, Claim = Claim, Conviction = Conviction, DrivingHistory = DrivingHistory, PolicyDetails = PolicyDetails, ContactDetails = ContactDetails };
            return risk;
        }

       
    }
}