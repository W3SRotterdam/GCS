using System;
using System.Linq;

namespace GCS.Services {
    public class QueryParamsServices {
        public String FormatQueryParameters(String url, String[] keys, String[] values) {
            var returnString = "";
            var i = 0;

            foreach (String key in keys) {
                returnString += "&" + key + "=" + values.ElementAt(i);
                i++;
            }

            return returnString;
        }
    }
}