using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PktViewModel
{
    public class HomeModel
    {
        public Int64 Document_TotalCount { get; set; }
        public string Document_TotalSize { get; set; }
        public Int64 Document_ActiveCount { get; set; }
        public Int64 Document_InactiveCount { get; set; }
        public List<FileStatModel> FileStat { get; set; }
    }

    public class FileStatModel
    {
        public string DocumentName { get; set; }
        public string FileCount { get; set; }
        public string FileSize { get; set; }
        public string Active { get; set; }
        public string Inactive { get; set; }

    }

}
