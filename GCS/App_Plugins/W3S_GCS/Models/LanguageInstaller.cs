using System;
using System.IO;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;

namespace W3S_GCS.App_Plugins.GCS.Models {
    public class LanguageInstaller {
        private static bool _executed;
        public static void CheckAndInstallLanguageActions() {
            if (!_executed) {
                InstallLanguageKey("sections", "GCS", "GCS");
                _executed = true;
            }
        }

        private static bool KeyMissing(string area, string key) {
#pragma warning disable CS0618 // Type or member is obsolete
            return ui.GetText(area, key) == string.Format("[{0}]", key);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private static void InstallLanguageKey(string area, string key, string value) {
            if (KeyMissing(area, key)) {
                var directory = HttpContext.Current.Server.MapPath(FormatUrl("/config/lang"));
                var languageFiles = Directory.GetFiles(directory);

                foreach (var languagefile in languageFiles) {
                    try {
                        var langcode = "";
                        var lastIndex = (languagefile.Length - languagefile.LastIndexOf('\\')) - 1;

                        if (lastIndex == 6) {
                            langcode = languagefile.Substring(languagefile.Length - 6, 2).ToLower();
                        } else if (lastIndex == 9) {
                            langcode = languagefile.Substring(languagefile.Length - 9, 5).ToLower();
                        }

                        if (!String.IsNullOrEmpty(langcode)) {
                            UpdateActionsForLanguageFile(string.Format("{0}.xml", langcode), area, key, value);
                        }
                    } catch (Exception ex) {
                        //LogHelper.Error<LanguageInstaller>("Backend Error in language installer", ex);
                    }
                }
            }
        }

        private static void UpdateActionsForLanguageFile(string languageFile, string area, string key, string value) {
            var doc = XmlHelper.OpenAsXmlDocument(string.Format("{0}/config/lang/{1}", GlobalSettings.Path, languageFile));
            var actionNode = doc.SelectSingleNode(string.Format("//area[@alias='{0}']", area));

            if (actionNode != null) {
                var node = actionNode.AppendChild(doc.CreateElement("key"));
                if (node.Attributes != null) {
                    var att = node.Attributes.Append(doc.CreateAttribute("alias"));
                    att.InnerText = key;
                }
                node.InnerText = value;
            }

            doc.Save(HttpContext.Current.Server.MapPath(string.Format("{0}/config/lang/{1}", GlobalSettings.Path, languageFile)));
        }

        private static string FormatUrl(string url) {
            return VirtualPathUtility.ToAbsolute(GlobalSettings.Path + url);
        }

    }
}