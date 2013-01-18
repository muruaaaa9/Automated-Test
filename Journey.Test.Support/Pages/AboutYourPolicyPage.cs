using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Journey.Test.Support.Model;
using OpenQA.Selenium.Remote;

namespace Journey.Test.Support.Pages
{
    public class AboutYourPolicyPage : Page<AboutYourPolicyPage>
    {
        private RemoteWebDriver _driver;
        private bool _qsTestEnabled;

        public AboutYourPolicyPage(RemoteWebDriver driver)
            : base(driver)
        {
            _driver = driver;
            _qsTestEnabled = ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase);
        }

        #region "Web Element - Common"
      
        #endregion

        #region About your policy mappings

        public readonly Dictionary<string, string> PolicyCoverTypes = new Dictionary<string, string>()
                                                                          {
                                                                              {"01", "Comprehensive"},
                                                                              {"02", "Third party fire & theft"},
                                                                              {"03", "Third party only"},
                                                                          };

        public readonly Dictionary<string, string> NcdPeriods = new Dictionary<string, string>()
                                                                    {
                                                                        {"0", "No NCD"},
                                                                        {"3", "3 Years"},
                                                                        {"4", "4 Years"},
                                                                        {"5", "5 Years"},
                                                                        {"6", "6 Years"},
                                                                        {"7", "7 Years"},
                                                                        {"9", "9 Years"},
                                                                        {"17", "17 Years"},
                                                                    };

        public readonly Dictionary<string, string> NcdSources = new Dictionary<string, string>()
                                                                    {
                                                                        {"1", "With this vehicle or a previous vehicle"},
                                                                        {"3", "With a company vehicle"},
                                                                        {"4", "In another country"},
                                                                    };

        #endregion

        #region About your policy page methods

        public AboutYourPolicyPage AssertPolicyDetailsDefaultsAreSelected()
        {
            return this;
        }

        public AboutYourPolicyPage FillPolicyDetails(PolicyDetails policyDetails)
        {
            WaitUntil("MainDriversListQuestion");
            SelectMainDriver(policyDetails.MainDriverDescription);

            SelectRegisteredKeeperOrLegalOwner(policyDetails);

            SelectCoverType(policyDetails.CoverTypeDescription);
            SelectPaymentMethod(policyDetails.PaymentMethodDescription);
            SelectCommencementDate(policyDetails.CommencementDate.Day.ToString(""));
            SetVoluntaryExcess(policyDetails.VoluntaryExcess);
            SelectNcdPeriod(policyDetails.NcdPeriodDescription);
            SelectNcdSource(policyDetails.NcdSourceDescription);
            if (policyDetails.NcdPeriodDescription.Trim().ToUpper().Equals(Constants.NoNcd))
            {
                SelectNamedDricerExperience(policyDetails.NamedDriverExperience);
                if (policyDetails.NamedDriverExperience.Trim().ToUpper()!=(Constants.No))
                {
                    SelectNamedDriverExperieonceYears(policyDetails.NamedDriverExperienceYears);
                }
            }
            return this;
        }

        private void SelectRegisteredKeeperOrLegalOwner(PolicyDetails policyDetails)
        {
            if(!policyDetails.MainDriverRegisteredKeeperAndLegalOwner)
            {
               SelectAreyoutheregisteredkeeperandlegalowner(policyDetails.MainDriverRegisteredKeeperAndLegalOwner);
               Thread.Sleep(500);
               SelectRegisteredKeeper(policyDetails.RegisteredKeeper);

               //REGISTERED KEEPER
               if (policyDetails.RegisteredKeeper.Trim().ToUpper().Equals(Constants.Other))
               {
                   WaitUntilAddAnotherAppears("T_REGKEEPER_ADDPERSON", "ADD PERSON").Click();
                   SelectRegisteredKeeperOrLegalOwnerRelationship(policyDetails.RegisteredKeeperRelationship);
                   SelectRegisteredKeeperOrLegalOwnerOtherTitle(policyDetails.RegisteredKeeperOtherTitle);
                   FillInRegisteredKeeperOrLegalOwnerOtherFirstname(policyDetails.RegisteredKeeperOtherFirstname);
                   FillInRegisteredKeeperOrLegalOwnerSurname(policyDetails.RegisteredKeeperOtherSurname);
                   SelectRegisteredKeeperOrLegalOwnerDobDay(policyDetails.RegisteredKeeperOtherDob.Day);
                   SelectRegisteredKeeperOrLegalOwnerDobMonth(policyDetails.RegisteredKeeperOtherDob.ToString("MMMM"));
                   SelectRegisteredKeeperOrLegalOwnerDobYear(policyDetails.RegisteredKeeperOtherDob.Year.ToString());
                   SelectLegalOwerOtherLiveAtSameAddress(policyDetails.RegisteredKeeperOtherLiveAtSameAddress);
                   WaitUntil("AYP_YP_ADDPERSON_ADD").Click();
               }
               if (Constants.CompanyTypes.Any(ct => policyDetails.RegisteredKeeper.Equals(ct)))
               {
                   FillInKeeperCompanyName(policyDetails.RegisteredKeeperCompanyName);
               }

               //LEGAL OWNER
               Thread.Sleep(500);
               SelectLegalOwner(policyDetails.LegalOwner);

                if (policyDetails.LegalOwner.Trim().ToUpper().Equals(Constants.Other))
               {
                   WaitUntilAddAnotherAppears("T_LEGALOWNER_ADDPERSON", "ADD PERSON").Click();
                   SelectRegisteredKeeperOrLegalOwnerRelationship(policyDetails.LegalOwnerRelationship);
                   SelectRegisteredKeeperOrLegalOwnerOtherTitle(policyDetails.LegalOwnerOtherTitle);
                   FillInRegisteredKeeperOrLegalOwnerOtherFirstname(policyDetails.LegalOwnerOtherFirstname);
                   FillInRegisteredKeeperOrLegalOwnerSurname(policyDetails.LegalOwnerOtherSurname);
                   SelectRegisteredKeeperOrLegalOwnerDobDay(policyDetails.LegalOwnerOtherDob.Day);
                   SelectRegisteredKeeperOrLegalOwnerDobMonth(policyDetails.LegalOwnerOtherDob.ToString("MMMM"));
                   SelectRegisteredKeeperOrLegalOwnerDobYear(policyDetails.LegalOwnerOtherDob.Year.ToString());
                   SelectLegalOwerOtherLiveAtSameAddress(policyDetails.LegalOwnerOtherLiveAtSameAddress);
                   WaitUntil("AYP_YP_ADDPERSON_ADD").Click();
                  
               }
                if (Constants.CompanyTypes.Any(ct => policyDetails.LegalOwner.Equals(ct)))
               {
                   FillInLegalOwnerCompanyName(policyDetails.LegalOwnerCompanyName);
               }
               Thread.Sleep(1000);
            }
        }

