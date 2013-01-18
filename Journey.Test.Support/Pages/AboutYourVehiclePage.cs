using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Journey.Test.Support.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Journey.Test.Support.Pages
{
    public class AboutYourVehiclePage : Page<AboutYourVehiclePage>
    {
        private RemoteWebDriver _driver;
        private bool _qsTestEnabled;

        public AboutYourVehiclePage(RemoteWebDriver driver)
            : base(driver)
        {
            _driver = driver;
            _qsTestEnabled = ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase);
        }
        
        #region "Web Element - Common"
        private IWebElement FindButton(string idToFind )
        {
            return WaitUntil(idToFind);
        }
        private IWebElement FindAddressButton
        {
            get
            {
                Thread.Sleep(1000);
                return Driver.FindElement(By.LinkText("Find my address"));
            }
        }

        #endregion
        #region "Vehicle Details WebElement"

        private IWebElement RegistrationNumberTextBox
        {
            get
            {
                return WaitUntil("Q_REGISTRATION_NUMBER");
            }
        }

        #endregion
        #region "Vehicle Details Methods"
        public AboutYourVehiclePage TypeRegistrationNumber(VehicleDetails vehicleDetails)
        {
            Thread.Sleep(300);
            
            if (vehicleDetails.KnownRegistrationNumber)
            {
                SelectKnowRegistartionNumber(YesNoMap[vehicleDetails.KnownRegistrationNumber]);
                Thread.Sleep(300);
            }

            RegistrationNumberTextBox.SendKeys(vehicleDetails.RegistrationNumber + Keys.Tab);
            return this;
        }
        public AboutYourVehiclePage FillManualLookupOnVehicleSelection(VehicleDetails vehicleDetails)
        {
            Thread.Sleep(2000);
            LaunchManualVehicleLookup(YesNoMap[vehicleDetails.KnownRegistrationNumber]);
            Thread.Sleep(300);
            SelectManufacturer(vehicleDetails.AdditionalDetails.Manufacturer);
            Thread.Sleep(300);
            SelectModel(vehicleDetails.AdditionalDetails.Model);
            Thread.Sleep(300);
            SelectRegistrationYearAndLetter(vehicleDetails.AdditionalDetails.RegistrationYearAndLetter);
            Thread.Sleep(300);
            SelectNumberOfDoorsOrStyle(vehicleDetails.AdditionalDetails.NumberOfDoors);
            Thread.Sleep(300);
            SelectTransmission(vehicleDetails.AdditionalDetails.Transmission);
            Thread.Sleep(400);
            SelectVehicleDescription(vehicleDetails.AdditionalDetails.VehicleDescription);
            Thread.Sleep(300);
            return this;
        }
        public AboutYourVehiclePage FillVehicleDetails(VehicleDetails vehicleDetails, ConfigDetails configDetails)
        {
            Thread.Sleep(300);
            WaitUntil("AYVP_VD_Next");
            if (AlarmOrImmobiliserQuestionDisplayed())
            {
                SelectAnAlarmOrImmobiliser(YesNoMap[vehicleDetails.HasAlarm]);
                if (vehicleDetails.HasAlarm)
                {
                    SelectAlarmType(vehicleDetails.AlarmType);
                }
            }
            if (TrackingDeviceQuestionDisplayed())
            {
                SelectTrackingDevice(YesNoMap[vehicleDetails.HasTrackingDevice]);
            }
            if (configDetails.ProductClass.ToUpper().Trim().Equals(Constants.PrivateCar))
            {
                if(IsImportedQuestionDisplayed())
                {
                    SelectCarImported(YesNoMap[vehicleDetails.IsImported]);    
                }
            }
            if (SteeringTypeQuestionDisplayed())
            {
                SelectSteeringType(vehicleDetails.SteeringType);
            }
            if (HowManySeatsQuestionDisplayed())
            {
                SetHowManySeats(vehicleDetails.NumberOfSeats);
            }
            if (VehicleValueQuestionDisplayed())
            {
                FillInVehicleValue(vehicleDetails.CurrentValue);
            }
            if (VehicleModifiedQuestionDisplayed())
            {
                SelectVehicleModified(YesNoMap[vehicleDetails.HasVehicleModified]);
            }
            if (configDetails.ProductClass.ToUpper().Trim().Equals(Constants.LightCommercial))
            {
                if (VanBodyTypeQuestionDisplayed())
                {
                    SelectVanBodyType(vehicleDetails.VanBodyType);
                }
            }

            return this;
        }
        public void ClickFind()
        {
            Thread.Sleep(300);
            FindButton("AYV_VD_Find").Click();
        }
        public void ClickOnThisIsMyVehicle()
        {
            Thread.Sleep(300);
            FindButton("AYVP_FYV_THISISMYVEHICLE").Click();
        }
        public void ClickNextOnVehicleDetails()
        {
            NextButton("AYVP_VD_Next").Click();
        }

        public void ClickNextOnVehicleUsage()
        {
            NextButton("ABY_VU_Next").Click();
        }
        private void SetHowManySeats(string value)
        {
            SetSliderValue("NumberOfSeatsQuestion", value);
        }
        private void SelectAnAlarmOrImmobiliser(string yesNo)
        {
            CustomRadioIconListClick("AlarmImmobiiserQuestion", yesNo);
        }
        private void SelectKnowRegistartionNumber(string yesNo)
        {
            CustomRadioIconListClick("RegistrationNumberKnownQuestion", yesNo);
        }
        private void SelectCarImported(string yesNo)
        {
            CustomRadioIconListClick("ImportedQuestion", yesNo);
        }
        private void SelectTrackingDevice(string yesNo)
        {
            CustomRadioIconListClick("TrackingDeviceQuestion", yesNo);
        }
        private void SelectVehicleModified(string yesNo)
        {
            CustomRadioIconListClick("ModificationsQuestion", yesNo);
        }
        private void SelectAlarmType(string alarmType)
        {
            var id = "Q_ALARM_IMMOBILISER_TYPE-button";
            var ulClassName = "Q_ALARM_IMMOBILISER_TYPE";
            CustomSelectDropdownByText(id, ulClassName, alarmType);
        }
        private void SelectVanBodyType(string bodyType)
        {
            var id = "Q_VAN_BODY_TYPE-button";
            var ulClassName = "Q_VAN_BODY_TYPE";
            CustomSelectDropdownByText(id,ulClassName,bodyType);
        }
        private void SelectManufacturer(string manufacturer)
        {
            var id = "s1-button";
            var ulClassName = "s1";
            CustomSelectDropdownByText(id, ulClassName, manufacturer);
        }
        private void SelectModel(string model)
        {
            var id = "s2-button";
            var ulClassName = "s2";
            CustomSelectDropdownByText(id, ulClassName, model);
        }
        private void SelectRegistrationYearAndLetter(string regYearAndLetter)
        {
            var id = "s3-button";
            var ulClassName = "s3";
            CustomSelectDropdownByText(id, ulClassName, regYearAndLetter);
        }
        private void SelectNumberOfDoorsOrStyle(string numberOfDoors)
        {
            var id = "s4-button";
            var ulClassName = "s4";
            CustomSelectDropdownByText(id, ulClassName, numberOfDoors);
        }
        private void SelectTransmission(string transmission)
        {
            var id = "s7-button";
            var ulClassName = "s7";
            CustomSelectDropdownByText(id, ulClassName, transmission);
        }
        private void SelectVehicleDescription(string vehicleDescription)
        {
            CustomRadioIconListByClassClick("carFinder", vehicleDescription);
        }
        private void SelectSteeringType(string steeringType)
        {
            CustomRadioIconListClick("SteeringTypeQuestion", steeringType);
        }
        public void ClickStartComparingButton()
        {
            Thread.Sleep(2000);
            if (Driver.FindElement(By.Id("startJourneyButton_Car")).Displayed)
            {
                Driver.FindElement(By.Id("startJourneyButton_Car")).Click();
            }
        }
        private void FillInVehicleValue(string value)
        {
            FillIn("Q_VEHICLE_VALUE", value);
        }

        private bool AlarmOrImmobiliserQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("AlarmImmobiiserQuestion");
        }
        private bool TrackingDeviceQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("TrackingDeviceQuestion");
        }
        private bool SteeringTypeQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("SteeringTypeQuestion");
        }
        private bool HowManySeatsQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("NumberOfSeatsQuestion");
        }
        private bool VehicleValueQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("VehicleValueQuestion");
        }
        private bool VehicleModifiedQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("ModificationsQuestion");
        }
        private bool VanBodyTypeQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("VanBodyTypeQuestion");
        }
        private bool IsImportedQuestionDisplayed()
        {
            return IsTheQuestionDisplayed("ImportedQuestion");
        }
        private void LaunchManualVehicleLookup(string yesNo)
        {
            CustomRadioIconListClick("RegistrationNumberKnownQuestion", yesNo);
        }
        #endregion

        #region "Vehicle Usage Mappings"


        public readonly Dictionary<string, string> VehicleKeptDuringDayCodeMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"VKD1","At home"},
                                                                                     {"VKD2","Office or factory car park"},
                                                                                     {"VKD3","Open public car park"},
                                                                                     {"VKD4", "Secure public car park"},
                                                                                     {"VKD5", "Street away from home"}
                                                                            };

        public readonly Dictionary<string, string> QS_OverNightParkingMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"10","LockedGarage"},
                                                                                     {"11","Unlocked Garage"},
                                                                                     {"4","Drive"},
                                                                                     {"12","Street Outside Home"},
                                                                                     {"13","Street away from home"},
                                                                                     {"O","Other"}
                                                                            };

        public readonly Dictionary<string, string> OverNightParkingMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"1","LockedGarage"},
                                                                                     {"3","Public Road"},
                                                                                     {"4","Drive"},
                                                                                     {"2","Private Property"},
                                                                                     {"5","Car Park"},
                                                                                     {"7","Locked Compound"}
                                                                            };

        public readonly Dictionary<string, string> QS_HowManyVehiclesAreThereMap = new Dictionary<string, string>
                                                                            {
                                                                                     {"1","1"},
                                                                                     {"2","2"},
                                                                                     {"3","3"},
                                                                                     {"4","4"},
                                                                                     {"5","5"},
                                                                                     {"6","6"},
                                                                                     {"7","7"},
                                                                                     {"8","8"},
                                                                                     {"9","9"}
                                                                            };


        #endregion
        #region "Vehicle Usage Publice Methods"
        public AboutYourVehiclePage FillLightCommercialVehicleUsage(VehicleUsage vehicleUsage)
        {
            WaitUntil("ABY_VU_Next");
          
            if (vehicleUsage.VehiclePurchased)
            {
                SelectIHaveNotBoughtTheVehicleYet("I haven't bought this van yet");
            }
            else
            {
                SelectPurchaseMonth(vehicleUsage.DateOfPurchase.Value.ToString("MMMM"));
                SelectPurchaseYear(vehicleUsage.DateOfPurchase.Value.ToString("yyyy"));
            }
            SelectUseOfVehicleForLC(vehicleUsage.UseVehicleForDescription);
            if (vehicleUsage.UseVehicleForDescription.Trim().ToUpper().Equals(Constants.BusinessUse))
            {
                SelectTypeOfBusinessUse(vehicleUsage.TypeOfBusinessUse);
                if (_qsTestEnabled)
                {
                    SetAnnualBusinessMileage(vehicleUsage.QS_BusinessAnnualMileage.ToString());
                }
                SelectWhatTypeOfCompanyUsesTheVan(vehicleUsage.TypeOfCompanyUsesTheVan);
                SelectPublicLiabilityHeld(YesNoMap[vehicleUsage.PublicLiability]);
                SelectTradeAssociationMember(YesNoMap[vehicleUsage.TradeAssociationMember]);
                if (vehicleUsage.TradeAssociationMember)
                {
                    SelectTradeAssociationName(vehicleUsage.TradeAssociationName);
                }
                FillInHowManyYearsCompanyRunning(vehicleUsage.HowManyYearsCompanyRunning);
                SelectYourVanHaveAnySigns(YesNoMap[vehicleUsage.AnySigns]);
                if(vehicleUsage.AnySigns)
                {
                    SelectSignsOnTheVehicle(vehicleUsage.SignsOnTheVehicle);
                }
                SelectItDisplayABadDriverHotline(YesNoMap[vehicleUsage.BadDriverHotline]);
            }

            SetAnnualMileage(vehicleUsage.AnnualMileage.ToString());
            var qsOverNightParking = vehicleUsage.QS_OvernightParkingDescription;
            Thread.Sleep(250);
            SelectVehicleKeptOverNightLightCommercial(vehicleUsage.QS_OvernightParkingDescription);
            Thread.Sleep(250);
            if (qsOverNightParking.Trim().ToUpper().Equals(Constants.OtherOverNightParking))
            {
                SelectWhereElseDoYouKeepTheCar(vehicleUsage.WhereElseDoYouKeepTheCarDescription);
            }
            var selectVehicleKeptElsewhere = vehicleUsage.KeptElsewhere;
            SelectVehicleKeptElsewhere(selectVehicleKeptElsewhere);
            if (selectVehicleKeptElsewhere.Trim().ToUpper().Equals(Constants.KeptElseWhere))
            {
                FillinVehicleKeptElsewhereAddress(vehicleUsage.KeptElsewhereHouseNumber, vehicleUsage.KeptElsewherePostcode);
            }
            //Do you use any other vehicles? checkbox (YES / NO)
            var yesNoUseOfOtherVehicle = YesNoMap[vehicleUsage.UseOfAnyOtherVehicle];
            SelectUseOfAnyOtherVehicle(yesNoUseOfOtherVehicle);
            //Do you use any other vehicles? (if YES, select dropdown value)
            if (yesNoUseOfOtherVehicle.Trim().ToUpper().Equals(Constants.Yes))
            {
                SelectUseOfAnyOtherVehicleDescription(vehicleUsage.UseOfAnyOtherVehicleDescription);
            }
            SelectAnyDangerousGoods(YesNoMap[vehicleUsage.AnyDangerousGoods]);
            return this;
        }
        public AboutYourVehiclePage FillPrivateCarVehicleUsage(VehicleUsage vehicleUsage)
        {
            WaitUntil("ABY_VU_Next");
            if (vehicleUsage.VehiclePurchased)
            {
                SelectPurchaseMonth(vehicleUsage.DateOfPurchase.Value.ToString("MMMM"));
                SelectPurchaseYear(vehicleUsage.DateOfPurchase.Value.ToString("yyyy"));
            }
            else
            {
                SelectIHaveNotBoughtTheVehicleYet("I haven't bought this car yet");
            }
            SelectUseOfVehicle(vehicleUsage.UseVehicleForDescription);
            if (vehicleUsage.UseVehicleForDescription.Trim().ToUpper().Equals(Constants.BusinessUse))
            {
                SelectWhoUsesForBusinessUse(vehicleUsage.QS_WhoUsesForBusinessUseDescription);
                if (_qsTestEnabled)
                {
                    SetAnnualBusinessMileage(vehicleUsage.QS_BusinessAnnualMileage.ToString());
                }
            }
            
            var qsOverNightParking = vehicleUsage.QS_OvernightParkingDescription;
            SelectVehicleKeptOverNight(vehicleUsage.QS_OvernightParkingDescription);
            Thread.Sleep(250);
            if (qsOverNightParking.Trim().ToUpper().Equals(Constants.OtherOverNightParking))
            {
                SelectWhereElseDoYouKeepTheCar(vehicleUsage.WhereElseDoYouKeepTheCarDescription);
            }
            var selectVehicleKeptElsewhere = vehicleUsage.KeptElsewhere;
            SelectVehicleKeptElsewhere(selectVehicleKeptElsewhere);
            if (selectVehicleKeptElsewhere.Trim().ToUpper().Equals(Constants.KeptElseWhere))
            {
                FillinVehicleKeptElsewhereAddress(vehicleUsage.KeptElsewhereHouseNumber, vehicleUsage.KeptElsewherePostcode);
            }

            SetAnnualMileage(vehicleUsage.AnnualMileage.ToString());
            SelectVehicleKeptDuringDay(vehicleUsage.VehicleKeptDuringDayDescription);
            //Do you use any other vehicles? checkbox (YES / NO)
            var yesNoUseOfOtherVehicle = YesNoMap[vehicleUsage.UseOfAnyOtherVehicle];
            SelectUseOfAnyOtherVehicle(yesNoUseOfOtherVehicle);
            //Do you use any other vehicles? (if YES, select dropdown value)
            if (yesNoUseOfOtherVehicle.Trim().ToUpper().Equals(Constants.Yes))
            {
                SelectUseOfAnyOtherVehicleDescription(vehicleUsage.UseOfAnyOtherVehicleDescription);
            }
            if (_qsTestEnabled)
            {
                SetHowManyVehiclesAreThereInYourHouse(QS_HowManyVehiclesAreThereMap[vehicleUsage.QS_HowManyVehicles]);
            }
            return this;
        }
        #endregion
        #region "Vehicle Usage Methods"

        private void SelectWhereElseDoYouKeepTheCar(string whereElse)
        {
            var id = "Q_VEHICLEUSAGE_KEPTDURINGNIGHT_OTHERPARKINGOPTIONS-button";
            var ulClassName = "Q_VEHICLEUSAGE_KEPTDURINGNIGHT_OTHERPARKINGOPTIONS";
            CustomSelectDropdownByText(id, ulClassName, whereElse);
        }

        private void SelectTradeAssociationName(string associationName)
        {
            var id = "Q_VEHICLEUSAGE_TRADEORGANISATIONNAMES-button";
            var ulClassName = "Q_VEHICLEUSAGE_TRADEORGANISATIONNAMES";
            CustomSelectDropdownByText(id, ulClassName, associationName);
        }

        private void FillInHowManyYearsCompanyRunning(string howManyYears)
        {
            FillIn("Q_VEHICLEUSAGE_YRSESTD", howManyYears);
        }
        private void SelectWhoUsesForBusinessUse(string whoUses)
        {
            var id = "Q_VEHICLEUSAGE_VEHICLEUSEBUSINESSTYPES-button";
            var ulClassName = "Q_VEHICLEUSAGE_VEHICLEUSEBUSINESSTYPES";
            CustomSelectDropdownByText(id, ulClassName, whoUses);
        }

        private void SelectTypeOfBusinessUse(string typeOfBusinessUse)
        {
            var id = "Q_VEHICLEUSAGE_VEHICLEUSEBUSINESSTYPES_LC-button";
            var ulClassName = "Q_VEHICLEUSAGE_VEHICLEUSEBUSINESSTYPES_LC";
            CustomSelectDropdownByText(id, ulClassName, typeOfBusinessUse);
        }

        private void SelectPurchaseMonth(string month)
        {
            var id = "Q_VEHICLE_PURCHASE_DATE_month-button";
            var ulClassName = "Q_VEHICLE_PURCHASE_DATE_month";
            CustomSelectDropdownByText(id, ulClassName, month);
       }
        private void SelectUseOfAnyOtherVehicleDescription(string description)
        {
             var id = "Q_VEHICLEUSAGE_USEOFANYOTHERVEHICLETYPES-button";
             var ulClassName = "Q_VEHICLEUSAGE_USEOFANYOTHERVEHICLETYPES";
             CustomSelectDropdownByText(id, ulClassName, description);
        }
        private void SelectPurchaseYear(string year)
        {
            var id = "Q_VEHICLE_PURCHASE_DATE_year-button";
            var ulClassName = "Q_VEHICLE_PURCHASE_DATE_year";
            CustomSelectDropdownByText(id, ulClassName, year);
        }

        private void SetAnnualMileage(string value)
        {
            FillIn("Q_VEHICLEUSAGE_ANNUALMILEAGE", value);
        }

        private void SetAnnualBusinessMileage(string value)
        {
            FillIn("Q_VEHICLEUSAGE_BUSINESSMILES", value);
        }

        private void SelectSignsOnTheVehicle(string sign)
        {
            var id = "Q_VEHICLEUSAGE_CARRYSINEAGE-button";
            var ulClassName = "Q_VEHICLEUSAGE_CARRYSINEAGE";
            CustomSelectDropdownByText(id, ulClassName, sign);
        }

        private void SelectVehicleKeptDuringDay(string text)
        {
            CustomRadioIconListClick("VehicleKeptDuringDayQuestion", text);
        }

        private void SelectVehicleKeptOverNight(string text)
        {
            CustomRadioIconListClick("VehicleKeptDuringNightQuestion", text);
        }
        private void SelectVehicleKeptOverNightLightCommercial(string text)
        {
            CustomRadioIconListClick("LCVehicleKeptDuringNightQuestion", text);
        }

        private void SelectVehicleKeptElsewhere(string text)
        {
            CustomRadioIconListClick("AddressWhereVehicleKeptQuestion", text);
        }

        private void FillinVehicleKeptElsewhereAddress(string elseWhereHouseNumber, string elseWherePostcode)
        {
            FillInElseWhereHouseNumber(elseWhereHouseNumber);
            FillInElseWherePostCode(elseWherePostcode);
            ClickFindAddressButton();
        }

        private void FillInElseWhereHouseNumber(string houseNumber)
        {
            FillIn("HouseNumberTextBox", houseNumber);
        }
        private void FillInElseWherePostCode(string postCode)
        {
            FillIn("Q_VU_Address_postcode", postCode);
        }
        private void ClickFindAddressButton()
        {
            FindAddressButton.Click();
            Thread.Sleep(250);
        }









        private void SelectAnyDangerousGoods(string yesNo)
        {
            CustomRadioIconListClick("DangerousGoodsQuestion", yesNo);
        }

        private void SelectPublicLiabilityHeld(string yesNo)
        {
            CustomRadioIconListClick("PublicLiabilityQuestion", yesNo);
        }
        private void SelectTradeAssociationMember(string yesNo)
        {
            CustomRadioIconListClick("MemberOfTradeOrganisationQuestion", yesNo);
        }
        private void SelectYourVanHaveAnySigns(string yesNo)
        {
            CustomRadioIconListClick("HaveAnySignsQuestion", yesNo);            
        }
        private void SelectIHaveNotBoughtTheVehicleYet(string yesNo)
        {
            CustomRadioIconListClick("PurchaseDateQuestion", yesNo);
        }
        private void SelectItDisplayABadDriverHotline(string yesNo)
        {
            CustomRadioIconListClick("BadDriverHotlineDisplayQuestion",yesNo);
        }
        private void SelectUseOfAnyOtherVehicle(string yesNo)
        {
            CustomRadioIconListClick("UseOfAnyOtherVehiclesQuestion", yesNo);
        }
        private void SelectWhatTypeOfCompanyUsesTheVan(string whoUses)
        {
            CustomRadioIconListClick("CompanyTypeQuestion", whoUses);
        }

        private void SelectUseOfVehicle(string text)
        {
            CustomRadioIconListClick("VehicleUsageQuestion", text);
        }

        private void SelectUseOfVehicleForLC(string text)
        {
            CustomRadioIconListClick("LCVehicleUsageQuestion", text);
        }

        private void SetHowManyVehiclesAreThereInYourHouse(string value)
        {
            SetSliderValue("VehiclesInHousehold", value);
        }

            
        #endregion


    }
}
