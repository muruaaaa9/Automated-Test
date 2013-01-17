using System;
using System.Configuration;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;


namespace Journey.Test.Support.ObjectMothers
{
    public class AdditonalDriverMother
    {
        public bool HasAdditionalDriver { get; set; }
        public string RelationshipDescription { get; set; }
        public string AdditionalDriverTitleDescription { get; set; }
        public string AdditionalDriverFirstName { get; set; }
        public string AdditionalDriverLastName { get; set; }
        public DateTime AdditionalDriverDateOfBirth { get; set; }
        public string AdditionalDriverMaritalStatusDescription { get; set; }
        public string AdditionalDriverStudentTypeCodeDescription { get; set; }
        public string AdditionalDriverEmploymentStatusDescription { get; set; }
        public string AdditionalDriverOccupationTitleDescription { get; set; }
        public string AdditionalDriverBusinessTypeDescription { get; set; }
        public string AdditionalDriverDrivingLicenceTypeDescription { get; set; }
        public string AdditionalDriverDrivingLicenceWhereIssuedDescription { get; set; }
        public string AdditionalDriverDrivingLicenceManualOrAutoDescription { get; set; }
        public string AdditionalDriverLicenceYearsHeldDescription { get; set; }
        public string AdditionalDriverUseOfAnyOtherVehicleDescription { get; set; }
        public string AdditionalDriverMedicalConditionsDecription { get; set; }
        public string AdditionalDriverWhyNotWorking { get; set; }
        public int HowManyAdditionalDrivers { get; set; }
        public ClaimCollection AdditionalDriverClaims { get; set; }
        public ConvictionCollection AdditionalDriverConvictions { get; set; }
        public bool AdditionalDriverHasNonMotorConvictions { get; set; }
        public int AdditionalDriverHowManyClaims { get; set; }
        public DateTime AdditionalDriverLicenceDate { get; set; }
        public bool AdditionalDriverResidentSinceBirth { get; set; }
        public DateTime? AdditionalDriverResidentSinceDate { get; set; }

        public AdditonalDriverMother()
        {

        }

        public AdditionalDriver Build()
        {
            HasAdditionalDriver = false;
            RelationshipDescription = "Parent";
            AdditionalDriverTitleDescription = "Mr";
            AdditionalDriverFirstName = "Ted";
            AdditionalDriverLastName = "Ruxpin";
            AdditionalDriverDateOfBirth = new DateTime(1960, 01, 01);
            AdditionalDriverMaritalStatusDescription = "Married";
            AdditionalDriverStudentTypeCodeDescription = "Student";
            AdditionalDriverEmploymentStatusDescription = "Full/Part Time Education";
            AdditionalDriverOccupationTitleDescription = "Nurse";
            AdditionalDriverBusinessTypeDescription = "Hospital";
            AdditionalDriverDrivingLicenceTypeDescription = "Full Licence"; // Full UK or Provisional
            AdditionalDriverDrivingLicenceWhereIssuedDescription = "UK"; //UK or EU or International
            AdditionalDriverDrivingLicenceManualOrAutoDescription = "Full UK (Manual)"; //Full UK or Auto
            AdditionalDriverLicenceYearsHeldDescription = "17 Years";
            AdditionalDriverUseOfAnyOtherVehicleDescription = "No access to any other vehicles";
            AdditionalDriverMedicalConditionsDecription = "No";
            AdditionalDriverResidentSinceBirth = true;
            AdditionalDriverResidentSinceDate = new DateTime(1985, 01, 01);

            return new AdditionalDriver()
                       {
                           HasAdditionalDriver = HasAdditionalDriver,
                           RelationshipDescription = RelationshipDescription,
                           AdditionalDriverTitleDescription = AdditionalDriverTitleDescription,
                           AdditionalDriverFirstName = AdditionalDriverFirstName,
                           AdditionalDriverLastName = AdditionalDriverLastName,
                           AdditionalDriverDateOfBirth = AdditionalDriverDateOfBirth,
                           AdditionalDriverMaritalStatusDescription = AdditionalDriverMaritalStatusDescription,
                           AdditionalDriverStudentTypeCodeDescription = AdditionalDriverStudentTypeCodeDescription,
                           AdditionalDriverEmploymentStatusDescription = AdditionalDriverEmploymentStatusDescription,
                           AdditionalDriverOccupationTitleDescription = AdditionalDriverOccupationTitleDescription,
                           AdditionalDriverBusinessTypeDescription = AdditionalDriverBusinessTypeDescription,
                           AdditionalDriverDrivingLicenceTypeDescription = AdditionalDriverDrivingLicenceTypeDescription,
                           AdditionalDriverDrivingLicenceWhereIssuedDescription = AdditionalDriverDrivingLicenceWhereIssuedDescription,
                           AdditionalDriverDrivingLicenceManualOrAutoDescription = AdditionalDriverDrivingLicenceManualOrAutoDescription,
                           AdditionalDriverLicenceYearsHeldDescription = AdditionalDriverLicenceYearsHeldDescription,
                           AdditionalDriverUseOfAnyOtherVehicleDescription = AdditionalDriverUseOfAnyOtherVehicleDescription,
                           AdditionalDriverMedicalConditionsDecription = AdditionalDriverMedicalConditionsDecription,
                           AdditionalDriverClaims = AdditionalDriverClaims,
                           AdditionalDriverConvictions = AdditionalDriverConvictions,
                           AdditionalDriverResidentSinceBirth = AdditionalDriverResidentSinceBirth,
                           AdditionalDriverResidentSinceDate = AdditionalDriverResidentSinceDate,
                       };
        }

    
        public AdditionalDriverCollection BuildFromCSV(DataRecord data)
        {
            HowManyAdditionalDrivers = Convert.ToInt32(data["HOWMANYADDITIONALDRIVERS"]);
            return GetAdditionalDrivers(data);
        }

