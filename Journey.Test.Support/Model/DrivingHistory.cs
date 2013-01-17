using System;

namespace Journey.Test.Support.Model
{
    public class DrivingHistory
    {
        public string DrivingLicenceTypeCode { get; set; }
        public string QS_DrivingLicenceTypeCode { get; set; }
        public string QS_DrivingLicenceTypeDescription { get; set; }
        public string QS_DrivingLicenceWhereIssuedCode { get; set; }
        public string QS_DrivingLicenceWhereIssuedDescription { get; set; }
        public string QS_DrivingLicenceManualOrAutoCode { get; set; }
        public string QS_DrivingLicenceManualOrAutoDescription { get; set; }
        public string LicenceYearsHeldCode { get; set; }
        public string QS_LicenceYearsHeldCode { get; set; }
        public string QS_LicenceYearsHeldDescription { get; set; }
        public bool HasAdditionalDrivingQualifications { get; set; }
        public string MedicalConditionCode { get; set; }
        public bool HasInsurancePolicyDeclined { get; set; }
        public string AdditionalDrivingQualificationDescription { get; set; }
        public DateTime? DrivingQualificationDate { get; set; }
        public DateTime? LicenceDate { get; set; }
        public string MedicalConditionsDecription { get; set; }
        public bool HasNonMotorConvictions { get; set; }
    }
}