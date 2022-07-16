using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PktViewModel
{
    public class ExportModel : DropdownModel
    {
            public List<DropdownModel> Creator { get; set; }
            public List<DropdownModel> Location { get; set; }
            public List<DropdownModel> DocumentProfile { get; set; }
            public string UploadDate_Start { get; set; }
            public string UploadDate_End { get; set; }
            public string SearchText { get; set; }
       
    }

    public class ReportModel
    {

    }
}
