

using Journey.Test.Support.Model;

namespace Journey.Test.Support.ObjectMothers
{
    public class ClaimCollection : System.Collections.CollectionBase
    {
        public ClaimCollection()
        {}
        public int Add(Claim item)
        {
            return this.List.Add(item);
        }
        public Claim this[int index]
        {
            get { return (Claim)this.List[index]; }
            set { this.List[index] = value; }
        }
    }
}