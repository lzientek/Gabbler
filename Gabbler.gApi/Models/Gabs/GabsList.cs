using System.Collections.Generic;

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