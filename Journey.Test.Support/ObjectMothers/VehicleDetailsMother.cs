using System;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;


namespace Journey.Test.Support.ObjectMothers
{
    public class VehicleDetailsMother
    {
        public VehicleAdditionalDetails AdditionalDetails { get; set; }
        public string NumberOfSeats { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationYear { get; set; }
        public string AlarmType { get; set; }
        public string SteeringType { get; set; }
        public string CurrentValue { get; set; }
        public bool HasAlarm { get; set; }
        public bool HasTrackingDevice { get; set; }
        public bool HasVehicleModified { get; set; }
        public bool IsImported { get; set; }
        public string VanBodyType { get; set; }
        public bool KnownRegistrationNumber { get; set; }

        public VehicleDetails Build()
        {
            CurrentValue = "9477";
            SteeringType = "Right Hand";
            AlarmType = "None";
            RegistrationNumber = "A1";
            RegistrationYear = "2007";
            IsImported = false;
            NumberOfSeats = "4";
            HasAlarm = false;
            HasTrackingDevice = false;
            HasVehicleModified = false;
            VanBodyType = "Van";
            KnownRegistrationNumber = true;
            AdditionalDetails = new VehicleAdditionalDetailsMother().Build();
            return new VehicleDetails
                       {
                           NumberOfSeats = NumberOfSeats,
                           IsImported = IsImported,
                           RegistrationNumber = RegistrationNumber,
                           SteeringType = SteeringType,
                           HasAlarm = HasAlarm,
                           CurrentValue = CurrentValue,
                           AlarmType = AlarmType,
                           HasTrackingDevice = HasTrackingDevice,
                           HasVehicleModified = HasVehicleModified,
                           VanBodyType = VanBodyType,
                           KnownRegistrationNumber = KnownRegistrationNumber,
                           AdditionalDetails = AdditionalDetails,
                       };
        }

        public VehicleDetails BuildFromCSV(DataRecord data)
        {
            RegistrationNumber = data["VEHICLEREGISTRATIONNUMBER"];
            NumberOfSeats = data["HOWMANYSEATS"];
            CurrentValue = data["VEHICLEVALUE"];
            HasAlarm = Convert.ToBoolean(data["HASALARM"]);
            AlarmType = data["ALARMTYPE"];
            HasTrackingDevice = Convert.ToBoolean(data["HASTRACKINGDEVICE"]);
            SteeringType = data["STEERINGTYPE"];
            HasVehicleModified = Convert.ToBoolean(data["VEHICLEMODIFIED"]);
            VanBodyType = data["VAN_BODYTYPE"];
            IsImported = Convert.ToBoolean(data["ISIMPORTED"]);
            KnownRegistrationNumber = Convert.ToBoolean(data["KNOWREGISTRATION"]);
            AdditionalDetails = new VehicleAdditionalDetailsMother().BuildFromCSV(data);
            return new VehicleDetails
            {
                NumberOfSeats = NumberOfSeats,
                IsImported = IsImported,
                RegistrationNumber = RegistrationNumber,
                SteeringType = SteeringType,
                HasAlarm = HasAlarm,
                CurrentValue = CurrentValue,
                AlarmType = AlarmType,
                HasTrackingDevice = HasTrackingDevice,
                HasVehicleModified = HasVehicleModified,
                VanBodyType = VanBodyType,
                KnownRegistrationNumber = KnownRegistrationNumber,
                AdditionalDetails = AdditionalDetails,
            };
        }
    }
}