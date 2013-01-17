using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class VehicleAdditionalDetailsMother
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string RegistrationYearAndLetter { get; set; }
        public string NumberOfDoors { get; set; }
        public string Transmission { get; set; }
        public string VehicleDescription { get; set; }

        public VehicleAdditionalDetailsMother()
        {

        }

        public VehicleAdditionalDetails Build()
        {
            Manufacturer = "Mini";
            Model = "Cooper";
            RegistrationYearAndLetter = "2009 (58, 09, 59)";
            NumberOfDoors = "3DR";
            Transmission = "Auto";
            VehicleDescription = "Camden D (110) [2009-2010]";
            return new VehicleAdditionalDetails(Manufacturer, Model, RegistrationYearAndLetter, NumberOfDoors, Transmission, VehicleDescription);
        }

        public VehicleAdditionalDetails BuildFromCSV(DataRecord data)
        {
            Manufacturer = data["MANUFACTURER"];
            Model = data["MODEL"];
            RegistrationYearAndLetter = data["REGYEAR"];
            NumberOfDoors = data["NOOFDOORS"];
            Transmission = data["TRANSMISSION"];
            VehicleDescription = data["VEHICLEDESCRIPTION"];
            return new VehicleAdditionalDetails(Manufacturer, Model, RegistrationYearAndLetter, NumberOfDoors, Transmission, VehicleDescription);
        }
    }
}