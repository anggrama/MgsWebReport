using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace DataStruct
{
    public class ResultViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Type { get; set; }
        public DataTable Data { get; set; }
    }

    public class ResultDetailViewModel
    {
        public DataTable HeaderData { get; set; }
        public DataTable ResultData { get; set; }
        
    }

    public class ResultBlob
    {
        public string ResultFile { get; set; }
    }

}
