using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class ConfigDetailsMother
    {
        public string ProductClass { get; set; }

        public ConfigDetails Build()
        {
            ProductClass = "PC";
            return new ConfigDetails(ProductClass);
        }
        public ConfigDetails BuildFromCSV(DataRecord data)
        {
            ProductClass = data["PRODUCTCLASS"];
            return new ConfigDetails(ProductClass);
        }

    }
  
}