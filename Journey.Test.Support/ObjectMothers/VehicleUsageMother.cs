using System;
using System.Configuration;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;


namespace Journey.Test.Support.ObjectMothers
{
    public class VehicleUsageMother
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
        public string HowManyYearsCompanyRunning{ get; set; }
        public bool AnySigns { get; set; }
        public string SignsOnTheVehicle { get; set; }
        public bool BadDriverHotline { get; set; }
        public string KeptElsewhere { get; set; }
        public string KeptElsewhereHouseNumber { get; set; }
        public string KeptElsewherePostcode { get; set; }

        public VehicleUsageMother()
        {
           
        }

        public VehicleUsage Build()
        {
            DateOfPurchase = new DateTime(2010, 1, 1);
            VehiclePurchased = false;
            UseOfAnyOtherVehicle = false;
            //UseVehicleFor = "05NN";
            UseVehicleForDescription = "Social, Domestic and Pleasure (SDP) only";
            AnnualMileage = 4000;
            QS_BusinessAnnualMileage = 1000;
            VehicleKeptDuringDay = "VKD1";
            VehicleKeptDuringDayDescription = "At home";
            OvernightParking = "1";
            QS_OvernightParking = "1";
            QS_OvernightParkingDescription = "Locked garage";
            IsVehicleKeptAtHomeAtNight = true;
            QS_HowManyVehicles = "5";
            WhereElseDoYouKeepTheCar = "7";
            WhereElseDoYouKeepTheCarDescription = "Locked Compound";
            QS_WhoUsesForBusinessUseDescription = "";
            OvernightAddress = new AddressMother().Build();
            AnyDangerousGoods = false;
            TypeOfBusinessUse = "Carriage Of Own Goods, Not For Hire Or Reward";
            TypeOfCompanyUsesTheVan = "Sole Trader";
            PublicLiability = false;
            TradeAssociationMember = true;
            TradeAssociationName = "Federation of master builders";
            HowManyYearsCompanyRunning = "10";
            AnySigns = false;
            SignsOnTheVehicle = "Front";
            BadDriverHotline = false;
            return new VehicleUsage
                       {
                           DateOfPurchase = DateOfPurchase,
                           VehiclePurchased = VehiclePurchased,
                           UseOfAnyOtherVehicle = UseOfAnyOtherVehicle,
                           UseOfAnyOtherVehicleDescription = UseOfAnyOtherVehicleDescription,
                           AnnualMileage = AnnualMileage,
                           QS_BusinessAnnualMileage = QS_BusinessAnnualMileage,
                           VehicleKeptDuringDay = VehicleKeptDuringDay,
                           VehicleKeptDuringDayDescription = VehicleKeptDuringDayDescription,
                           OvernightParking = OvernightParking,
                           QS_OvernightParking = QS_OvernightParking,
                           QS_OvernightParkingDescription = QS_OvernightParkingDescription,
                           QS_WhoUsesForBusinessUse = QS_WhoUsesForBusinessUse,
                           QS_WhoUsesForBusinessUseDescription = QS_WhoUsesForBusinessUseDescription,
                           QS_HowManyVehicles = QS_HowManyVehicles,
                           WhereElseDoYouKeepTheCar = WhereElseDoYouKeepTheCar,
                           WhereElseDoYouKeepTheCarDescription = WhereElseDoYouKeepTheCarDescription,
                           IsVehicleKeptAtHomeAtNight = IsVehicleKeptAtHomeAtNight,
                           UseVehicleForDescription = UseVehicleForDescription,
                           OvernightAddress = OvernightAddress,
                           AnyDangerousGoods = AnyDangerousGoods,
                           TypeOfBusinessUse = TypeOfBusinessUse,
                           TypeOfCompanyUsesTheVan = TypeOfCompanyUsesTheVan,
                           PublicLiability = PublicLiability,
                           TradeAssociationMember = TradeAssociationMember,
                           TradeAssociationName = TradeAssociationName,
                           HowManyYearsCompanyRunning = HowManyYearsCompanyRunning,
                           AnySigns = AnySigns,
                           SignsOnTheVehicle = SignsOnTheVehicle,
                           BadDriverHotline = BadDriverHotline,
                           KeptElsewhere = KeptElsewhere,
                           KeptElsewhereHouseNumber = KeptElsewhereHouseNumber,
                           KeptElsewherePostcode = KeptElsewherePostcode,
                       };
        }