        private AdditionalDriverCollection GetAdditionalDrivers(DataRecord data)
        {
            AdditionalDriverCollection additionalDrivers = new AdditionalDriverCollection();

            for (int currentDriver = 1; currentDriver <= HowManyAdditionalDrivers; currentDriver++)
            {
                additionalDrivers.Add(BuildAdditionalDriver(data, currentDriver.ToString()));
            }

            return additionalDrivers;
        }

        private AdditionalDriver BuildAdditionalDriver(DataRecord data, String currentDriver)
        {
            HasAdditionalDriver = Convert.ToBoolean(data["HASADDITIONALDRIVERS"]);


            RelationshipDescription = data["ADD" + currentDriver + "RELATIONSHIP"];
            AdditionalDriverTitleDescription = data["ADD" + currentDriver + "TITLE"];
            AdditionalDriverFirstName = data["ADD" + currentDriver + "FIRSTNAME"];
            AdditionalDriverLastName = data["ADD" + currentDriver + "SURNAME"];
            AdditionalDriverDateOfBirth = Extension.GetDateTime(data["ADD" + currentDriver + "DOB"]); //[61]
            AdditionalDriverMaritalStatusDescription = data["ADD" + currentDriver + "MARITALSTATUS"];
            AdditionalDriverOccupationTitleDescription = data["ADD" + currentDriver + "OCCUPATION"];
            AdditionalDriverBusinessTypeDescription = data["ADD" + currentDriver + "BUSINESSTYPE"];


            if (ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase))
            {
                AdditionalDriverDrivingLicenceTypeDescription = data["ADD" + currentDriver + "LICENCETYPE"];
                AdditionalDriverDrivingLicenceWhereIssuedDescription = data["QS_ADD" + currentDriver + "LICENCEISSUED"];
                AdditionalDriverDrivingLicenceManualOrAutoDescription = data["ADD" + currentDriver + "LICENCETRANSMISSIONRESTRICTION"];
                AdditionalDriverEmploymentStatusDescription = data["QS_ADD" + currentDriver + "EMPLOYMENTSTATUS"];
                if (AdditionalDriverEmploymentStatusDescription.Trim().ToUpper().Equals(Constants.CurrentlyNotWorking))
                {
                    AdditionalDriverWhyNotWorking = data["QS_ADD" + currentDriver + "WHYNOTWORKING"];
                }
                else if (AdditionalDriverEmploymentStatusDescription.Trim().ToUpper().Equals(Constants.FullOrPartTimeEducation))
                {
                    AdditionalDriverStudentTypeCodeDescription = data["ADD" + currentDriver + "STUDENTTYPE"];
                }
            }
            else
            {
                AdditionalDriverDrivingLicenceTypeDescription = data["ADD" + currentDriver + "LICENCETYPE_OLD"];
                AdditionalDriverEmploymentStatusDescription = data["ADD" + currentDriver + "EMPLOYMENTSTATUS"];
                if (AdditionalDriverEmploymentStatusDescription.Trim().ToUpper().Equals(Constants.CurrentlyNotWorking))
                {
                    AdditionalDriverWhyNotWorking = data["ADD" + currentDriver + "WHYNOTWORKING"];
                }
            }
            