        #region "Registered Keeper Or Legal Owner"
        private void SelectAreyoutheregisteredkeeperandlegalowner(bool registeredKeeper)
        {
            CustomRadioIconListClick("RegKeeperAndLegalOwnerQuestion", YesNoMap[registeredKeeper]);
        }
        private void SelectRegisteredKeeper(string registeredKeeper)
        {
            var id = "Q_REGKEEPERTYPES-button";
            var ulClassName = "Q_REGKEEPERTYPES";
            CustomSelectDropdownByText(id, ulClassName, registeredKeeper);
        }
        private void SelectLegalOwner(string value)
        {
            var id = "Q_LEGALOWNERTYPES-button";
            var ulClassName = "Q_LEGALOWNERTYPES";
            CustomSelectDropdownByText(id, ulClassName, value);
        }
        private void FillInKeeperCompanyName(string registeredKeeperCompanyName)
        {
            FillIn("Q_REGKEEPERCOMPANYNAME", registeredKeeperCompanyName);
        }
        private void FillInLegalOwnerCompanyName(string legalOwnerCompanyName)
        {
            FillIn("Q_LEGALOWNERCOMPANYNAME", legalOwnerCompanyName);
        }
        private void SelectRegisteredKeeperOrLegalOwnerRelationship(string relationship)
        {
            var id = "Q_ADDPERSON_RELATIONSHIPTOPROPOSER-button";
            var ulClassName = "Q_ADDPERSON_RELATIONSHIPTOPROPOSER";
            CustomSelectDropdownByText(id, ulClassName, relationship);
        }
        private void SelectRegisteredKeeperOrLegalOwnerOtherTitle(string title)
        {
            var id = "Q_ADDPERSON_TITLE-button";
            var ulClassName = "Q_ADDPERSON_TITLE";
            CustomSelectDropdownByText(id, ulClassName, title);
        }
        private void FillInRegisteredKeeperOrLegalOwnerOtherFirstname(string firstname)
        {
            FillIn("Q_ADDPERSON_FIRSTNAME", firstname);
        }
        private void FillInRegisteredKeeperOrLegalOwnerSurname(string firstname)
        {
            FillIn("Q_ADDPERSON_LASTTNAME", firstname);
        }
        private void SelectRegisteredKeeperOrLegalOwnerDobDay(int day)
        {
            var id = "Q_ADDPERSON_DATEOFBIRTH_day-button";
            var ulClassName = "Q_ADDPERSON_DATEOFBIRTH_day";
            CustomSelectDropdownByText(id, ulClassName, day.ToString());
        }
        private void SelectRegisteredKeeperOrLegalOwnerDobMonth(string month)
        {
            var id = "Q_ADDPERSON_DATEOFBIRTH_month-button";
            var ulClassName = "Q_ADDPERSON_DATEOFBIRTH_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }
        private void SelectRegisteredKeeperOrLegalOwnerDobYear(string year)
        {
            var id = "Q_ADDPERSON_DATEOFBIRTH_year-button";
            var ulClassName = "Q_ADDPERSON_DATEOFBIRTH_year";
            CustomSelectDropdownByText(id, ulClassName, year);
        }
        private void SelectLegalOwerOtherLiveAtSameAddress(bool liveAtSameAddress)
        {
            CustomRadioIconListClick("LiveAtSameAddressQuestion", YesNoMap[liveAtSameAddress]);
        }
        #endregion


        private void SelectMainDriver(string value)
        {
            var id = "Q_AD_MAINDRIVERSLIST-button";
            var ulClassName = "Q_AD_MAINDRIVERSLIST";
            CustomSelectDropdownByText(id, ulClassName, value);
        }
        private void SelectCoverType(string value)
        {
            var id = "Q_PoD_COVERTYPES-button";
            var ulClassName = "Q_PoD_COVERTYPES";
            CustomSelectDropdownByText(id, ulClassName, value);
        }
        private void SelectPaymentMethod(string value)
        {
            CustomRadioIconListClick("PaymentTypesQuestion", value);
        }
        public void SelectCommencementDate(string value)
        {
            CustomSelectCalendarByText("CoverDateQuestion", value);
        }
        private void SetVoluntaryExcess(string value)
        {
            SetSliderValue("VoluntaryExcessQuestion", value);
        }
        private void SelectNcdPeriod(string value)
        {
            var id = "Q_PoD_NCDLIST-button";
            var ulClassName = "Q_PoD_NCDLIST";
            CustomSelectDropdownByText(id, ulClassName, value);
        }
        private void SelectNcdSource(string value)
        {
            CustomRadioIconListClick("NCDSourceQuestion", value);
        }
        private void SelectNamedDricerExperience(string value)
        {
            CustomRadioIconListClick("NamedDriverExperienceQuestion", value);
        }
        private void SelectNamedDriverExperieonceYears(string value)
        {
            var id = "Q_NAMED_DRIVER_EXPERIENCE_YEARS-button";
            var ulClassName = "Q_NAMED_DRIVER_EXPERIENCE_YEARS";
            CustomSelectDropdownByText(id, ulClassName, value);
        }
        public void ClickNextOnPolicyDetails()
        {
            NextButton("AYPP_PD_Next").Click();
            WaitUntil("Q_CONTACT_EMAIL");
        }
        
        #endregion

        #region "Contact Details Mapping"
        public readonly Dictionary<bool, string> EmailMap = new Dictionary<bool, string>
                                                                            {
                                                                                     {true,"Email"},
                                                                                     {false,""}
                                                                            };
        public readonly Dictionary<bool, string> TelephoneMap = new Dictionary<bool, string>
                                                                            {
                                                                                     {true,"Telephone"},
                                                                                     {false,""}
                                                                            };
        public readonly Dictionary<bool, string> SMSMap = new Dictionary<bool, string>
                                                                            {
                                                                                     {true,"SMS"},
                                                                                     {false,""}
                                                                            };
        public readonly Dictionary<bool, string> PostMap = new Dictionary<bool, string>
                                                                            {
                                                                                     {true,"Post"},
                                                                                     {false,""}
                                                                            };
        #endregion
        #region "Contact Details"

        public AboutYourPolicyPage FillContactDetails(ContactDetails contactDetails)
        {
            
            WaitUntil("AYPP_CD_GetQuotes");
            EnterEmail(contactDetails.Email);
            EnterTelephoneNumber(contactDetails.TelephoneNumber);
            if (contactDetails.EmailOptIn)
            {
                SelectEmailOptIn(EmailMap[contactDetails.EmailOptIn]);
            }
            if (contactDetails.TelephoneOptIn)
            {
                SelectTelephoneOptIn(TelephoneMap[contactDetails.TelephoneOptIn]);
            }
            if (contactDetails.SMSOptIn)
            {
                SelectTelephoneSMSIn(SMSMap[contactDetails.SMSOptIn]);
            }
            if (contactDetails.PostOptIn)
            {
                SelectTelephonePostOptIn(PostMap[contactDetails.PostOptIn]);
            }
            AcceptTermsAndConditions();
            return this;
        }

        public void EnterEmail(string email)
        {
            FillIn("Q_CONTACT_EMAIL", email);
        }
        public void EnterTelephoneNumber(string telno)
        {
            FillIn("Q_CONTACT_TEL", telno);
        }
        private void SelectEmailOptIn(string value)
        {
            CustomRadioIconListClick("ContactTypeQuestion", value);
        }
        private void SelectTelephoneOptIn(string value)
        {
            CustomRadioIconListClick("ContactTypeQuestion", value);
        }
        private void SelectTelephoneSMSIn(string value)
        {
            CustomRadioIconListClick("ContactTypeQuestion", value);
        }
        private void SelectTelephonePostOptIn(string value)
        {
            CustomRadioIconListClick("ContactTypeQuestion", value);
        }
        public void AcceptTermsAndConditions()
        {
            CustomRadioIconLongListClick("AcceptTermsAndConditionsQuestion", "Please tick this box");
        }
        public void ClickGetQuotes()
        {
            NextButton("AYPP_CD_GetQuotes").Click();
        }

        #endregion
       
    }
}