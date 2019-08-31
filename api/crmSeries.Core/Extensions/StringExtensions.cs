using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace crmSeries.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SplitWords(this string s)
        {
            return Regex.Replace(s, "([a-z])([A-Z])", "$1 $2");
        }

        public static string FormatPhoneNumber(this string s)
        {
            if (s == null)
            {
                return null;
            }

            var number = new string(s.Where(char.IsDigit).ToArray());
            return Regex.Replace(number, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
        }

        public static string Truncate(this string s, int maxLength)
        {
            if (maxLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            return s?.Substring(0, Math.Min(s.Length, maxLength));
        }

        public static string NormalizeNewLines(this string s)
        {
            const string pattern = @"\r\n|\r|\n";

            return Regex.Replace(s, pattern, Environment.NewLine);
        }

        public static string ToCamel(this string value)
        {
            if (value.Length == 0)
                return value;
            if (value.Length == 1)
                return value.ToLower();
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }

        public static string Repeat(this string source, int maxLength)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = maxLength / source.Length;
            if (maxLength % source.Length > 0)
                ++num;
            for (int index = 0; index < num; ++index)
                stringBuilder.Append(source);
            return stringBuilder.ToString(0, maxLength);
        }

        public static string GetMimeType(this string fileExt)
        {
            var ext = fileExt?.ToLower();

            switch (ext)
            {
                case ".doc":
                    return "application/msword";

                case ".pdf":
                    return "application/pdf";
                    
                case ".zip":
                    return "application/zip";
                case ".bmp":
                    return "image/bmp";

                case ".gif":
                    return "image/gif";

                case ".jpg":
                case ".jpeg":
                case ".jpe":
                    return "image/jpeg";

                case ".png":
                    return "image/png";

                case ".tiff":
                case ".tif":
                    return "image/tiff";

                case ".htm":
                case ".html":
                    return "text/html";

                case ".txt":
                    return "text/plain";

                case ".rtf":
                    return "text/richtext";

                case ".mpg":
                case ".mpeg":
                case ".mpe":
                    return "video/mpeg";

                case ".viv":
                case ".vivo":
                    return "video/vnd.vivo";

                case ".qt":
                case ".mov":
                    return "video/quicktime";

                case ".avi":
                    return "video/x-msvideo";

                default:
                    return "application/octet-stream";
            }
        }
    }
}
