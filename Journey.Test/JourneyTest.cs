using System.Collections;
using System.Configuration;
using System.IO;
using Journey.Test.Support;
using Journey.Test.Support.Model;
using Journey.Test.Support.ObjectMothers;
using Journey.Test.Support.Pages;
using Kent.Boogaart.KBCsv;
using NUnit.Framework;

namespace Journey.Test

{
    [TestFixture]
    public class JourneyTest : BaseJourneyTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        [TestCaseSource(typeof (RiskTestData), "TestCases")]
        public void QuestionSetJourneyTest(RiskTestData _risk)
        {
            var aboutYourVehiclePage = Browser.Current.GetPage<AboutYourVehiclePage>();
            aboutYourVehiclePage.ClickStartComparingButton();

            if (_risk.VehicleDetails.KnownRegistrationNumber)
                aboutYourVehiclePage.TypeRegistrationNumber(_risk.VehicleDetails).ClickFind();
            else
                aboutYourVehiclePage.FillManualLookupOnVehicleSelection(_risk.VehicleDetails).ClickOnThisIsMyVehicle(); 

            aboutYourVehiclePage.FillVehicleDetails(_risk.VehicleDetails, _risk.ConfigDetails).ClickNextOnVehicleDetails();
            if (_risk.ConfigDetails.ProductClass.Trim().ToUpper().Equals(Constants.PrivateCar))
            {
                aboutYourVehiclePage.FillPrivateCarVehicleUsage(_risk.VehicleUsage).ClickNextOnVehicleUsage();
            }
            else if (_risk.ConfigDetails.ProductClass.Trim().ToUpper().Equals(Constants.LightCommercial))
            {
                aboutYourVehiclePage.FillLightCommercialVehicleUsage(_risk.VehicleUsage).ClickNextOnVehicleUsage();
            }

            var aboutYouPage = Browser.Current.GetPage<AboutYouPage>();
            aboutYouPage.FillPersonalDetails(_risk.PersonalDetails).ClickNextOnPersonalDetails();
            aboutYouPage.FillClaims(_risk.Claims).FillConvictions(_risk.Convictions).FillNonMotoringConvictions(_risk.DrivingHistory).ClickNextOnClaimsAndConvictions();
            aboutYouPage.FillDrivingHistory(_risk.DrivingHistory).ClickNextOnDrivingHistory();
            aboutYouPage.FillAdditionalDrivers(_risk.AdditionalDrivers).ClickNextOnAdditionalDrivers();

            var aboutYourPolicyPage = Browser.Current.GetPage<AboutYourPolicyPage>();
            aboutYourPolicyPage.AssertPolicyDetailsDefaultsAreSelected().FillPolicyDetails(_risk.PolicyDetails).ClickNextOnPolicyDetails();
            aboutYourPolicyPage.FillContactDetails(_risk.ContactDetails).ClickGetQuotes();

            var pricePage = Browser.Current.GetPage<PricePage>();
            pricePage.AssertPricePage();
        }
    }


    public class RiskTestData
    {
        public VehicleAdditionalDetails AdditionalDetails { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
        public ConfigDetails ConfigDetails { get; set; }
        public VehicleUsage VehicleUsage { get; set; }
        public PersonDetails PersonalDetails { get; set; }
        public ClaimCollection Claims { get; set; }
        public ConvictionCollection Convictions { get; set; }
        public DrivingHistory DrivingHistory { get; set; }
        //public AdditionalDriver AdditionalDriver { get; set; }
        public AdditionalDriverCollection AdditionalDrivers { get; set; }
        public PolicyDetails PolicyDetails { get; set; }
        public ContactDetails ContactDetails { get; set; }


        
        public static IEnumerable TestCases
        {
            get
            {
                string testName;
                var cTestdataCsv = GetDatasheet();
                using (var reader = new CsvReader(cTestdataCsv))
                {
                    reader.ReadHeaderRecord();
                    foreach (DataRecord record in reader.DataRecords)
                    {
                        if (record["EXECUTION"].Trim().ToUpper().Equals("Y"))  // Needs to prepare testcase(s) based upon execution column
                        {
                            testName = GetTestName(record);
                            
                            yield return new TestCaseData(new RiskTestData
                                                              {
                                                                  ConfigDetails =
                                                                      new ConfigDetailsMother().BuildFromCSV(record),
                                                                  VehicleDetails =
                                                                      new VehicleDetailsMother().BuildFromCSV(record),
                                                                  VehicleUsage =
                                                                      new VehicleUsageMother().BuildFromCSV(record),
                                                                  PersonalDetails =
                                                                      new PersonalDetailsMother().BuildFromCSV(record),
                                                                  Claims = new ClaimMother().BuildFromCSV(record),
                                                                  Convictions =
                                                                      new ConvictionMother().BuildFromCSV(record),
                                                                  DrivingHistory =
                                                                      new DrivingHistoryMother().BuildFromCSV(record),
                                                                  AdditionalDrivers = 
                                                                      new AdditonalDriverMother().BuildFromCSV(record),
                                                                  PolicyDetails =
                                                                      new PolicyDetailsMother().BuildFromCSV(record),
                                                                  ContactDetails =
                                                                      new ContactDetailsMother().BuildFromCSV(record)
                                                              }).SetName(testName);
                        }
                    }
                    reader.Close();
                    
                }
            }
        }

        private static string GetTestName(DataRecord record)
        {
            var name = record["TESTID"];
            var proposer = record["TITLE"];
            var firstName = record["FIRSTNAME"];
            var surName = record["SURNAME"];
            string testName = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", "Test - ", name, " - ", proposer, " ", firstName, " ", surName);
            return testName;
        }

        private static string GetDatasheet()
        {
            //Read from config entry to get the path of the Datasheet
            var cTestdataCsv = ConfigurationManager.AppSettings["RiskDataCSVPath"];
            //Check for the datasheet exist in the path
            var fileName = "TestData.csv";
            if (!File.Exists(cTestdataCsv))
            {
                //If not exist, get the default datasheet from the deployed (debug/release folder of the project)
                cTestdataCsv = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            }
            return cTestdataCsv;
        }

    }
}
