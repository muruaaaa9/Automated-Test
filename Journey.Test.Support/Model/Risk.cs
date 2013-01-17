
namespace Journey.Test.Support.Model
{
    public class Risk
    {

        public string ClientReference { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
        public VehicleUsage VehicleUsage { get; set; }
        public PersonDetails PersonalDetails { get; set; }
        public Claim Claim { get; set; }
        public Conviction Conviction { get; set; }
        public DrivingHistory DrivingHistory { get; set; }
        public PolicyDetails PolicyDetails { get; set; }
        public ContactDetails ContactDetails { get; set; }

        //public int ProposerId
        //{
        //    get { return PersonalDetails.Id; }
        //}



        //public Dictionary<string, string> ListOfNameOfAllPeopleOnTheRisk()
        //{
        //    var people = new Dictionary<string, string>();
        //    people.Add(PersonalDetails.Id.ToString(), PersonalDetails.FullName);
        //    return people;
        //}
    }
}