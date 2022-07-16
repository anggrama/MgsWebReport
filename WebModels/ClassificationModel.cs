using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PktViewModel
{
    public class ClassificationModel
    {
        public string ClassCode { get; set; }
        public string ClassDescription { get; set; }
        public int? ActiveYears { get; set; }
        public string ActiveDescription { get; set; }
        public int? InactiveYears { get; set; }
        public string InactiveDescription { get; set; }
        public string FollDepreciation { get; set; }
        public string ClassSecurity { get; set; }
        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
        public string BasicConsideration { get; set; }
        public string ProcessingUnit { get; set; }
        public bool IsActive { get; set; }

    }
}
