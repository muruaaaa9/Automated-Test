using System.Collections.Generic;
using System.Linq;

namespace Journey.Test.Support.Model
{
    public class TitleGender
    {
        private static readonly Dictionary<string, TitleGender> TitleGenderMap = new Dictionary<string, TitleGender>
                                                                                     {
                                                                                         {"MR", new TitleGender("M","Mr","Mr")},
                                                                                         {"MRS", new TitleGender("F","Mrs","Mrs")},
                                                                                         {"MS", new TitleGender("F","Ms", "Ms")},
                                                                                         {"MISS", new TitleGender("F","Miss", "Miss")},
                                                                                         {"DRM", new TitleGender("M","Dr - Male", "Dr")},
                                                                                         {"DRF", new TitleGender("F","Dr - Female","Dr")}
                                                                                     };

        private readonly string _gender;
        private readonly string _description;
        private readonly string _displayTitle;

        public string DisplayTitle
        {
            get { return _displayTitle; }
        }

        private string Gender
        {
            get { return _gender; }
        }

        private string Description
        {
            get { return _description; }
        }

        public static string GetDisplayTitle(string titleCode)
        {
            if (string.IsNullOrEmpty(titleCode))
                return string.Empty;
            return TitleGenderMap[titleCode].DisplayTitle;
        }

        private TitleGender(string gender, string description, string displayTitle)
        {
            _gender = gender;
            _description = description;
            _displayTitle = displayTitle;
        }

        public static IList<ReferenceDataItem> GetTitleTypes()
        {
            return TitleGenderMap.Select(x => new ReferenceDataItem(x.Key, x.Value.Description)).ToList();
        }
        public static Dictionary<string, string> GetDisplayTitles()
        {
            return TitleGenderMap.ToDictionary(item => item.Key, item => item.Value.DisplayTitle);
        }

        public static string GetGender(string titleCode)
        {
            return TitleGenderMap[titleCode].Gender;
        }
    }
}