using GCS.Models.Tree;
using System.Linq;
using System.Net.Http.Formatting;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace GCS.Controllers.Trees {
    [PluginController("GCS")]
    [Tree("GCS", "GCSTree", "Google Custom Search", iconClosed: "icon-binoculars")]
    public class GCSTreeController : TreeController {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings) {
            var nodes = new TreeNodeCollection();
            if (id == "-1") {
                nodes.Add(this.CreateTreeNode("dashboard", id, queryStrings, "Settings", "icon-umb-settings", false, "/GCS/GCS/Edit/settings/1"));
                if (Umbraco.UmbracoContext.Security.CurrentUser.Groups.Where(g => g.Alias == "administrator") != null) {
                    nodes.Add(this.CreateTreeNode("dashboard", id, queryStrings, "Development", "icon-terminal", false, "/GCS/GCS/Development/development/1"));
                }
                nodes.Add(this.CreateTreeNode("dashboard", id, queryStrings, "Statistics", "icon-bars", false, "/GCS/GCS/Stats/statistics/1"));
                if (Umbraco.UmbracoContext.Security.CurrentUser.Groups.Where(g => g.Alias == "administrator") != null) {
                    nodes.Add(this.CreateTreeNode("dashboard", id, queryStrings, "Readme", "icon-book-alt-2", false, "/GCS/GCS/ReadMe/readme/1"));
                }
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings) {
            MenuItemCollection menu = new MenuItemCollection();
            menu.DefaultMenuAlias = "gcs";
            menu.Items.Add<PropertiesActionModel>("gcs");
            return menu;
        }

    }
}