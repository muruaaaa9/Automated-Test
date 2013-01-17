using System;
using System.Linq;

namespace Journey.Test.Support.ObjectMothers
{
    public static class Extension
    {
        public static DateTime GetDateTime(string date)
        {
            var strings = date.Split('/');
            return new DateTime(Convert.ToInt32(strings[2]), Convert.ToInt32(strings[1]), Convert.ToInt32(strings[0]));
        }

        public static bool LicenceHeldYearsMatched(string licenceYearsHeldDescription)
        {
            string[] licenceHeldYearsArray =
                {
                    "Less than 1 Year", "At least 1 Year", "At least 2 Years", "At least 3 Years"
                };

            bool matched = licenceHeldYearsArray.Any(s => licenceYearsHeldDescription.Trim().Equals(s));
            return matched;
        }
    }
}