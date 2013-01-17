namespace Journey.Test.Support.Model
{
    public class ConfigDetails
    {
        public string ProductClass { get; set; }
        
        public ConfigDetails(string productClass)
        {
            ProductClass = productClass;
        }
    }
}