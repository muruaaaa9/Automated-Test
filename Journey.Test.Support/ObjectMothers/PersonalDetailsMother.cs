using System;
using System.Configuration;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class PersonalDetailsMother
    {

        public string TitleDescription { get; set; }

        public string StudentTypeCodeDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatusDescription { get; set; }
        public bool OwnsHome { get; set; }
        public bool HasChildrenUnderSixteen { get; set; }
        public string EmploymentStatusDescription { get; set; }
        public string OccupationTitleDescription { get; set; }
        public string BusinessTypeDescription { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string WhyNotWorking { get; set; }
        public bool ResidentSinceBirth { get; set; }
        public DateTime? ResidentSinceDate { get; set; }

        public PersonalDetailsMother()
        {
        }

        public PersonDetails Build()
        {
            LastName = "QUOTE";
            DateOfBirth = new DateTime(1960, 01, 01);
            FirstName = "CAR";
            TitleDescription = "Mr";
            StudentTypeCodeDescription = "Student";
            OccupationTitleDescription = "Nurse";
            BusinessTypeDescription = "Hospital";
            HouseNumber = "31";
            PostCode = "PE6 8NW";

            bool _qsTestEnabled = ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase);
            if (_qsTestEnabled)
            {
                EmploymentStatusDescription = "Full/Part Time Education";
            }
            else
            {
                EmploymentStatusDescription = "Employed";
            }
            return new PersonDetails
                       {
                           FirstName = FirstName,
                           LastName = LastName,
                           DateOfBirth = DateOfBirth,
                           HasChildrenUnderSixteen = false,
                           ResidentSinceDate = new DateTime(1985, 01, 01),
                           ResidentSinceBirth = true,
                           EmploymentStatusDescription = EmploymentStatusDescription,
                           MaritalStatusCode = "M",
                           MaritalStatusDescription = "Married",
                           OwnsHome = false,
                           TitleDescription = TitleDescription,
                           StudentTypeCodeDescription = StudentTypeCodeDescription,
                           OccupationTitleDescription = OccupationTitleDescription,
                           BusinessTypeDescription = BusinessTypeDescription,
                           HouseNumber = HouseNumber,
                           PostCode = PostCode
                       };
        }

        public PersonDetails BuildFromCSV(DataRecord data)
        {
                TitleDescription = data["TITLE"];
                FirstName = data["FIRSTNAME"];
                LastName = data["SURNAME"];
                DateOfBirth = Extension.GetDateTime(data["DOB"]);
                MaritalStatusDescription = data["MARITALSTATUS"];
                OwnsHome = Convert.ToBoolean(data["HOMEOWNER"]);
                HasChildrenUnderSixteen = Convert.ToBoolean(data["CHILDRENUNDER16"]);
                ResidentSinceBirth = Convert.ToBoolean(data["RESIDENTSINCEBIRTH"]);
                if (ResidentSinceBirth != true)
                {
                    ResidentSinceDate = Extension.GetDateTime(data["RESIDENTSINCEDATE"]);
                }
                BusinessTypeDescription = data["BUSINESSTYPE"];
                OccupationTitleDescription = data["OCCUPATION"];
                HouseNumber = data["HOUSENUMBER"];
                PostCode = data["POSTCODE"];
                if (ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase))
                {
                    EmploymentStatusDescription = data["QS_EMPLOYMENTSTATUS"];            
                    if (EmploymentStatusDescription.Trim().ToUpper().Equals(Constants.CurrentlyNotWorking))
                    {
                        WhyNotWorking = data["QS_WHYNOTWORKING"];
                    }
                    else if(EmploymentStatusDescription.Trim().ToUpper().Equals(Constants.FullOrPartTimeEducation))
                    {
                        StudentTypeCodeDescription = data["STUDENTTYPE"];
                    }
                }
                else
                {
                    EmploymentStatusDescription = data["EMPLOYMENTSTATUS"];
                    if (EmploymentStatusDescription.Trim().ToUpper().Equals(Constants.CurrentlyNotWorking))
                    {
                        WhyNotWorking = data["WHYNOTWORKING"];
                    }
                }

            return new PersonDetails
            {
                TitleDescription = TitleDescription,
                FirstName =FirstName,
                LastName =LastName,
                DateOfBirth = DateOfBirth,
                MaritalStatusDescription =MaritalStatusDescription,
                OwnsHome =OwnsHome,
                HasChildrenUnderSixteen = HasChildrenUnderSixteen,
                ResidentSinceDate = ResidentSinceDate,
                ResidentSinceBirth = ResidentSinceBirth,
                EmploymentStatusDescription = EmploymentStatusDescription,
                BusinessTypeDescription = BusinessTypeDescription,
                OccupationTitleDescription = OccupationTitleDescription,
                StudentTypeCodeDescription =StudentTypeCodeDescription,
                HouseNumber = HouseNumber,
                PostCode = PostCode,
                WhyNotWorking = WhyNotWorking,
            };
        }
    }
}