
using Journey.Test.Support.Model;

namespace Journey.Test.Support.ObjectMothers
{
    public class AdditionalDriverCollection : System.Collections.CollectionBase
    {
        public AdditionalDriverCollection()
        { }
        public int Add(AdditionalDriver item)
        {
            return this.List.Add(item);
        }
        public AdditionalDriver this[int index]
        {
            get { return (AdditionalDriver)this.List[index]; }
            set { this.List[index] = value; }
        }
    }
}
