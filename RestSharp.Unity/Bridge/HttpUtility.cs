using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace System.Web
{
    internal static class HttpUtility
    {
        public static string UrlDecode(string value) => WebUtility.UrlDecode(value);
        public static string UrlEncode(string value) => WebUtility.UrlEncode(value);
        public static string UrlEncode(string value, Encoding encoding) => UrlEncode(value);

        public static string HtmlDecode(string value) => WebUtility.HtmlDecode(value);
        public static string HtmlEncode(string value) => WebUtility.HtmlEncode(value);
        
        public static NameValueCollection ParseQueryString(string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var result = new NameValueCollection();

            var queryIndex = query.IndexOf('?');
            if (queryIndex >= 0)
            {
                query = query.Substring(queryIndex + 1);
            }

            var args = query.Split('&');
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                var equalIndex = arg.IndexOf('=');
                if (equalIndex < 0)
                {
                    result.Add(arg, string.Empty);
                    continue;
                }

                result.Add(arg.Substring(0, equalIndex), UrlDecode(arg.Substring(equalIndex + 1)));
            }

            return result;
        }
    }
}
