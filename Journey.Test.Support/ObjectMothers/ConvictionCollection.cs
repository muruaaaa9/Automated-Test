
using Journey.Test.Support.Model;

namespace Journey.Test.Support.ObjectMothers
{
    public class ConvictionCollection : System.Collections.CollectionBase
    {
        public ConvictionCollection()
        { }
        public int Add(Conviction item)
        {
            return this.List.Add(item);
        }
        public Conviction this[int index]
        {
            get { return (Conviction)this.List[index]; }
            set { this.List[index] = value; }
        }
    }
}