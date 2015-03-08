using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gabbler.gApi.Models.Gabs;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Models.Search
{
    public class SearchListModel
    {
        public int NbResultUser { get; set; }
        public int NbResultGabs { get; set; }
        public IEnumerable<UserBasicModel> ListOfUser { get; set; }
        public IEnumerable<GabBasicModel> ListOfGab { get; set; }



    }
}