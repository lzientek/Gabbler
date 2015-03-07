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
        public List<UserBasicModel> ListOfUser { get; set; }
        public GabsList ListOfGab { get; set; }

    }
}