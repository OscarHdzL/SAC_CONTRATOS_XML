using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Modelos
{
    public class DocumentsFileManager
    {
        public String Token { get; set; }
        public int versions_consecutive { get; set; }
        public String versions_description { get; set; }
        public String versions_author { get; set; }
        public String versions_title { get; set; }
        public int Alert { get; set; }

    }

    public class DocumentsFileManagerVersion 
    {
        public String Token { get; set; }
        public int versions_consecutive { get; set; }
        public String versions_description { get; set; }
        public String versions_author { get; set; }
        public String versions_title { get; set; }
        public String Alert_description { get; set; }
        public int Alert { get; set; }

    }
}
