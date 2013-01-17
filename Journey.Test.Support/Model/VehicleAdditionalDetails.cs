namespace Journey.Test.Support.Model
{
    public class VehicleAdditionalDetails
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string RegistrationYearAndLetter { get; set; }
        public string NumberOfDoors { get; set; }
        public string Transmission { get; set; }
        public string VehicleDescription { get; set; }

        public VehicleAdditionalDetails(string manufacturer, string model, string regYearAndLetter, string numberOfDoors, string transmission, string description)
        {
            Manufacturer = manufacturer;
            Model = model;
            RegistrationYearAndLetter = regYearAndLetter;
            NumberOfDoors = numberOfDoors;
            Transmission = transmission;
            VehicleDescription = description;
        }

        public VehicleAdditionalDetails()
        {
        }
    }
}