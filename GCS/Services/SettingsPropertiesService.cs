using GCS.Models.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GCS.Services {
    public class SettingsPropertiesService {

        private DocumentTypeService DocumentTypeService;

        public SettingsPropertiesService() {
            DocumentTypeService = new DocumentTypeService();
        }

        public List<ItemsObj> DocTypeList() {
            List<ItemsObj> objects = new List<ItemsObj>();
            int index = 1;
            foreach (String alias in DocumentTypeService.DocumentTypeAliasses()) {
                objects.Add(new ItemsObj() {
                    id = index++,
                    sortOrder = index++,
                    value = alias
                });
            }
            return objects;
        }

        public List<Object> GetDocTypeFilterConfig(string savedObj) {
            List<Object> list = new List<Object>();
            List<DocumentTypeFilterItem> setValues = new List<DocumentTypeFilterItem>();
            List<String> docTypes = DocumentTypeService.DocumentTypeAliasses(true);

            if (!String.IsNullOrEmpty(savedObj)) {
                setValues = JsonConvert.DeserializeObject<List<DocumentTypeFilterItem>>(savedObj);
            }

            foreach (var doctype in docTypes) {
                var setValue = setValues.FirstOrDefault(v => v.alias == doctype);

                list.Add(new {
                    label = doctype,
                    ischecked = setValue != null ? true : false,
                    text = setValue != null ? setValue.text : ""
                });
            }

            return list;
        }
    }

    public class ItemsObj {
        public int id { get; set; }
        public int sortOrder { get; set; }
        public String value { get; set; }
    }
}