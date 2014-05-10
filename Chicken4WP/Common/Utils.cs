using System;
using System.Text.RegularExpressions;

namespace Chicken4WP.Common
{
    public static class Utils
    {
        #region const
        private static Regex SourceRegex = new Regex(@".*>(?<url>[\s\S]+?)</a>");
        private static Regex SourceUrlRegex = new Regex(@"<a href=\""(?<link>[^\s>]+)\""");
        private static Regex UserNameRegex = new Regex(@"([^A-Za-z0-9_]|^)@(?<name>(_*[A-Za-z0-9]{1,15}_*)+)(?![A-Za-z0-9_@])");
        private static Regex HashTagRegex = new Regex(@"#(?<hashtag>\w+)(?!(\w+))");
        private const string USERNAMEPATTERN = @"(?<name>{0})(?![A-Za-z0-9_@])";
        private const string HASHTAGPATTERN = @"(?<hashtag>{0})(?!(\w+))";
        private const string URLPATTERN = @"(?<text>{0})(?![A-Za-z0-9-_/])";
        #endregion

        #region parse tweet string
        public static DateTime ParseToDateTime(string date)
        {
            var year = date.Substring(25, 5);
            var month = date.Substring(4, 3);
            var day = date.Substring(8, 2);
            var time = date.Substring(11, 9).Trim();

            var dateTime = string.Format("{0}/{1}/{2} {3}", month, day, year, time);
            var result = DateTime.Parse(dateTime).ToLocalTime();
            return result;
        }

        public static string ParseToSource(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return Const.DEFAULTSOURCE;
            }
            string result = SourceRegex.Match(source).Groups["url"].Value;
            if (string.IsNullOrEmpty(result))
            {
                return Const.DEFAULTSOURCE;
            }
            return result;
        }

        public static string ParseToSourceUrl(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return Const.DEFAULTSOURCEURL;
            }
            string result = SourceUrlRegex.Match(source).Groups["link"].Value;
            if (string.IsNullOrEmpty(result))
            {
                return Const.DEFAULTSOURCEURL;
            }
            return result;
        }
        #endregion
    }
}