        public VehicleUsage BuildFromCSV(DataRecord data)
        {
            if (Convert.ToBoolean(data["VEHICLEPURCHASED"]))
            {
                DateOfPurchase = Extension.GetDateTime(data["PURCHASEDATE"]);    
            }
            VehiclePurchased = Convert.ToBoolean(data["VEHICLEPURCHASED"]);            
            UseVehicleForDescription = data["VEHICLEUSE"];
            AnnualMileage = Convert.ToInt32(data["MILEAGE"]);
            VehicleKeptDuringDayDescription = data["VEHICLEKEPTDAY"];
            QS_WhoUsesForBusinessUseDescription = data["WHOUSESFORBUSINESS"];
            if (ConfigurationManager.AppSettings["QSTestEnabled"].Equals("True", StringComparison.OrdinalIgnoreCase))
            {
                QS_OvernightParkingDescription = data["QS_VEHICLEKEPTNIGHT"];
                if (data["QS_BUSINESSMILEAGE"].Trim() != "")
                {
                    QS_BusinessAnnualMileage = Convert.ToInt32(data["QS_BUSINESSMILEAGE"]);
                }
                QS_HowManyVehicles = data["QS_VEHICLESINHOUSE"];
            }
            else
            {
                QS_OvernightParkingDescription = data["VEHICLEKEPTNIGHT"];
            }
            
            WhereElseDoYouKeepTheCarDescription = data["WHEREELSECARKEPT"];
            UseOfAnyOtherVehicle = data["PROPOSERUSEOFOTHERVEHICLES"] != "No";
            if (UseOfAnyOtherVehicle)
            {
                UseOfAnyOtherVehicleDescription = data["PROPOSERUSEOFOTHERVEHICLES"];
            }
            if (data["VAN_DANGEROUSGOODS"] != "")
            {
                AnyDangerousGoods = Convert.ToBoolean(data["VAN_DANGEROUSGOODS"]);
            }
            TypeOfBusinessUse = data["VAN_TYPEOFBUSINESS"];
            TypeOfCompanyUsesTheVan = data["VAN_TYPEOFCOMPANY"];
            if (data["VAN_PUBLICLIABILITY"] != "")
            {
                PublicLiability = Convert.ToBoolean(data["VAN_PUBLICLIABILITY"]);
            }
            if (data["VAN_TRADEASSOCIATION"] != "")
            {
                TradeAssociationMember = Convert.ToBoolean(data["VAN_TRADEASSOCIATION"]);
            }
            TradeAssociationName = data["VAN_TRADEASSOCIATIONNAME"];
            HowManyYearsCompanyRunning = data["VAN_YEARSCOMPANYRUNNING"];
            if (data["VAN_ANYSIGNS"] != "")
            {
                AnySigns = Convert.ToBoolean(data["VAN_ANYSIGNS"]);
            }
            SignsOnTheVehicle = data["VAN_SIGNSONTHEVEHICLE"];
            if (data["VAN_BADDRIVERHOTLINE"] != "")
            {
                BadDriverHotline = Convert.ToBoolean(data["VAN_BADDRIVERHOTLINE"]);
            }
            
            KeptElsewhere = data["KEPTELSEWHERE"];
            if (KeptElsewhere.ToUpper().Trim().Equals(Constants.KeptElseWhere))
            {
                KeptElsewhereHouseNumber = data["KEPTELSEWHEREHOUSENO"];
                KeptElsewherePostcode = data["KEPTELSEWHEREPOSTCODE"];
            }
            return new VehicleUsage
            {
                DateOfPurchase = DateOfPurchase,
                VehiclePurchased = VehiclePurchased,
                UseOfAnyOtherVehicle = UseOfAnyOtherVehicle,
                UseOfAnyOtherVehicleDescription = UseOfAnyOtherVehicleDescription,
                AnnualMileage = AnnualMileage,
                QS_BusinessAnnualMileage = QS_BusinessAnnualMileage,
                VehicleKeptDuringDay = VehicleKeptDuringDay,
                VehicleKeptDuringDayDescription = VehicleKeptDuringDayDescription,
                OvernightParking = OvernightParking,
                QS_OvernightParking = QS_OvernightParking,
                QS_OvernightParkingDescription = QS_OvernightParkingDescription,
                QS_WhoUsesForBusinessUse = QS_WhoUsesForBusinessUse,
                QS_WhoUsesForBusinessUseDescription = QS_WhoUsesForBusinessUseDescription,
                QS_HowManyVehicles = QS_HowManyVehicles,
                WhereElseDoYouKeepTheCar = WhereElseDoYouKeepTheCar,
                WhereElseDoYouKeepTheCarDescription = WhereElseDoYouKeepTheCarDescription,
                IsVehicleKeptAtHomeAtNight = IsVehicleKeptAtHomeAtNight,
                UseVehicleForDescription = UseVehicleForDescription,
                OvernightAddress = OvernightAddress,
                AnyDangerousGoods = AnyDangerousGoods,
                TypeOfBusinessUse = TypeOfBusinessUse,
                TypeOfCompanyUsesTheVan = TypeOfCompanyUsesTheVan,
                PublicLiability = PublicLiability,
                TradeAssociationMember = TradeAssociationMember,
                TradeAssociationName = TradeAssociationName,
                HowManyYearsCompanyRunning = HowManyYearsCompanyRunning ,
                AnySigns = AnySigns,
                SignsOnTheVehicle = SignsOnTheVehicle,
                BadDriverHotline = BadDriverHotline,
                KeptElsewhere = KeptElsewhere,
                KeptElsewhereHouseNumber = KeptElsewhereHouseNumber,
                KeptElsewherePostcode = KeptElsewherePostcode,
            };
        }

 
    }
}