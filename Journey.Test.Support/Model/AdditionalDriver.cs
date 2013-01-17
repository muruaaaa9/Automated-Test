using System;
using Journey.Test.Support.ObjectMothers;


namespace Journey.Test.Support.Model
{
    public class AdditionalDriver
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
        public ClaimCollection AdditionalDriverClaims { get; set; }
        public ConvictionCollection AdditionalDriverConvictions { get; set; }
        public DateTime AdditionalDriverLicenceDate { get; set; }
        public bool AdditionalDriverResidentSinceBirth { get; set; }
        public DateTime? AdditionalDriverResidentSinceDate { get; set; }
        public bool AdditionalDriverHasNonMotorConvictions { get; set; }
    }
}