using System;
using System.Collections.Generic;

namespace GCS.Models.Filters {
    public class FileTypeFilter {
        public Dictionary<String, String> FileTypes = new Dictionary<String, String>() {
        { "", "" },
        { "swf", "Adobe Flash (.swf)" },
        { "pdf", "PDF (.pdf)" },
        { "ps", "Adobe PostScript (.ps)" },
        { "dwf", "Autodesk Design Web Format (.dwf)" },
        { "kml", "Google Earth (.kml)" },
        { "gpx", "GPS eXchange Format (.gpx)" },
        { "hwp", "Hancom Hanword (.hwp)" },
        { "xls, xlsx", "Microsoft Excel (.xls, .xlsx)" },
        { "ppt, pptx", "Microsoft PowerPoint (.ppt, .pptx)" },
        { "doc, docx", "Microsoft Word (.doc, .docx)" },
        { "odp", "OpenOffice presentation (.odp)" },
        { "ods", "OpenOffice spreadsheet (.ods)" },
        { "odt", "OpenOffice text (.odt)" },
        { "rtf", "Rich Text Format (.rtf)" },
        { "svg", "Scalable Vector Graphic (.svg)" },
        { "tex", "TeX/LaTeX (.tex)" },
        { "txt", "Text (.txt)" },
        { "wml, wap", "Wireless Markup Language (.wml, .wap)" },
        { "xml", "XML (.xml)" }
        };
    }
}