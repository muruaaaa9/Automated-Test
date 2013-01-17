using System;
using System.Linq;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class ConvictionMother
    {
        public bool HasMotorConvictions { get; set; }
        public int HowManyConvictions { get; set; }
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

        public ConvictionMother()
        {
           
        }

        public Conviction Build()
        {
            ConvictionCode = "DR30 Driving Then Failing To Supply A Specimen";
            ConvictionDate = new DateTime(2012, 10, 1);
            PenaltyPointsGiven = false;
            ResultInAFine = false;
            ConvictionResultInADrivingBan = false;
            WereYouBreathalysed = true;
            ConvictionsBreathalysedReading = Conviction.BreathalysedReading;
            return new Conviction()
            {
                ConvictionCode = ConvictionCode,
                ConvictionDate = ConvictionDate,
                PenaltyPointsGiven = PenaltyPointsGiven,
                ResultInAFine = ResultInAFine,
                ConvictionResultInADrivingBan = ConvictionResultInADrivingBan,
                WereYouBreathalysed = WereYouBreathalysed,
                ConvictionsBreathalysedReading = ConvictionsBreathalysedReading,
                NoOfPoints = NoOfPoints,
                FineAmount = FineAmount,
                BanLengnth = BanLengnth,
            };
        }

        public ConvictionCollection BuildFromCSV(DataRecord data)
        {
            HowManyConvictions = Convert.ToInt32(data["PROPOSERHOWMANYCONVICTIONS"]);
            var convictions = GetProposerConvictions(data);
            return convictions;
        }

        private Conviction BuildConviction(string convictionCode, string convictionDate, string penaltyPointsGiven, string noOfPoints, string resultInAFine, string fineAmount, string convictionResultInADrivingBan, string banLength, string wereYouBreathalysed, string convictionsBreathalysedReading)
        {
            
                string[] convictionCodeArray = { "DR10", "DR20", "DR30", "DR40", "DR50", "DR60", "CD40", "CD60", "CD70" };

                ConvictionCode = convictionCode;
                ConvictionDate = Extension.GetDateTime(convictionDate);
                PenaltyPointsGiven = Convert.ToBoolean(penaltyPointsGiven);
                if (PenaltyPointsGiven)
                {
                    NoOfPoints = noOfPoints;
                }
                ResultInAFine = Convert.ToBoolean(resultInAFine);
                if(ResultInAFine)
                {
                    FineAmount = fineAmount;
                }
                ConvictionResultInADrivingBan = Convert.ToBoolean(convictionResultInADrivingBan);
                if(ConvictionResultInADrivingBan)
                {
                    BanLengnth = banLength;
                }
                bool all = convictionCodeArray.Any(s => ConvictionCode.Substring(0, 4).Equals(s));
                if (all)
                {
                    WereYouBreathalysed = Convert.ToBoolean(wereYouBreathalysed);
                    if (WereYouBreathalysed)
                    {
                        ConvictionsBreathalysedReading = convictionsBreathalysedReading;
                    }
                }


            return new Conviction()
            {
                HasMotorConvictions = HasMotorConvictions,
                ConvictionCode = ConvictionCode,
                PenaltyPointsGiven = PenaltyPointsGiven,
                ResultInAFine = ResultInAFine,
                ConvictionResultInADrivingBan = ConvictionResultInADrivingBan,
                WereYouBreathalysed = WereYouBreathalysed,
                ConvictionsBreathalysedReading = ConvictionsBreathalysedReading,
                ConvictionDate = ConvictionDate,
                NoOfPoints = NoOfPoints,
                FineAmount = FineAmount,
                BanLengnth = BanLengnth,
            };
        }

        private ConvictionCollection GetProposerConvictions(DataRecord data)
        {
            ConvictionCollection convictions = new ConvictionCollection();
            for (int currentProposerConviction = 1; currentProposerConviction <= HowManyConvictions; currentProposerConviction ++ )
            {
                convictions.Add(BuildConviction(
                    data["PROPOSERCONV" + currentProposerConviction + "CONVICTIONCODE"], 
                    data["PROPOSERCONV" + currentProposerConviction + "CONVICTIONDATE"],
                    data["PROPOSERCONV" + currentProposerConviction + "POINTS"],
                    data["PROPOSERCONV" + currentProposerConviction + "NOOFPOINTS"],
                    data["PROPOSERCONV" + currentProposerConviction + "FINE"],
                    data["PROPOSERCONV" + currentProposerConviction + "FINEAMOUNT"],
                    data["PROPOSERCONV" + currentProposerConviction + "BAN"],
                    data["PROPOSERCONV" + currentProposerConviction + "BANLENGTH"],
                    data["PROPOSERCONV" + currentProposerConviction + "BREATHALYSED"],
                    data["PROPOSERCONV" + currentProposerConviction + "BRETHALYSERREADING"]));
            }
            return convictions;
        }

        public ConvictionCollection GetAdditionalDriverConvictions(DataRecord data, string currentDriver)
        {
            ConvictionCollection convictions = new ConvictionCollection();

            int AdditionalDriverHowManyConvictions = Convert.ToInt32(data["ADD" + currentDriver + "HOWMANYCONVICTIONS"]); ;
            for (int currentConviction = 1; currentConviction <= AdditionalDriverHowManyConvictions; currentConviction ++ )
            {
                convictions.Add(BuildConviction(
                    data["ADD" + currentDriver + "CONV" + currentConviction + "CONVICTIONCODE"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "CONVICTIONDATE"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "POINTS"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "NOOFPOINTS"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "FINE"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "FINEAMOUNT"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "BAN"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "BANLENGTH"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "BREATHALYSED"],
                    data["ADD" + currentDriver + "CONV" + currentConviction + "BRETHALYSERREADING"]));
            }
            return convictions;
        }
    }
}