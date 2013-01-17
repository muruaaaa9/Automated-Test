namespace Journey.Test.Support.Model
{
    public class ReferenceDataItem
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public ReferenceDataItem()
        {
        }

        public ReferenceDataItem(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public bool Equals(ReferenceDataItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Code, Code) && Equals(other.Description, Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ReferenceDataItem)) return false;
            return Equals((ReferenceDataItem)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? Code.GetHashCode() : 0) * 397) ^ (Description != null ? Description.GetHashCode() : 0);
            }
        }
    }
}