using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Journey.Test.Support.Model;
using Journey.Test.Support.ObjectMothers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Journey.Test.Support.Pages
{
    public class AboutYouPage : Page<AboutYouPage>
    {
        private RemoteWebDriver _driver;
        private bool _qsTestEnabled;

        public AboutYouPage(RemoteWebDriver driver)
            : base(driver)
        {
            _driver = driver;
            _qsTestEnabled = ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase);
        }

        #region "Web Element - Common"

        private IWebElement FindAddressButton
        {
            get
            {
                  Thread.Sleep(1000);
                  return Driver.FindElement(By.LinkText("Find my address"));
            }
        }

        
        
        #endregion

        #region "Personal Details Mappings"

        public readonly Dictionary<string, string> TitleMap = new Dictionary<string, string>
                                                                  {
                                                             {"MR", "Mr"},
                                                             {"MRS", "Mrs"},
                                                             {"MS", "Ms"},
                                                             {"MISS", "Miss"},
                                                             {"DRM", "Dr - Male"},
                                                             {"DRF", "Dr - Female"},
                                                         };

        public readonly Dictionary<string, string> StudentTypeMap = new Dictionary<string, string>
                                                                  {
                                                             {"S50", "Mature Student - Living At Home"},
                                                             {"S51", "Mature Student - Living Away"},
                                                             {"S48", "Medical Student - Living At Home"},
                                                             {"S49", "Medical Student - Living Away"},
                                                             {"S34", "Student"},
                                                             {"S44", "Student - Living At Home"},
                                                             {"S45", "Student - Living Away"},
                                                             {"S52", "Student Nurse - Living At Home"},
                                                             {"S53", "Student Nurse - Living Away"},
                                                             {"S46", "Student Teacher - Living At Home"},
                                                             {"S47", "Student Teacher - Living Away"},
                                                         };

        public readonly Dictionary<string, string> MaritalStatusMap = new Dictionary<string, string>
                                                                          {
                                                             {"M", "Married"},
                                                             {"B", "Civil Partnered"},
                                                             {"S", "Single"},
                                                             {"P", "Common Law Partnered/Cohabiting"},
                                                             {"D", "Divorced/Dissolved"},
                                                         };

        public readonly Dictionary<string, string> EmploymentStatusMap = new Dictionary<string, string>
                                                                             {
                                                             {PersonDetails.Employed, "Employed"},
                                                             {PersonDetails.SelfEmployed, "Self-Employed"},
                                                             {PersonDetails.CurrentlyNotWorking, "Currently not working"},
                                                             {PersonDetails.FullOrPartTime, "Full/Part Time Education"}
                                                         };

        public readonly Dictionary<string, string> ReasonForNotWorkingMap = new Dictionary<string, string>
                                                                                {
                                                             {"U", "Unemployed"},
                                                             {"H", "House Person"},
                                                             {"F", "Full/Part Time Education"},
                                                             {"R", "Retired"},
                                                             {"N", "Not Employed Due To Disability/Illness"},
                                                         };

        #endregion
        #region "Personal Details Public Methods"

        public AboutYouPage FillPersonalDetails(PersonDetails personDetails)
        {
            WaitUntil("AYP_PD_Next");
            SelectTitle(personDetails.TitleDescription);
            TypeFirstName(personDetails.FirstName);
            TypeLastName(personDetails.LastName);
            SelectDateOfBirth(personDetails.DateOfBirth);
            SelectMaritalStatus(personDetails.MaritalStatusDescription);
            SelectHomeOwner(YesNoMap[personDetails.OwnsHome]);
            SelectChildrenUnderSixteen(YesNoMap[personDetails.HasChildrenUnderSixteen]);
            SelectEmploymentStatus(personDetails.EmploymentStatusDescription);
            Thread.Sleep(1500);
            if (personDetails.EmploymentStatusDescription.Trim().ToUpper().Equals(Constants.FullOrPartTimeEducation))
            {
                SelectTypeOfStudent(personDetails.StudentTypeCodeDescription);
            }
            else if (personDetails.EmploymentStatusDescription.Trim().ToUpper().Equals(Constants.CurrentlyNotWorking))
            {
                SelectWhyNotWorkingDescription(personDetails.WhyNotWorking);
            }
            else 
            {
                EnterOccuptionTitle(personDetails.OccupationTitleDescription.Substring(0, 3));
                SelectOccupationTitle(personDetails.OccupationTitleDescription);
                EnterBusinessType(personDetails.BusinessTypeDescription.Substring(0, 3));
                SelectBusinessType(personDetails.BusinessTypeDescription);
            }
            if (!personDetails.ResidentSinceBirth)
            {
                SelectResidency("Since birth");
                SelectResidencyDateMonth(personDetails.ResidentSinceDate.Value.ToString("MMMM"));
                SelectResidencyDateYear(personDetails.ResidentSinceDate.Value.ToString("yyyy"));
            }
            FillInHouseAddress(personDetails);
            return this;
        }

        public AboutYouPage AssertAboutYouPage()
        {
            WaitUntil("AYP_PD_Next");
            Assert.That(AssertTitle(), Is.EqualTo(true));
            return this;
        }

        #endregion
        #region "Personal Details Private Methods"

        private void SelectTitle(string title)
        {
            var id = "Q_PD_TITLETYPES-button";
            var ulClassName = "Q_PD_TITLETYPES";
            CustomSelectDropdownByText(id, ulClassName, title);

        }
        private void SelectWhyNotWorkingDescription(string whyNotWorking)
        {
            var id = "Q_PD_WHYNOTWORKING-button";
            var ulClassName = "Q_PD_WHYNOTWORKING";
            CustomSelectDropdownByText(id, ulClassName, whyNotWorking);
        }
        private void SelectTypeOfStudent(string title)
        {
            var id = "Q_OI_STUDENT_OCCUPATION-button";
            var ulClassName = "Q_OI_STUDENT_OCCUPATION";
            CustomSelectDropdownByText(id, ulClassName, title);

        }

        private void TypeFirstName(string firstName)
        {
            FillIn("Q_PD_FIRSTNAME", firstName);
        }

        private void TypeLastName(string lastName)
        {
            FillIn("Q_PD_LASTTNAME", lastName);
        }

        private void SelectDateOfBirth(DateTime dateOfBirth)
        {
            SelectDOBDay(dateOfBirth.Day);
            SelectDOBMonth(dateOfBirth.ToString("MMMM"));
            SelectDOBYear(dateOfBirth.Year);
        }

        #region "Date Of Birth"

        private void SelectDOBDay(int day)
        {
            var id = "Q_PD_DATEOFBIRTH_day-button";
            var ulClassName = "Q_PD_DATEOFBIRTH_day";
            CustomSelectDropdownByText(id, ulClassName, day.ToString());
        }

        private void SelectDOBMonth(string month)
        {
            var id = "Q_PD_DATEOFBIRTH_month-button";
            var ulClassName = "Q_PD_DATEOFBIRTH_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }

        private void SelectDOBYear(int year)
        {
            var id = "Q_PD_DATEOFBIRTH_year-button";
            var ulClassName = "Q_PD_DATEOFBIRTH_year";
            CustomSelectDropdownByText(id, ulClassName, year.ToString());
        }

        #endregion

        private void SelectMaritalStatus(string maritalStatusText)
        {
            var id = "Q_PD_MARITALSTATUS-button";
            var ulClassName = "Q_PD_MARITALSTATUS";
            CustomSelectDropdownByText(id, ulClassName, maritalStatusText);
        }

        private void SelectHomeOwner(string yesNo)
        {
            CustomRadioIconListClick("HomeOwnerQuestion", yesNo);
        }

        private void SelectChildrenUnderSixteen(string yesNo)
        {
            CustomRadioIconListClick("AnyChildrenQuestion", yesNo);
        }

        private bool AssertTitle()
        {
            var aboutyou = "ABOUTYOU";
            return Driver.Title.Trim().ToUpper().Contains(aboutyou);
        }

        #region "Home Address"

        private void FillInHouseAddress(PersonDetails personDetails)
        {
            FillInHouseNumber(personDetails.HouseNumber);
            FillInPostCode(personDetails.PostCode);
            ClickFindAddressButton();
        }

        private void FillInHouseNumber(string houseNumber)
        {
            FillIn("HouseNumberTextBox", houseNumber);
        }

        private AboutYouPage FillInPostCode(string postCode)
        {
            FillIn("Q_PD_Address_postcode", postCode);
            return this;
        }

        public AboutYouPage ClickFindAddressButton()
        {
            FindAddressButton.Click();
            Thread.Sleep(250);
            return this;
        }

        public void ClickNextOnPersonalDetails()
        {
            NextButton("AYP_PD_Next").Click();
            WaitUntil("ClaimsAnyQuestion");
        }

        public void ClickNextOnClaimsAndConvictions()
        {
            Thread.Sleep(200);
            NextButton("AYP_CC_Next").Click();
        }

        public void ClickNextOnDrivingHistory()
        {
            NextButton("AYP_DH_Next").Click();
            WaitUntil("AddAdditionalDriverQuestion");
        }

        public void ClickNextOnAdditionalDrivers()
        {
            NextButton("AYP_AD_Next").Click();
        }
        #endregion

        #region "Employment status"

        private void SelectReasonForNotworking(string value)
        {
            var id = "Q_PD_WHYNOTWORKING-button";
            var ulClassName = "Q_PD_WHYNOTWORKING";
            CustomSelectDropdownByText(id, ulClassName, value);
        }

        private void SelectEmploymentStatus(string value)
        {
            CustomRadioIconListClick("EmploymentTypesQuestion", value);
        }

        private void SelectOccupationTitle(string description)
        {
            SelectTextFromPullDownMenu(description);
        }

        private void EnterOccuptionTitle(string threeLetterDescription)
        {
            var id = "Q_PD_OCCUPATION";
            CustomSelectTextBoxByText(id, threeLetterDescription);
        }

        private void EnterBusinessType(string threeLetterDescription)
        {
            var id = "Q_PD_INDUSTRY";
            CustomSelectTextBoxByText(id, threeLetterDescription);
        }

        private void SelectBusinessType(string description)
        {
            SelectTextFromPullDownMenu(description);
        }


        private void SelectResidency(string sinceBirth)
        {
            CustomRadioIconListClick("HowLongYouLivedNUKQuestion", sinceBirth);
        }
        private void SelectResidencyDateMonth(string month)
        {
            var id = "Q_PD_SINCE_BIRTH_month-button";
            var ulClassName = "Q_PD_SINCE_BIRTH_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }
        private void SelectResidencyDateYear(string year)
        {
            var id = "Q_PD_SINCE_BIRTH_year-button";
            var ulClassName = "Q_PD_SINCE_BIRTH_year";
            CustomSelectDropdownByText(id, ulClassName, year);
        }

        #endregion

        #endregion

        #region "Claims Mappings"
        public readonly Dictionary<string, string> ClaimTypeMap = new Dictionary<string, string>
                                                                  {
                                                             {"A", "Accident"},
                                                             {"TH", "Theft"},
                                                             {"O", "Other"},
                                                         };
        public readonly Dictionary<string, string> ClaimWhoWasAtFaultMap = new Dictionary<string, string>
                                                                  {
                                                             {"B", "Both parties"},
                                                             {"N", "No other vehicle involved"},
                                                             {"O", "Other party"},
                                                             {"C", "Our driver"},
                                                             {"S", "Unoccupied vehicle"},
                                                         };

        public readonly Dictionary<string, string> ClaimWasThereAnyInjuriesMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };

        
        public readonly Dictionary<string, string> ClaimsTypeOfDamageMap = new Dictionary<string, string>
                                                                               {
                                                                                   {"0","Damaged - Amount Known"},
                                                                                   {"1","No Damage"},
                                                                                   {"2","Write-Off"},
                                                                                   {"3","Unknown"},
                                                                               };
        public readonly Dictionary<string, string> ClaimsMadeUnderDriversInsuranceMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };

        public readonly Dictionary<string, string> ClaimsWasTheNoClaimsDiscountAffectedMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };

        public readonly Dictionary<string, string> ConvictionsPenaltyPointsGivenMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };
        public readonly Dictionary<string, string> ConvictionsResultInAFineMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };
        public readonly Dictionary<string, string> ConvictionsResultInADrivingBanMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };
        public readonly Dictionary<string, string> ConvictionsWereYouBreathalysedMap = new Dictionary<string, string>
                                                                  {
                                                             {"true", "Yes"},
                                                             {"false", "No"},
                                                         };
        #endregion

        #region "Claims and Convictions"

        public AboutYouPage AssertClaimsAndConvictionsDefaultsAreSelected()
        {
            Thread.Sleep(2000);
            Assert.That(HasMotorAccidentsIsSelectedToNo(), Is.EqualTo(true));
            Assert.That(HasMotorConvictionsIsSelectedToNo(), Is.EqualTo(true));
            Assert.That(HasCriminalConvictionsIsSelectedToNo(), Is.EqualTo(true));
            return this;
        }

        public bool HasMotorAccidentsIsSelectedToNo()
        {
            return GetCustomRadioButtonElement("ClaimsAnyQuestion", "No").Selected;
        }

        public bool HasMotorConvictionsIsSelectedToNo()
        {
            return GetCustomRadioButtonElement("ConvictionsAnyQuestion", "No").Selected;
        }

        public bool HasCriminalConvictionsIsSelectedToNo()
        {
            return GetCustomRadioButtonElement("NMCriminalConvictionsAnyQuestion", "No").Selected;
        }

        public AboutYouPage FillClaims(ClaimCollection claims)
        {
            Thread.Sleep(250);
            if (claims.Count > 0)
            {
                FillClaimsCollection(claims);
            }
            return this;
        }

        private void FillClaimsCollection(ClaimCollection claims)
        {
            if (claims.Count > 0)
            {
                var qDhClaimsany = "Q_DH_CLAIMSANY";
                for (int claimCount = 1; claimCount <= claims.Count; claimCount++)
                {
                    if (claimCount == 1)
                    {
                        LaunchClaimsLightBox(qDhClaimsany);
                        FillAccidentClaims(claims[claimCount - 1]);
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qDhClaimsany, "ADD ANOTHER").Click();
                        FillAccidentClaims(claims[claimCount - 1]);
                    }
                    if (claimCount < claims.Count)
                    {
                        if (WaitUntilAddAnotherAppears(qDhClaimsany, "ADD ANOTHER").Displayed)
                        {
                            WaitUntilAddAnotherAppears(qDhClaimsany, "ADD ANOTHER").Click();
                        }
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qDhClaimsany, "ADD ANOTHER");
                    }
                }
            }
        }

        private void FillAdditionalDriverClaimsCollection(ClaimCollection claims)
        {
            if (claims.Count > 0)
            {
                var qADClaimsany = "Q_AD_CLAIMSANY";

                for (int claimCount = 1; claimCount <= claims.Count; claimCount++)
                {
                    if (claimCount == 1)
                    {
                        LaunchClaimsLightBox("Q_AD_CLAIMSANY");
                        FillClaimBasedOnClaimType(claims[claimCount - 1]);
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qADClaimsany, "ADD ANOTHER").Click();
                        FillClaimBasedOnClaimType(claims[claimCount - 1]);
                    }
                    if (claimCount < claims.Count)
                    {
                        if (WaitUntilAddAnotherAppears(qADClaimsany, "ADD ANOTHER").Displayed)
                        {
                            WaitUntilAddAnotherAppears(qADClaimsany, "ADD ANOTHER").Click();
                        }
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qADClaimsany, "ADD ANOTHER");
                    }
                }
            }
        }

        private void FillClaimBasedOnClaimType(Claim claim)
        {
            if (claim.ClaimTypeDescription.Trim().ToUpper().Equals(Constants.AccidentClaim)) FillAccidentClaims(claim);
            if (claim.ClaimTypeDescription.Trim().ToUpper().Equals(Constants.TheftClaim)) FillTheftClaims(claim);
            if (claim.ClaimTypeDescription.Trim().ToUpper().Equals(Constants.OtherClaim)) FillOtherClaims(claim);
        }


        public AboutYouPage FillConvictions(ConvictionCollection convictions)
        {
            FillConvictionsCollection(convictions);
            return this; 
        }
        private void FillConvictionsCollection(ConvictionCollection convictions)
        {
            if (convictions.Count > 0)
            {
                var qDhConvictionsany = "Q_DH_CONVICTIONSANY";
                for (int convictionCount = 1; convictionCount <= convictions.Count; convictionCount++)
                {
                    if (convictionCount == 1)
                    {
                        LaunchConvictionsLightBox(qDhConvictionsany);
                        FillQSConvictions(convictions[convictionCount -1]);
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qDhConvictionsany, "ADD ANOTHER").Click();
                        FillQSConvictions(convictions[convictionCount - 1]);
                    }
                    if (convictionCount < convictions.Count)
                    {
                        if (WaitUntilAddAnotherAppears(qDhConvictionsany, "ADD ANOTHER").Displayed)
                        {
                            WaitUntilAddAnotherAppears(qDhConvictionsany, "ADD ANOTHER").Click();
                        }
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qDhConvictionsany, "ADD ANOTHER");
                    }
                }

            }
        }

        private void FillQSConvictions(Conviction conviction)
        {
            WaitUntil("ConvictionQuestion");
            EnterConvictionCode(conviction.ConvictionCode.Substring(0, 3));
            SelectConvictionCode(conviction.ConvictionCode);
            SelectConvictionDateMonth(conviction.ConvictionDate.Value.ToString("MMMM"));
            SelectConvictionDateYear(conviction.ConvictionDate.Value.ToString("yyyy"));
            SelectWerePenaltyPointsGiven(YesNoMap[conviction.PenaltyPointsGiven]);
            if (conviction.PenaltyPointsGiven)
            {
                FillInNoOfPoints(conviction.NoOfPoints);
            }
            SelectDidTheConvictionResultInAFine(YesNoMap[conviction.ResultInAFine]);
            if (conviction.ResultInAFine)
            {
                FillInFineAmount(conviction.FineAmount);
            }
            SelectDidTheConvictionResultInADrivingBan(YesNoMap[conviction.ConvictionResultInADrivingBan]);
            if (conviction.ConvictionResultInADrivingBan)
            {
                FillInBanLength(conviction.BanLengnth);
            }
            if (_qsTestEnabled)
            {
                string[] convictionCodeArray = {"DR10", "DR20", "DR30", "DR40", "DR50", "DR60", "CD40", "CD60", "CD70"};
                bool all = convictionCodeArray.Any(s => conviction.ConvictionCode.Substring(0, 4).Equals(s));
                if (all)
                {
                    SelectWereYouBreathalysed(YesNoMap[conviction.WereYouBreathalysed]);
                    if (conviction.WereYouBreathalysed)
                    {
                        FillInBreathalyserReading(conviction.ConvictionsBreathalysedReading);
                    }
                }
            }
            NextButton("AY_CONVICTIONS_ADD").Click();
            Thread.Sleep(1000);
        }

        private void FillAdditionalDriverConvictionsCollection(ConvictionCollection convictions)
        {
            if (convictions.Count > 0)
            {
                var qADConvictionsany = "Q_AD_CONVICTIONSANY";
                for (int convictionCount = 1; convictionCount <= convictions.Count; convictionCount++)
                {
                    if (convictionCount == 1)
                    {
                        LaunchConvictionsLightBox(qADConvictionsany);
                        FillQSConvictions(convictions[convictionCount - 1]);
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qADConvictionsany, "ADD ANOTHER").Click();
                        FillQSConvictions(convictions[convictionCount - 1]);
                    }
                    if (convictionCount < convictions.Count)
                    {
                        if (WaitUntilAddAnotherAppears(qADConvictionsany, "ADD ANOTHER").Displayed)
                        {
                            WaitUntilAddAnotherAppears(qADConvictionsany, "ADD ANOTHER").Click();
                        }
                    }
                    else
                    {
                        WaitUntilAddAnotherAppears(qADConvictionsany, "ADD ANOTHER");
                    }
                }
            }
        }
         private void EnterConvictionCode(string threeLetterDescription)
         {
             var id = "Q_CONVICTION_TYPE";
             CustomSelectTextBoxByText(id, threeLetterDescription);
         }

         private void SelectConvictionCode(string description)
         {
             SelectTextFromPullDownMenu(description);
         }

         private void SelectConvictionDateMonth(string month)
         {
             var id = "Q_CONVICTION_DATE_month-button";
             var ulClassName = "Q_CONVICTION_DATE_month";
             CustomSelectDropdownByText(id, ulClassName, month);
         }

         private void SelectConvictionDateYear(string year)
         {
             var id = "Q_CONVICTION_DATE_year-button";
             var ulClassName = "Q_CONVICTION_DATE_year";
             CustomSelectDropdownByText(id, ulClassName, year);
         }

         private void SelectWerePenaltyPointsGiven(string text)
         {
             CustomRadioIconListClick("PenaltyYNQuestion", text);
         }

         private void FillInNoOfPoints(string text)
         {
             FillIn("Q_CONVICTION_PENALTY_POINTS", text);
         }

         private void SelectDidTheConvictionResultInAFine(string text)
         {
             CustomRadioIconListClick("FineYNQuestion", text);
         }

         private void FillInFineAmount(string text)
         {
             FillIn("Q_CONVICTION_FINE_AMOUNT", text);
         }

        private void SelectDidTheConvictionResultInADrivingBan(string text)
        {
            CustomRadioIconListClick("BanYNQuestion", text);
        }

         private void FillInBanLength(string text)
         {
             FillIn("Q_CONVICTION_BAN_MONTHS", text);
         }

        private void SelectWereYouBreathalysed(string text)
        {
            CustomRadioIconListClick("AlcoholYNQuestion", text);
        }

         private void FillInBreathalyserReading(string text)
         {
             FillIn("Q_CONVICTION_ALCOHOL_LEVEL", text);
         }

         public AboutYouPage FillNonMotoringConvictions(DrivingHistory drivingHistory)
         {
             if (drivingHistory.HasNonMotorConvictions)
             {
                 CustomRadioIconListClick("NMCriminalConvictionsAnyQuestion", "YES");
             }
             return this;
         }
        #endregion
        #region "Conviction Private Methods"

         private void LaunchConvictionsLightBox(string className)
        {
            CustomRadioIconListClick("ConvictionsAnyQuestion",className, "YES");    
        }
        
        #endregion
        #region "Claims Private Methods"

        private void LaunchClaimsLightBox(string className)
        {
            Thread.Sleep(250);
            CustomRadioIconListClick("ClaimsAnyQuestion", className, "YES");    
        }
        
        private void FillAccidentClaims(Claim claim)
        {
            WaitUntil("ClaimTypeQuestion");
            SelectClaimMadeFor(claim.ClaimTypeDescription);
            SelectWhoWasDriving(claim.WhoWasDriving);
            SelectWhoWasAtFault(claim.WhoWasAtFaultDescription);
            SelectWasThereAnyInjuries(YesNoMap[claim.WasThereAnyInjuries]);
            SelectIncidentOccurMonth(claim.ClaimDate.Value.ToString("MMMM"));
            SelectIncidentOccurYear(claim.ClaimDate.Value.ToString("yyyy"));
            SelectTypeOfDamage(claim.TypeOfDamageDescription);
            SelectWasTheClaimMadeUnderDriversInsurance(YesNoMap[claim.ClaimMadeUnderDriverInsurance]);
            if (_qsTestEnabled)
            {
                SelectWasTheNoClaimsDiscountAffected(YesNoMap[claim.NoClaimDiscountAffected]);
            }
            NextButton("AY_CLAIMS_ADD").Click();
            //WaitUntilByLinkText("Add another");
        }
        private void FillTheftClaims(Claim claim)
        {
            WaitUntil("ClaimTypeQuestion");
            SelectClaimMadeFor(claim.ClaimTypeDescription);
            SelectTypeOfTheft(claim.TheftTypeDescription);
            SelectIncidentOccurMonth(claim.ClaimDate.Value.ToString("MMMM"));
            SelectIncidentOccurYear(claim.ClaimDate.Value.ToString("yyyy"));
            SelectTypeOfDamage(claim.TypeOfDamageDescription);
            SelectWasTheClaimMadeUnderDriversInsurance(YesNoMap[claim.ClaimMadeUnderDriverInsurance]);
            NextButton("AY_CLAIMS_ADD").Click();
        }

        private void FillOtherClaims(Claim claim)
        {
            WaitUntil("ClaimTypeQuestion");
            SelectClaimMadeFor(claim.ClaimTypeDescription);
            SelectClaimMadeFor2(claim.ClaimMadeForDescription);
            SelectIncidentOccurMonth(claim.ClaimDate.Value.ToString("MMMM"));
            SelectIncidentOccurYear(claim.ClaimDate.Value.ToString("yyyy"));
            SelectTypeOfDamage(claim.TypeOfDamageDescription);
            SelectWasTheClaimMadeUnderDriversInsurance(YesNoMap[claim.ClaimMadeUnderDriverInsurance]);
            NextButton("AY_CLAIMS_ADD").Click();
        }
        private void SelectClaimMadeFor(string text)
        {
            CustomRadioIconListClick("ClaimTypeQuestion", text);    
        }
        private void SelectWhoWasDriving(string text)
        {
            var id = "Q_CLAIMS_WHO_WAS_DRIVING-button";
            var ulClassName = "Q_CLAIMS_WHO_WAS_DRIVING";
            CustomSelectDropdownByText(id, ulClassName, text);
        }
        private void SelectWhoWasAtFault(string text)
        {
            var id = "Q_CLAIMS_WHO_WAS_AT_FAULT-button";
            var ulClassName = "Q_CLAIMS_WHO_WAS_AT_FAULT";
            CustomSelectDropdownByText(id, ulClassName, text);
        }
        private void SelectWasThereAnyInjuries(string text)
        {
            CustomRadioIconListClick("AnyInjuriesQuestion", text);
        }

        private void SelectIncidentOccurMonth(string month)
        {
            var id = "Q_CLAIMS_INCIDENT_DATE_month-button";
            var ulClassName = "Q_CLAIMS_INCIDENT_DATE_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }
        private void SelectIncidentOccurYear(string year)
        {
            var id = "Q_CLAIMS_INCIDENT_DATE_year-button";
            var ulClassName = "Q_CLAIMS_INCIDENT_DATE_year";
            CustomSelectDropdownByText(id, ulClassName, year);
        }
        private void SelectTypeOfDamage(string text)
        {
            var id = "Q_CLAIMS_TYPE_OF_DAMAGE-button";
            var ulClassName = "Q_CLAIMS_TYPE_OF_DAMAGE";
            CustomSelectDropdownByText(id, ulClassName, text);
        }
        private void SelectTypeOfTheft(string typeOfTheft)
        {
            var id = "Q_CLAIMS_THEFT_TYPE-button";
            var ulClassName = "Q_CLAIMS_THEFT_TYPE";
            CustomSelectDropdownByText(id, ulClassName, typeOfTheft);
        }
        private void SelectClaimMadeFor2(string claimMadeFor)
        {
            var id = "Q_CLAIMS_OTHER_CLAIM_TYPE-button";
            var ulClassName = "Q_CLAIMS_OTHER_CLAIM_TYPE";
            CustomSelectDropdownByText(id, ulClassName, claimMadeFor);
        }
        private void SelectWasTheClaimMadeUnderDriversInsurance(string text)
        {
            CustomRadioIconListClick("ClaimMadeUnderInsuranceQuestion", text);
        }
        private void SelectWasTheNoClaimsDiscountAffected(string text)
        {
            CustomRadioIconListClick("ClaimAffectedNCDQuestion", text);
        }
        #endregion

        #region "Driving History Mappings"
        public readonly Dictionary<string, string> DrivingLicenceTypesCodeMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"F","Full UK Car"},
                                                                                     {"P","Provisional UK"},
                                                                                     {"I","Other International"},
                                                                                     {"E", "Full EEC"}
                                                                            };

        public readonly Dictionary<string, string> LicenceYearsHeldMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"0","Less than 1 Year"},
                                                                                     {"4","4 Years"},
                                                                                     {"8","8 Years"},
                                                                                     {"17","17 Years"},
                                                                                     {"25","25 Years +"}
                                                                            };

        public readonly Dictionary<string, string> FullOrProivisionalLicenceCodeMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"F","Full Licence"},
                                                                                     {"P","Provisional Licence"}
                                                                            };

        public readonly Dictionary<string, string> LicenceWhereIssuedCodeMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"F","UK"},
                                                                                     {"E","EU"},
                                                                                     {"H","European non-EU"},
                                                                                     {"N","International"}
                                                                            };

        public readonly Dictionary<string, string> LicenceManualOrAutomaticCodeMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"F","Full UK (Manual)"},
                                                                                     {"FA","Full UK (Automatic only)"}
                                                                            };


        #endregion
        #region "Driving History Methods"

        public AboutYouPage AssertDrivingHistoryDefaultsAreSelected()
        {
            Assert.That(AnyAdditionalDrivingQuialificationsToNo(), Is.EqualTo(true));
            Assert.That(HasInsurancePolicyDeclinedToNo(), Is.EqualTo(true));
            //Assert.That(HasDrivingLicenceTypeToFullUK(), Is.EqualTo(true));                           //TODO
            //Assert.That(HasMedicalConditionsOrDisabilitesRepredToDVLA(), Is.EqualTo(true));           //TODO
            return this;
        }

        public AboutYouPage FillDrivingHistory(DrivingHistory drivingHistory)
        {
            if (_qsTestEnabled)
            {
                WaitUntil("LicenceFullOrProvisionalQuestion");
                SelectFullOrProvisionalDrivingLicence(drivingHistory.QS_DrivingLicenceTypeDescription);
                SelectLicenceWhereIssued(drivingHistory.QS_DrivingLicenceWhereIssuedDescription);
                if (drivingHistory.QS_DrivingLicenceTypeDescription.Trim().ToUpper().Equals(Constants.LicenceTypeDescription))
                {
                    if (drivingHistory.QS_DrivingLicenceWhereIssuedDescription.Trim().ToUpper().Equals(Constants.LicenceWhereIssued))
                    {
                        SelectLicenceManualOrAutomaticOnly(drivingHistory.QS_DrivingLicenceManualOrAutoDescription);
                    }
                }
            }
            else
            {
                WaitUntil("LicenceTypeQuestion");
                SelectLicenceType(drivingHistory.QS_DrivingLicenceTypeDescription);
            }
            SelectLicenceYearsHeld(drivingHistory.QS_LicenceYearsHeldDescription);
            var matched = Extension.LicenceHeldYearsMatched(drivingHistory.QS_LicenceYearsHeldDescription);
            if (matched)
            {
                if (drivingHistory.QS_LicenceYearsHeldDescription.Trim().ToUpper().Equals(Constants.LessThanOneYear))
                {
                    SelectLicenceDateDay(drivingHistory.LicenceDate.Value.Day);
                    SelectLicenceDateMonth(drivingHistory.LicenceDate.Value.ToString("MMMM"));
                    SelectLicenceDateYear(drivingHistory.LicenceDate.Value.Year);
                }
                else
                {
                    SelectLicenceDateMonth(drivingHistory.LicenceDate.Value.ToString("MMMM"));
                    SelectLicenceDateYear(drivingHistory.LicenceDate.Value.Year);
                }
            }

            SelectAnyDrivingQualifications(YesNoMap[drivingHistory.HasAdditionalDrivingQualifications]);
            if (drivingHistory.HasAdditionalDrivingQualifications)
            {
                SelectDrivingQualificationDescription(drivingHistory.AdditionalDrivingQualificationDescription);
                SelectDrivingQualificationMonth(drivingHistory.DrivingQualificationDate.Value.ToString("MMMM"));
                SelectDrinvingQualificationYear(drivingHistory.DrivingQualificationDate.Value.ToString("yyyy"));
            }
            SelectMedicalConditionDescription(drivingHistory.MedicalConditionsDecription);
            SelectInsurancePolicyDeclined(YesNoMap[drivingHistory.HasInsurancePolicyDeclined]);
            return this;
        }

        

        #region "AssertDrivingHistoryDefaults"
        private bool HasMedicalConditionsOrDisabilitesRepredToDVLA()
        {
            var selectElement = GetCustomSelectText("DVLAMedicalConditionQuestion");
            return selectElement.Text.Trim().ToUpper().Equals(Constants.No);
        }

        private bool HasDrivingLicenceTypeToFullUK()
        {
            return GetCustomRadioButtonElement("LicenceTypeQuestion", "Full UK Car").Selected;
        }

        private bool AnyAdditionalDrivingQuialificationsToNo()
        {
            return GetCustomRadioButtonElement("DrivingQualificationAnyQuestion", "No").Selected;
        }

        private bool HasInsurancePolicyDeclinedToNo()
        {
            return GetCustomRadioButtonElement("InsurancePolicyDeclinedQuestion", "No").Selected;
        }
        #endregion
        #region "FillDrivingHistory"

        private void SelectLicenceType(string text)
        {
            CustomRadioIconListClick("LicenceTypeQuestion", text);
        }

        private void SelectFullOrProvisionalDrivingLicence(string text)
        {
            CustomRadioIconListClick("LicenceFullOrProvisionalQuestion", text);
        }

        private void SelectLicenceWhereIssued(string text)
        {
            CustomRadioIconListClick("LicenceIssueLocationQuestion", text);
        }

        private void SelectLicenceManualOrAutomaticOnly(string text)
        {
            CustomRadioIconListClick("LicenceManualAutoQuestion", text);
        }

        private void SelectLicenceYearsHeld(string yearsHeld)
        {
            var id = "Q_DH_LICENCEHELDYEARS-button";
            var ulClassName = "Q_DH_LICENCEHELDYEARS";
            CustomSelectDropdownByText(id, ulClassName, yearsHeld);
        }

        private void SelectAnyDrivingQualifications(string yesNo)
        {
            CustomRadioIconListClick("DrivingQualificationAnyQuestion", yesNo);
        }

        private void SelectDrivingQualificationDescription(string text)
        {
            var id = "Q_DH_DRIVINGQUALIFICATIONTYPES-button";
            var ulClassName = "Q_DH_DRIVINGQUALIFICATIONTYPES";
            CustomSelectDropdownByText(id, ulClassName, text);
        }

        private void SelectDrivingQualificationMonth(string month)
        {
            var id = "Q_DH_DRIVINGQUALIFICATIONDATE_month-button";
            var ulClassName = "Q_DH_DRIVINGQUALIFICATIONDATE_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }

        private void SelectDrinvingQualificationYear(string year)
        {
            var id = "Q_DH_DRIVINGQUALIFICATIONDATE_year-button";
            var ulClassName = "Q_DH_DRIVINGQUALIFICATIONDATE_year";
            CustomSelectDropdownByText(id, ulClassName, year);
        }

        private void SelectMedicalConditionDescription(string text)
        {
            var id = "Q_DH_DVLAMEDICALCONDITIONQUESTION-button";
            var ulClassName = "Q_DH_DVLAMEDICALCONDITIONQUESTION";
            CustomSelectDropdownByText(id, ulClassName, text);
        }
        private void SelectInsurancePolicyDeclined(string yesNo)
        {
            CustomRadioIconListClick("InsurancePolicyDeclinedQuestion", yesNo);
        }
        
        

        #endregion

        #endregion

        #region "Additional Drivers Methods"

        public AboutYouPage FillAdditionalDrivers(AdditionalDriverCollection additionalDrivers)
        {

            if (additionalDrivers.Count > 0)
            {
                AdditionalDriversCollection(additionalDrivers);
            }

            return this;
        }

        private void AdditionalDriversCollection(AdditionalDriverCollection additionalDrivers)
        {
            var qADDrivers = "addRemoveSummary";
            for (int driver = 1; driver <= additionalDrivers.Count; driver++)
            {
                if (driver > 1)
                {
                    WaitUntilAddAnotherAppears(qADDrivers, "ADD ANOTHER").Click();
                }
                FillAdditionalDriver(additionalDrivers[driver - 1], driver);

            }
        }

        private void FillAdditionalDriver(AdditionalDriver additionalDriver, int driver)
        {
            SelectAdditonalDriver(YesNoMap[additionalDriver.HasAdditionalDriver]);
            if (additionalDriver.HasAdditionalDriver)
            {

                WaitUntil("RelationshipToAdditionalDriverQuestion");
                SelectRelationshipDescription(additionalDriver.RelationshipDescription);
                SelectTitleDescription(additionalDriver.AdditionalDriverTitleDescription);
                TypeAdditionalDriverFirstName(additionalDriver.AdditionalDriverFirstName);
                TypeAdditionalDriverLastName(additionalDriver.AdditionalDriverLastName);
                SelectAdditionalDriverDateOfBirth(additionalDriver.AdditionalDriverDateOfBirth);
                SelectAdditionalDriverMaritalStatus(additionalDriver.AdditionalDriverMaritalStatusDescription);
                SelectAdditionalDriverEmploymentStatus(additionalDriver.AdditionalDriverEmploymentStatusDescription);
                Thread.Sleep(1500);
                if (additionalDriver.AdditionalDriverEmploymentStatusDescription.Trim().ToUpper().Equals(Constants.FullOrPartTimeEducation))
                {
                    SelectAdditionalDriverTypeOfStudent(additionalDriver.AdditionalDriverStudentTypeCodeDescription);
                }
                else if (additionalDriver.AdditionalDriverEmploymentStatusDescription.Trim().ToUpper().Equals(Constants.CurrentlyNotWorking))
                {
                    SelectAdditionalDriverWhyNotWorking(additionalDriver.AdditionalDriverWhyNotWorking);
                }
                else
                {
                    EnterAdditionalDriverEnterOccuptionTitle(additionalDriver.AdditionalDriverOccupationTitleDescription.Substring(0, 3));
                    SelectAdditionalDriverSelectOccupationTitle(additionalDriver.AdditionalDriverOccupationTitleDescription);
                    EnterAdditionalDriverEnterBusinessType(additionalDriver.AdditionalDriverBusinessTypeDescription.Substring(0, 3));
                    SelectAdditionalDriverSelectBusinessType(additionalDriver.AdditionalDriverBusinessTypeDescription);
                }
                if (!additionalDriver.AdditionalDriverResidentSinceBirth)
                {
                    SelectAdditionalDriverResidency("Since birth");
                    SelectAdditionalDriverResidencyDateMonth(additionalDriver.AdditionalDriverResidentSinceDate.Value.ToString("MMMM"));
                    SelectAdditionalDriverResidencyDateYear(additionalDriver.AdditionalDriverResidentSinceDate.Value.ToString("yyyy"));
                }
                if (_qsTestEnabled)
                {
                    SelectAdditionalDriverFullOrProvisionalDrivingLicence(additionalDriver.AdditionalDriverDrivingLicenceTypeDescription);
                    SelectAdditionalDriverLicenceWhereIssued(additionalDriver.AdditionalDriverDrivingLicenceWhereIssuedDescription);
                    if (additionalDriver.AdditionalDriverDrivingLicenceTypeDescription.Trim().ToUpper().Equals(Constants.LicenceTypeDescription))
                    {
                        if (additionalDriver.AdditionalDriverDrivingLicenceWhereIssuedDescription.Trim().ToUpper().Equals(Constants.LicenceWhereIssued))
                        {
                            SelectLicenceAdditionalDriverManualOrAutomaticOnly(additionalDriver.AdditionalDriverDrivingLicenceManualOrAutoDescription);
                        }
                    }
                }
                else
                {
                    SelectAdditionalDriverLicenceType(additionalDriver.AdditionalDriverDrivingLicenceTypeDescription);
                }
                SelectAdditionalDriverLicenceYearsHeld(additionalDriver.AdditionalDriverLicenceYearsHeldDescription);
                var matched = Extension.LicenceHeldYearsMatched(additionalDriver.AdditionalDriverLicenceYearsHeldDescription);
                if (matched)
                {
                    if (additionalDriver.AdditionalDriverLicenceYearsHeldDescription.Trim().ToUpper().Equals(Constants.LessThanOneYear))
                    {
                        SelectAdditionalDriverLicenceDateDay(additionalDriver.AdditionalDriverLicenceDate.Day);
                        SelectAdditionalDriverLicenceDateMonth(additionalDriver.AdditionalDriverLicenceDate.ToString("MMMM"));
                        SelectAdditionalDriverLicenceDateYear(additionalDriver.AdditionalDriverLicenceDate.Year);
                    }
                    else
                    {
                        SelectAdditionalDriverLicenceDateMonth(additionalDriver.AdditionalDriverLicenceDate.ToString("MMMM"));
                        SelectAdditionalDriverLicenceDateYear(additionalDriver.AdditionalDriverLicenceDate.Year);
                    }
                }

                SelectAdditionalDriverUseOfAnyOtherVehicle(additionalDriver.AdditionalDriverUseOfAnyOtherVehicleDescription);
                SelectAdditionalDriverMedicalConditionDescription(additionalDriver.AdditionalDriverMedicalConditionsDecription);
                NextButton("AY_ADDDRIVER_Next").Click();
                //WaitUntilByClass("Q_AD_CLAIMSANY");
                Thread.Sleep(1500);
                //Additional Drivers
                if (additionalDriver.AdditionalDriverClaims.Count > 0)
                {
                    FillAdditionalDriverClaimsCollection(additionalDriver.AdditionalDriverClaims);
                }
                if (additionalDriver.AdditionalDriverConvictions.Count > 0)
                {
                    FillAdditionalDriverConvictionsCollection(additionalDriver.AdditionalDriverConvictions);
                }
                Thread.Sleep(500);
                SelectAdditionalDriverNonMotoringConviction(YesNoMap[additionalDriver.AdditionalDriverHasNonMotorConvictions]);
                NextButton("AY_ADDDRIVER_CANDC_Next").Click();
                //WaitUntilByLinkText("Add another");
                WaitUntilAddAnotherAppears("Q_AD_CLAIMSANY", "ADD ANOTHER");
            }
        }

        public AboutYouPage AssertAdditionalDriversDefaultsAreSelected()
        {
            Assert.That(AdditionalDriverDetailsDefaultToNo(), Is.EqualTo(true));
            return this;
        }

        private bool AdditionalDriverDetailsDefaultToNo()
        {
            return GetCustomRadioButtonElement("AddAdditionalDriverQuestion", "No").Selected;
        }


        #region "Fill Addtional Drivers"

        private void SelectAdditonalDriver(string yesNo)
        {
            CustomRadioIconListClick("AddAdditionalDriverQuestion", yesNo);
        }
        private void SelectRelationshipDescription(string relationship)
        {
            var id = "Q_AD_RELATIONSHIPTOADDITIONALDRIVER-button";
            var ulClassName = "Q_AD_RELATIONSHIPTOADDITIONALDRIVER";
            CustomSelectDropdownByText(id, ulClassName, relationship);
        }
        private void SelectTitleDescription(string relationship)
        {
            var id = "Q_AD_TITLETYPES-button";
            var ulClassName = "Q_AD_TITLETYPES";
            CustomSelectDropdownByText(id, ulClassName, relationship);
        }
        private void TypeAdditionalDriverFirstName(string firstName)
        {
            FillIn("Q_AD_FIRSTNAME", firstName);
        }
        private void TypeAdditionalDriverLastName(string firstName)
        {
            FillIn("Q_AD_LASTTNAME", firstName);
        }
        private void SelectAdditionalDriverDateOfBirth(DateTime dateOfBirth)
        {
            SelectAdditionalDriverDOBDay(dateOfBirth.Day);
            SelectAdditionalDriverDOBMonth(dateOfBirth.ToString("MMMM"));
            SelectAdditionalDriverDOBYear(dateOfBirth.Year);
        }

        #region "AdditionalDriver Date Of Birth"

        private void SelectAdditionalDriverDOBDay(int day)
        {
            Thread.Sleep(200);
            var id = "Q_AD_DATEOFBIRTH_day-button";
            var ulClassName = "Q_AD_DATEOFBIRTH_day";
            CustomSelectDropdownByText(id, ulClassName, day.ToString());
        }

        private void SelectAdditionalDriverDOBMonth(string month)
        {
            var id = "Q_AD_DATEOFBIRTH_month-button";
            var ulClassName = "Q_AD_DATEOFBIRTH_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }

        private void SelectAdditionalDriverDOBYear(int year)
        {
            var id = "Q_AD_DATEOFBIRTH_year-button";
            var ulClassName = "Q_AD_DATEOFBIRTH_year";
            CustomSelectDropdownByText(id, ulClassName, year.ToString());
        }

        #endregion
        private void SelectAdditionalDriverMaritalStatus(string maritalStatusText)
        {
            var id = "Q_AD_MARITALSTATUS-button";
            var ulClassName = "Q_AD_MARITALSTATUS";
            CustomSelectDropdownByText(id, ulClassName, maritalStatusText);
        }
        private void SelectAdditionalDriverEmploymentStatus(string value)
        {
            CustomRadioIconListByClassClick("Q_AD_EMPLOYMENT_TYPES", value);
        }

        private void SelectAdditionalDriverWhyNotWorking(string whyNotWorking)
        {
            var id = "Q_AD_WHYNOTWORKING-button";
            var ulClassName = "Q_AD_WHYNOTWORKING";
            CustomSelectDropdownByText(id, ulClassName, whyNotWorking);
        }


        private void SelectAdditionalDriverTypeOfStudent(string title)
        {
            var id = "Q_AD_STUDENT_OCCUPATION-button";
            var ulClassName = "Q_AD_STUDENT_OCCUPATION";
            CustomSelectDropdownByText(id, ulClassName, title);

        }
        private void EnterAdditionalDriverEnterOccuptionTitle(string threeLetterDescription)
        {
            var id = "Q_AD_OCCUPATION";
            CustomSelectTextBoxByText(id, threeLetterDescription);
        }
        private void SelectAdditionalDriverSelectOccupationTitle(string description)
        {
            SelectTextFromPullDownMenu(description);
        }
        private void EnterAdditionalDriverEnterBusinessType(string threeLetterDescription)
        {
            var id = "Q_AD_INDUSTRY";
            CustomSelectTextBoxByText(id, threeLetterDescription);
        }
        private void SelectAdditionalDriverSelectBusinessType(string description)
        {
            SelectTextFromPullDownMenu(description);
        }



        private void SelectAdditionalDriverResidency(string sinceBirth)
        {
            CustomRadioIconListClick("HowLongYouLivedNUKQuestion", sinceBirth);
        }
        private void SelectAdditionalDriverResidencyDateMonth(string month)
        {
            var id = "Q_AD_SINCE_BIRTH_month-button";
            var ulClassName = "Q_AD_SINCE_BIRTH_month";
            CustomSelectDropdownByText(id, ulClassName, month);
        }
        private void SelectAdditionalDriverResidencyDateYear(string year)
        {
            var id = "Q_AD_SINCE_BIRTH_year-button";
            var ulClassName = "Q_AD_SINCE_BIRTH_year";
            CustomSelectDropdownByText(id, ulClassName, year);
        }



        private void SelectAdditionalDriverLicenceType(string text)
        {
            CustomRadioIconListByClassClick("Q_AD_LICENCETYPE", text);
        }
        private void SelectAdditionalDriverFullOrProvisionalDrivingLicence(string text)
        {
            CustomRadioIconListByClassClick("Q_AD_LICENCE_FULL_PROVISIONAL", text);
        }
        private void SelectAdditionalDriverLicenceWhereIssued(string text)
        {
            CustomRadioIconListByClassClick("Q_AD_LICENCE_ISSUE_LOCATION", text);
        }
        private void SelectLicenceAdditionalDriverManualOrAutomaticOnly(string text)
        {
            CustomRadioIconListByClassClick("Q_AD_LICENCE_MANUAL_AUTO", text);
        }
        private void SelectAdditionalDriverLicenceYearsHeld(string yearsHeld)
        {
            var id = "Q_AD_LICENCEHELDYEARS-button";
            var ulClassName = "Q_AD_LICENCEHELDYEARS";
            CustomSelectDropdownByText(id, ulClassName, yearsHeld);
        }
        private void SelectLicenceDateDay(int day)
        {
            var id = "Q_DH_LICENCEDATE_day-button";
            var ulClassName = "Q_DH_LICENCEDATE_day";
            CustomSelectDropdownByText(id, ulClassName, day.ToString());
        }
        private void SelectAdditionalDriverLicenceDateDay(int day)
        {
            var id = "Q_AD_LICENCEDATE_day-button";
            var ulClassName = "Q_AD_LICENCEDATE_day";
            CustomSelectDropdownByText(id, ulClassName, day.ToString());
        }
        private void SelectLicenceDateMonth(string month)
        {
            var id = "Q_DH_LICENCEDATE_month-button";
            var ulClassName = "Q_DH_LICENCEDATE_month";
            CustomSelectDropdownByText(id, ulClassName, month.ToString());
        }
        private void SelectAdditionalDriverLicenceDateMonth(string month)
        {
            var id = "Q_AD_LICENCEDATE_month-button";
            var ulClassName = "Q_AD_LICENCEDATE_month";
            CustomSelectDropdownByText(id, ulClassName, month.ToString());
        }
        private void SelectLicenceDateYear(int year)
        {
            var id = "Q_DH_LICENCEDATE_year-button";
            var ulClassName = "Q_DH_LICENCEDATE_year";
            CustomSelectDropdownByText(id, ulClassName, year.ToString());
        }
        private void SelectAdditionalDriverLicenceDateYear(int year)
        {
            var id = "Q_AD_LICENCEDATE_year-button";
            var ulClassName = "Q_AD_LICENCEDATE_year";
            CustomSelectDropdownByText(id, ulClassName, year.ToString());
        }
        private void SelectAdditionalDriverUseOfAnyOtherVehicle(string useOfOtherVehicleText)
        {
            var id = "Q_AD_USEOFANYOTHERVEHICLETYPES-button";
            var ulClassName = "Q_AD_USEOFANYOTHERVEHICLETYPES";
            CustomSelectDropdownByText(id, ulClassName, useOfOtherVehicleText);
        }
        private void SelectAdditionalDriverMedicalConditionDescription(string text)
        {
            var id = "Q_AD_DVLAMEDICALCONDITIONQUESTION-button";
            var ulClassName = "Q_AD_DVLAMEDICALCONDITIONQUESTION";
            CustomSelectDropdownByText(id, ulClassName, text);
        }

        private void SelectAdditionalDriverNonMotoringConviction(string text)
        {
            
            CustomRadioIconListClick("NMCriminalConvictionsAnyQuestion", "Q_AD_NMCRIMINALCONVICTIONSANY", text);
        }
   
        #endregion

        #endregion

    }
}