            AdditionalDriverLicenceYearsHeldDescription = data["ADD" + currentDriver + "LICENCEHELD"];
            bool matched = Extension.LicenceHeldYearsMatched(AdditionalDriverLicenceYearsHeldDescription);
            if (matched)
            {
                AdditionalDriverLicenceDate = Extension.GetDateTime(data["ADD" + currentDriver + "LICENCEDATE"]);
            }
            AdditionalDriverUseOfAnyOtherVehicleDescription = data["ADD" + currentDriver + "USEOFOTHERVEHICLES"];
            AdditionalDriverMedicalConditionsDecription = data["ADD" + currentDriver + "MEDICALCONDITIONS"];
            AdditionalDriverClaims = new ClaimMother().GetAdditionalDriverClaims(data, currentDriver);
            AdditionalDriverConvictions = new ConvictionMother().GetAdditionalDriverConvictions(data, currentDriver);
            AdditionalDriverResidentSinceBirth = Convert.ToBoolean(data["ADD" + currentDriver + "RESIDENTSINCEBIRTH"]);
            if (!AdditionalDriverResidentSinceBirth)
            {
                AdditionalDriverResidentSinceDate = Extension.GetDateTime(data["ADD" + currentDriver + "RESIDENTSINCEDATE"]);
            }
            AdditionalDriverHasNonMotorConvictions = Convert.ToBoolean(data["ADD" + currentDriver + "CRIMINALCONVICTIONS"]);

            return new AdditionalDriver()
                       {
                           HasAdditionalDriver = HasAdditionalDriver,
                           RelationshipDescription = RelationshipDescription,
                           AdditionalDriverTitleDescription = AdditionalDriverTitleDescription,
                           AdditionalDriverFirstName = AdditionalDriverFirstName,
                           AdditionalDriverLastName = AdditionalDriverLastName,
                           AdditionalDriverDateOfBirth = AdditionalDriverDateOfBirth,
                           AdditionalDriverMaritalStatusDescription = AdditionalDriverMaritalStatusDescription,
                           AdditionalDriverStudentTypeCodeDescription = AdditionalDriverStudentTypeCodeDescription,
                           AdditionalDriverEmploymentStatusDescription = AdditionalDriverEmploymentStatusDescription,
                           AdditionalDriverOccupationTitleDescription = AdditionalDriverOccupationTitleDescription,
                           AdditionalDriverBusinessTypeDescription = AdditionalDriverBusinessTypeDescription,
                           AdditionalDriverDrivingLicenceTypeDescription = AdditionalDriverDrivingLicenceTypeDescription,
                           AdditionalDriverDrivingLicenceWhereIssuedDescription = AdditionalDriverDrivingLicenceWhereIssuedDescription,
                           AdditionalDriverDrivingLicenceManualOrAutoDescription = AdditionalDriverDrivingLicenceManualOrAutoDescription,
                           AdditionalDriverLicenceYearsHeldDescription = AdditionalDriverLicenceYearsHeldDescription,
                           AdditionalDriverUseOfAnyOtherVehicleDescription = AdditionalDriverUseOfAnyOtherVehicleDescription,
                           AdditionalDriverMedicalConditionsDecription = AdditionalDriverMedicalConditionsDecription,
                           AdditionalDriverClaims = AdditionalDriverClaims,
                           AdditionalDriverConvictions = AdditionalDriverConvictions,
                           AdditionalDriverWhyNotWorking = AdditionalDriverWhyNotWorking,
                           AdditionalDriverLicenceDate = AdditionalDriverLicenceDate,
                           AdditionalDriverResidentSinceBirth = AdditionalDriverResidentSinceBirth,
                           AdditionalDriverResidentSinceDate = AdditionalDriverResidentSinceDate,
                           AdditionalDriverHasNonMotorConvictions = AdditionalDriverHasNonMotorConvictions,
                       };
        }
    }
}