using System;
using System.Linq;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class PolicyDetailsMother
    {
        public string MainDriverDescription { get; set; }
        public string CoverTypeDescription { get; set; }
        public string PaymentMethodDescription { get; set; }
        public DateTime CommencementDate { get; set; }
        public string VoluntaryExcess { get; set; }
        public string NcdPeriodDescription { get; set; }
        public string NcdSourceDescription { get; set; }
        public string NamedDriverExperience { get; set; }
        public string NamedDriverExperienceYears { get; set; }

        public bool MainDriverRegisteredKeeperAndLegalOwner { get; set; }
        public string RegisteredKeeper { get; set; }
        public string RegisteredKeeperCompanyName { get; set; }
        public string RegisteredKeeperRelationship              { get; set; }
        public string RegisteredKeeperOtherTitle                { get; set; }
        public string RegisteredKeeperOtherFirstname            { get; set; }
        public string RegisteredKeeperOtherSurname              { get; set; }
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

        public PolicyDetailsMother()
        {
           
        }

        public PolicyDetails Build()
        {
            MainDriverDescription = "Mr Car Quote";
            CoverTypeDescription = "Comprehensive";
            PaymentMethodDescription = "Monthly";
            CommencementDate = DateTime.Now.Date;
            VoluntaryExcess = "250";
            NcdPeriodDescription = "5 Years";
            NcdSourceDescription = "With this vehicle or a previous vehicle";
            return new PolicyDetails()
                       {
                           MainDriverDescription = MainDriverDescription,
                           CoverTypeDescription = CoverTypeDescription,
                           PaymentMethodDescription = PaymentMethodDescription,
                           CommencementDate = CommencementDate,
                           VoluntaryExcess = VoluntaryExcess,
                           NcdPeriodDescription = NcdPeriodDescription,
                           NcdSourceDescription = NcdSourceDescription,
                           NamedDriverExperience = NamedDriverExperience,
                           NamedDriverExperienceYears = NamedDriverExperienceYears,
                       };

        }

        public PolicyDetails BuildFromCSV(DataRecord data)
        {
            MainDriverDescription = data["MAINDRIVER"];
            CoverTypeDescription = data["COVERTYPE"];
            PaymentMethodDescription = data["PAYMENTTYPE"];
            CommencementDate = DateTime.Now.Date;
            VoluntaryExcess = data["VOLUNTARYEXCESS"];
            NcdPeriodDescription = data["NCDYEARS"];
            NcdSourceDescription = data["NCDFROM"];
            if (NcdPeriodDescription.Trim().ToUpper().Equals(Constants.NoNcd))
            {
                NamedDriverExperience = data["NAMEDDRIVEREXP"];
                if (NamedDriverExperience != Constants.No)
                {
                    NamedDriverExperienceYears = data["NAMEDDRIVEREXPYEARS"];
                }
            }

            MainDriverRegisteredKeeperAndLegalOwner = Convert.ToBoolean(data["MAINDRIVERISREGISTEREDKEEPERANDLEGALOWNER"]);
            if (!MainDriverRegisteredKeeperAndLegalOwner)  //Are you the registered keeper or legal owner question wired up
            {
                RegisteredKeeper = data["REGISTEREDKEEPER"];
                LegalOwner = data["LEGALOWNER"];
                
                //AddPerson data wireup for registered keeper
                RegisteredKeeperAddPerson(data, Constants.CompanyTypes);
                //AddPerson data wireup for Legal owner
                LegalOwnerAddPerson(data, Constants.CompanyTypes);
            }

            return new PolicyDetails()
                       {
                           MainDriverDescription = MainDriverDescription,
                           CoverTypeDescription = CoverTypeDescription,
                           PaymentMethodDescription = PaymentMethodDescription,
                           CommencementDate = CommencementDate,
                           VoluntaryExcess = VoluntaryExcess,
                           NcdPeriodDescription = NcdPeriodDescription,
                           NcdSourceDescription = NcdSourceDescription,
                           NamedDriverExperience = NamedDriverExperience,
                           NamedDriverExperienceYears = NamedDriverExperienceYears,
                           MainDriverRegisteredKeeperAndLegalOwner = MainDriverRegisteredKeeperAndLegalOwner,
                           RegisteredKeeper = RegisteredKeeper,
                           RegisteredKeeperCompanyName = RegisteredKeeperCompanyName,
                           RegisteredKeeperRelationship = RegisteredKeeperRelationship,
                           RegisteredKeeperOtherTitle = RegisteredKeeperOtherTitle,          
                           RegisteredKeeperOtherFirstname = RegisteredKeeperOtherFirstname,
                           RegisteredKeeperOtherSurname = RegisteredKeeperOtherSurname,    
                           RegisteredKeeperOtherDob = RegisteredKeeperOtherDob,    
                           RegisteredKeeperOtherLiveAtSameAddress = RegisteredKeeperOtherLiveAtSameAddress,
                           LegalOwner = LegalOwner,
                           LegalOwnerCompanyName = LegalOwnerCompanyName,
                           LegalOwnerRelationship = LegalOwnerRelationship,
                           LegalOwnerOtherTitle = LegalOwnerOtherTitle,
                           LegalOwnerOtherFirstname = LegalOwnerOtherFirstname,
                           LegalOwnerOtherSurname = LegalOwnerOtherSurname,
                           LegalOwnerOtherDob = LegalOwnerOtherDob,
                           LegalOwnerOtherLiveAtSameAddress = LegalOwnerOtherLiveAtSameAddress,

                       };
        }

        private void RegisteredKeeperAddPerson(DataRecord data, string[] companyTypes)
        {
            if (RegisteredKeeper.Trim().ToUpper().Equals(Constants.Other))
            {
                RegisteredKeeperRelationship = data["REGISTEREDKEEPEROTHERRELATIONSHIP"];
                RegisteredKeeperOtherTitle = data["REGISTEREDKEEPEROTHERTITLE"];
                RegisteredKeeperOtherFirstname = data["REGISTEREDKEEPEROTHERFIRSTNAME"];
                RegisteredKeeperOtherSurname = data["REGISTEREDKEEPEROTHERSURNAME"];
                RegisteredKeeperOtherDob = Extension.GetDateTime(data["REGISTEREDKEEPEROTHERDOB"]);
                RegisteredKeeperOtherLiveAtSameAddress = Convert.ToBoolean( data["REGISTEREDKEEPEROTHERLIVESATSAMEADDRESS"]);
            }
            if (companyTypes.Any(ct => RegisteredKeeper.Equals(ct)))
            {
                RegisteredKeeperCompanyName = data["REGISTEREDKEEPERCOMPANYNAME"];
            }
        }
        
        private void LegalOwnerAddPerson(DataRecord data, string[] companyTypes)
        {
            if (LegalOwner.Trim().ToUpper().Equals(Constants.Other))
            {
                LegalOwnerRelationship = data["LEGALOWNEROTHERRELATIONSHIP"];
                LegalOwnerOtherTitle = data["LEGALOWNEROTHERTITLE"];
                LegalOwnerOtherFirstname = data["LEGALOWNEROTHERFIRSTNAME"];
                LegalOwnerOtherSurname = data["LEGALOWNEROTHERSURNAME"];
                LegalOwnerOtherDob = Extension.GetDateTime(data["LEGALOWNEROTHERDOB"]);
                LegalOwnerOtherLiveAtSameAddress = Convert.ToBoolean( data["LEGALOWNEROTHERLIVESATSAMEADDRESS"]);
            }
            if (companyTypes.Any(ct => LegalOwner.Equals(ct)))
            {
                LegalOwnerCompanyName = data["LEGALOWNERCOMPANYNAME"];
            }
        }
    }
}