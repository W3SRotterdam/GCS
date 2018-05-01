using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace W3S_GCS.Services {
    public class NodeService {

        UmbracoHelper uh = new UmbracoHelper(UmbracoContext.Current);

        public string GetPathByUdi(string udi) {
            var content = GetContent(udi);
            return content != null ? content.Url : "";
        }

        public string GetMediaPathByUdi(string udi) {
            var content = GetMediaContent(udi);
            return content != null ? content.Url : "";
        }

        public List<string> GetPathsByUdi(string input) {
            List<string> result = new List<string>();
            foreach (var udi in input.Split(',').ToList()) {
                var content = GetContent(udi);
                if (content != null) {
                    result.Add(content.Url);
                }
            }
            return result;
        }

        public List<string> GetPathsByUdi(List<string> udis) {
            List<string> result = new List<string>();
            foreach (var udi in udis) {
                var content = GetContent(udi);
                if (content != null) {
                    result.Add(content.Url);
                }
            }
            return result;
        }

        public List<string> GetAbsoluteURLByUdi(string udis) {
            List<string> result = new List<string>();
            foreach (var udi in udis.Split(',').ToList()) {
                var content = GetContent(udi);
                if (content != null) {
                    result.Add(content.UrlAbsolute());
                }
            }
            return result;
        }

        public String GetRedirectNodeURL(IPublishedContent node, string alias = "") {
            var redirectNode = node.FirstChild(alias);
            return redirectNode != null ? redirectNode.Url : "";
        }

        public CultureInfo GetCurrentCulture(IPublishedContent node) {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (node != null) {
                culture = node.GetCulture();
            }

            return culture;
        }

        public IDomain GetCurrentDomain(List<IDomain> domains, String URL) {
            IDomain domain = null;
            var parts = URL.Split('/').Where(x => !String.IsNullOrEmpty(x));
            int index = parts.Count();

            while (!String.IsNullOrEmpty(URL) && index != 0 && domain == null) {
                domain = domains.FirstOrDefault(d => d.DomainName.TrimEnd('/').ToLower() == URL.ToLower().TrimEnd('/'));
                string part = parts.ElementAt(index - 1);
                int partIndex = URL.LastIndexOf(part);

                if (partIndex > -1) {
                    URL = URL.Remove(partIndex, part.Length).Insert(partIndex, "");
                }

                //URL = URL.Replace(parts.ElementAt(index - 1), "");
                index--;
            }

            return domain;
        }

        private IPublishedContent GetContent(string udi) {
            return uh.TypedContent(GetIdForUdi(udi));
        }

        private IPublishedContent GetMediaContent(string udi) {
            return uh.TypedMedia(GetIdForUdi(udi));
        }

        private int GetIdForUdi(string udi) {
            return uh.GetIdForUdi(Udi.Parse(udi));
        }
    }
}