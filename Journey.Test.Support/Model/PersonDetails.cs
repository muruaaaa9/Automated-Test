using System;

namespace Journey.Test.Support.Model
{
    public class PersonDetails
    {
        private static readonly Address HardCodedAddress = new Address { Id = 1, AddressLine1 = "Flat 56", AddressLine2 = "Forum House", AddressLine3 = "Empire Way", AddressLine4 = "Wembley", PostCode = "HA9 0AB", DpsCode = "5E" };
        public const string SelfEmployed = "S";
        public const string Employed = "E";
        public const string UnEmployed = "U";
        public const string CurrentlyNotWorking = "currentlynotworking";
        public const string FullOrPartTime = "F";

        public int Id { get; set; }
        public string TitleCode { get; set; }
        public string TitleDescription { get; set; }
        public string StudentTypeCode { get; set; }
        public string StudentTypeCodeDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatusCode { get; set; }
        public string MaritalStatusDescription { get; set; }
        public bool OwnsHome { get; set; }
        public bool HasChildrenUnderSixteen { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmploymentStatusDescription { get; set; }
        public bool Ghost { get; set; }
        public ReferenceDataItem OccupationTitle { get; set; }
        public string OccupationTitleDescription { get; set; }
        public ReferenceDataItem BusinessType { get; set; }
        public string BusinessTypeDescription { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string WhyNotWorking { get; set; }
        public bool ResidentSinceBirth { get; set; }
        public DateTime? ResidentSinceDate { get; set; }

        public Address HomeAddress
        {
            get { return HardCodedAddress; }
        }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1} {2}", TitleGender.GetDisplayTitle(TitleCode), FirstName, LastName).ToTitleCase().Trim();
            }
        }

        public bool IsCurrentlyNotWorking()
        {
            return EmploymentStatus != SelfEmployed && EmploymentStatus != Employed;
            
        }
    }
}