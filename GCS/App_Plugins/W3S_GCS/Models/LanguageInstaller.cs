using System;
using System.IO;
using System.Web;
using umbraco;
using Umbraco.Core;
using Umbraco.Core.Logging;

namespace W3S_GCS.App_Plugins.GCS.Models {
    public class LanguageInstaller {
        private static bool _executed;

        /// <summary>
        /// We need to add the text label on the actions otherwise they don;t appear on the context menu,
        /// Check each label and if not in the Umbraco langua file, add it to the actions node
        /// </summary>
        public static void CheckAndInstallLanguageActions() {
            if (!_executed) {
                InstallLanguageKey("sections", "GCS", "GCS");
                //InstallLanguageKey(Constants.LanguageFileAreas.Actions, Constants.LanguageFileKeys.MediaSecurityMenuAction, Constants.TextResources.MediaSecurityMenuAction);
                _executed = true;
            }
        }

        private static bool KeyMissing(string area, string key) {
#pragma warning disable CS0618 // Type or member is obsolete
            return ui.GetText(area, key) == string.Format("[{0}]", key);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Loop through the language config folder and add language nodes to the language files
        /// If the language is not in our language file install the english variant.
        /// </summary>
        private static void InstallLanguageKey(string area, string key, string value) {
            if (KeyMissing(area, key)) {
                var directory = HttpContext.Current.Server.MapPath(FormatUrl("/config/lang"));
                var languageFiles = Directory.GetFiles(directory);

                foreach (var languagefile in languageFiles) {
                    try {
                        //Strip 2digit langcode from filename
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
                        LogHelper.Error<LanguageInstaller>("Backend Error in language installer", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Update a language file withe the language xml
        /// </summary>
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

        /// <summary>
        /// Returns the url with the correct Umbraco folder
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static string FormatUrl(string url) {
            return VirtualPathUtility.ToAbsolute(GlobalSettings.Path + url);
        }

    }
}