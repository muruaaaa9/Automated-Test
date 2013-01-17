using Journey.Test.Support.Model;

namespace Journey.Test.Support.ObjectMothers
{
    public class AddressMother
    {
        public int Id { get; set; }

        public AddressMother()
        {
            Id = 1;
        }

        public Address Build()
        {
            return new Address { Id = Id, AddressLine1 = "Flat 56", AddressLine2 = "Forum House", AddressLine3 = "Empire Way", AddressLine4 = "Wembley", PostCode = "HA9 0AB", DpsCode = "5E" };
        }

    }
}