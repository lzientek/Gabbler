using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gabbler.gApi.Models.Gabs
{
    public class GabsList
    {
        public int TotalGabs { get; set; }
        public int NbOfShownGabs { get; set; }
        public int StartGab { get; set; }

        public List<GabBasicModel> Gabs { get; set; }
    }
}