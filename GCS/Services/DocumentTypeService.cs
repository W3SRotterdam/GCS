using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace W3S_GCS.Services {
    public class DocumentTypeService {

        private UmbracoHelper uh = new UmbracoHelper(UmbracoContext.Current);

        public List<String> DocumentTypeAliasses(Boolean requireDescendants = false) {
            List<String> aliasses = new List<String>();

            foreach (IPublishedContent rootNode in uh.TypedContentAtRoot()) {
                aliasses.AddRange(processAliasses(rootNode, requireDescendants));
            }

            return aliasses.Distinct().ToList();
        }

        private List<String> processAliasses(IPublishedContent startNode, Boolean requireDescendants = false) {
            List<String> list = new List<String>();

            list.Add(startNode.DocumentTypeAlias);

            List<IPublishedContent> desc = requireDescendants ? startNode.Descendants().Where(p => p.Children.Count() > 0 && p.Children.FirstOrDefault().TemplateId > 0).ToList() : startNode.Descendants().ToList();

            foreach (IPublishedContent node in desc) {
                list.Add(node.DocumentTypeAlias);
                processAliasses(node);
            }

            return list;
        }
    }
}