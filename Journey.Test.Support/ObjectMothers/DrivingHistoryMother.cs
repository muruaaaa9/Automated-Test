using System;
using System.Configuration;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class DrivingHistoryMother
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
        public DateTime LicenceDate { get; set; }
        public string MedicalConditionsDecription { get; set; }
        public bool HasNonMotorConvictions { get; set; }

        public DrivingHistoryMother()
        {
           
        }

        public DrivingHistory Build()
        {
            HasAdditionalDrivingQualifications = false;
            HasInsurancePolicyDeclined = false;
            LicenceYearsHeldCode = "8";
            QS_LicenceYearsHeldCode = "17";
            QS_LicenceYearsHeldDescription = "17 Years";
            DrivingLicenceTypeCode = "F";
            QS_DrivingLicenceTypeCode = "F";  // Full UK or Provisional
            QS_DrivingLicenceTypeDescription = "Full Licence";  // Full UK or Provisional
            QS_DrivingLicenceWhereIssuedCode = "F";  //UK or EU or International
            QS_DrivingLicenceWhereIssuedDescription = "UK";  //UK or EU or International
            QS_DrivingLicenceManualOrAutoCode = "F";  //Full UK or Auto
            QS_DrivingLicenceManualOrAutoDescription = "Full UK (Manual)";  //Full UK or Auto
            MedicalConditionCode = "DVN";
            AdditionalDrivingQualificationDescription = "AA Proficiency";
            DrivingQualificationDate = new DateTime(2010, 1, 1);
            MedicalConditionsDecription = "No";
            HasNonMotorConvictions = false;
            return new DrivingHistory()
                       {
                           DrivingLicenceTypeCode = DrivingLicenceTypeCode,
                           HasAdditionalDrivingQualifications = HasAdditionalDrivingQualifications,
                           HasInsurancePolicyDeclined = HasInsurancePolicyDeclined,
                           LicenceYearsHeldCode = LicenceYearsHeldCode,
                           MedicalConditionCode = MedicalConditionCode,
                           QS_DrivingLicenceManualOrAutoCode = QS_DrivingLicenceManualOrAutoCode,
                           QS_DrivingLicenceManualOrAutoDescription = QS_DrivingLicenceManualOrAutoDescription,
                           QS_DrivingLicenceTypeCode = QS_DrivingLicenceTypeCode,
                           QS_DrivingLicenceTypeDescription = QS_DrivingLicenceTypeDescription,
                           QS_DrivingLicenceWhereIssuedCode = QS_DrivingLicenceWhereIssuedCode,
                           QS_DrivingLicenceWhereIssuedDescription = QS_DrivingLicenceWhereIssuedDescription,
                           QS_LicenceYearsHeldCode = QS_LicenceYearsHeldCode,
                           QS_LicenceYearsHeldDescription = QS_LicenceYearsHeldDescription,
                           AdditionalDrivingQualificationDescription = AdditionalDrivingQualificationDescription,
                           DrivingQualificationDate = DrivingQualificationDate,
                           MedicalConditionsDecription = MedicalConditionsDecription,
                           HasNonMotorConvictions = HasNonMotorConvictions,
                       };
        }

        public DrivingHistory BuildFromCSV(DataRecord data)
        {
            LicenceYearsHeldCode = "8";
            QS_LicenceYearsHeldCode = "17";
            DrivingLicenceTypeCode = "F";
            QS_DrivingLicenceTypeCode = "F";  // Full UK or Provisional
            QS_DrivingLicenceWhereIssuedCode = "F";  //UK or EU or International
            QS_DrivingLicenceManualOrAutoCode = "F";  //Full UK or Auto

            if (ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase))
            {
                QS_DrivingLicenceTypeDescription = data["LICENCETYPE"];  // Full UK or Provisional
                QS_DrivingLicenceWhereIssuedDescription = data["QS_LICENCEISSUED"];  //UK or EU or International

                if (QS_DrivingLicenceTypeDescription.Trim().ToUpper().Equals(Constants.LicenceTypeDescription))
                {
                    if (QS_DrivingLicenceWhereIssuedDescription.Equals(Constants.LicenceWhereIssued))
                    {
                        QS_DrivingLicenceManualOrAutoDescription = data["QS_LICENCETRANSMISSIONRESTRICTION"]; //Full UK or Auto
                    }
                }
            }
            else
            {
                QS_DrivingLicenceTypeDescription = data["LICENCETYPE_OLD"];  // Full UK or Provisional
            }
            
            QS_LicenceYearsHeldDescription = data["LICENCEHELD"];
            bool matched = Extension.LicenceHeldYearsMatched(QS_LicenceYearsHeldDescription);
            if(matched)
            {
                LicenceDate = Extension.GetDateTime(data["LICENCEDATE"]);
            }

            HasAdditionalDrivingQualifications = Convert.ToBoolean(data["DRIVINGQUALIFICATIONS"]);
            if (HasAdditionalDrivingQualifications)
            {
                AdditionalDrivingQualificationDescription = data["DRIVINGQUALIFICATIONDESCRIPTION"];
                DrivingQualificationDate = Extension.GetDateTime(data["DRIVINGQUALIFICATIONDATE"]);
            }
            MedicalConditionsDecription = data["MEDICALCONDITIONS"];
            HasInsurancePolicyDeclined = Convert.ToBoolean(data["INSURANCEDECLINED"]);

            MedicalConditionCode = "DVN";
            HasNonMotorConvictions = Convert.ToBoolean(data["CRIMINALCONVICTIONS"]);
            return new DrivingHistory()
            {
                DrivingLicenceTypeCode = DrivingLicenceTypeCode,
                HasInsurancePolicyDeclined = HasInsurancePolicyDeclined,
                LicenceYearsHeldCode = LicenceYearsHeldCode,
                MedicalConditionCode = MedicalConditionCode,
                QS_DrivingLicenceManualOrAutoCode = QS_DrivingLicenceManualOrAutoCode,
                QS_DrivingLicenceManualOrAutoDescription = QS_DrivingLicenceManualOrAutoDescription,
                QS_DrivingLicenceTypeCode = QS_DrivingLicenceTypeCode,
                QS_DrivingLicenceTypeDescription = QS_DrivingLicenceTypeDescription,
                QS_DrivingLicenceWhereIssuedCode = QS_DrivingLicenceWhereIssuedCode,
                QS_DrivingLicenceWhereIssuedDescription = QS_DrivingLicenceWhereIssuedDescription,
                QS_LicenceYearsHeldCode = QS_LicenceYearsHeldCode,
                QS_LicenceYearsHeldDescription = QS_LicenceYearsHeldDescription,
                HasAdditionalDrivingQualifications = HasAdditionalDrivingQualifications,
                AdditionalDrivingQualificationDescription = AdditionalDrivingQualificationDescription,
                DrivingQualificationDate = DrivingQualificationDate,
                MedicalConditionsDecription = MedicalConditionsDecription,
                HasNonMotorConvictions = HasNonMotorConvictions,
                LicenceDate = LicenceDate ,
            };
        }
    }
}