namespace Journey.Test.Support.Model
{
    public class VehicleDetails
    {
        public VehicleAdditionalDetails AdditionalDetails { get; set; }
        public string NumberOfSeats { get; set; }
        public string RegistrationNumber { get; set; }
        public string AlarmType { get; set; }
        public string SteeringType { get; set; }
        public string CurrentValue { get; set; }
        public string VanBodyType { get; set; }
        public bool HasAlarm { get; set; }
        public bool HasTrackingDevice { get; set; }
        public bool HasVehicleModified { get; set; }
        public bool IsImported { get; set; }
        public bool KnownRegistrationNumber { get; set; }
    }
}
