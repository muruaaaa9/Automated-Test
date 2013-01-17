using System;

namespace Journey.Test.Support.Model
{
    public class VehicleUsage
    {
        public DateTime? DateOfPurchase { get; set; }
        public bool VehiclePurchased { get; set; }
        public string UseVehicleForDescription { get; set; }
        public int AnnualMileage { get; set; }
        public int QS_BusinessAnnualMileage { get; set; }
        public string VehicleKeptDuringDay { get; set; }
        public string VehicleKeptDuringDayDescription { get; set; }
        public string OvernightParking { get; set; }
        public string QS_OvernightParking { get; set; }
        public string QS_OvernightParkingDescription { get; set; }
        public string QS_WhoUsesForBusinessUse { get; set; }
        public string QS_WhoUsesForBusinessUseDescription { get; set; }
        public string QS_HowManyVehicles { get; set; }
        public string WhereElseDoYouKeepTheCar { get; set; }
        public string WhereElseDoYouKeepTheCarDescription { get; set; }
        public bool IsVehicleKeptAtHomeAtNight { get; set; }
        public bool UseOfAnyOtherVehicle { get; set; }
        public string UseOfAnyOtherVehicleDescription { get; set; }
        public Address OvernightAddress { get; set; }
        public bool AnyDangerousGoods { get; set; }
        public string TypeOfBusinessUse { get; set; }
        public string TypeOfCompanyUsesTheVan { get; set; }
        public bool PublicLiability { get; set; }
        public bool TradeAssociationMember { get; set; }
        public string TradeAssociationName { get; set; }
        public string HowManyYearsCompanyRunning { get; set; }
        public bool AnySigns { get; set; }
        public string SignsOnTheVehicle { get; set; }
        public bool BadDriverHotline { get; set; }
        public string KeptElsewhere { get; set; }
        public string KeptElsewhereHouseNumber { get; set; }
        public string KeptElsewherePostcode { get; set; }
    }
}