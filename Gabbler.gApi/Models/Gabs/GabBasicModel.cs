using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Models.Gabs
{
    public class GabBasicModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public int NbOfLikes { get; set; }
        public int NbOfComments { get; set; }
        public UserBasicModel User { get; set; }
    }
}