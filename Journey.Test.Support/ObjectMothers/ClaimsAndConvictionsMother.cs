using Journey.Test.Support.Model;

namespace Journey.Test.Support.ObjectMothers
{
    public class ClaimsAndConvictionsMother
    {
        public ClaimsAndConvictions Build()
        {
            return new ClaimsAndConvictions(false, false, false);
        }
    }
}