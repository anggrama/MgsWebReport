using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct
{
    public class QueueViewModel
    {
        public string NoLab { get; set; }
        public string NoRM { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }

    }

    public class SearchResult
    {
        public string RegId { get; set; }
        public string NoRm { get; set; }
        public string PatientName { get; set; }
        public List<SearchResultDetail> ResultDetail { get; set; }
    }

    public class SearchResultDetail
    {
        public string TestId { get; set; }
        public string TestName { get; set; }
        public string TestValue { get; set; }
        
    }
        

}
