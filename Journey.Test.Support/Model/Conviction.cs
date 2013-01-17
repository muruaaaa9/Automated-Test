using System;

namespace Journey.Test.Support.Model
{
    public class Conviction
    {
        public const string BreathalysedReading = "450";

        public bool HasMotorConvictions { get; set; }

        public string ConvictionCode { get; set; }
        public bool PenaltyPointsGiven { get; set; }
        public string NoOfPoints { get; set; }
        public bool ResultInAFine { get; set; }
        public string FineAmount { get; set; }
        public bool ConvictionResultInADrivingBan { get; set; }
        public string BanLengnth { get; set; }
        public bool WereYouBreathalysed { get; set; }
        public string ConvictionsBreathalysedReading { get; set; }
        public DateTime? ConvictionDate { get; set; }
    }
}