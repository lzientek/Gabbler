using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Gabbler.gApi.Helpers.Json;
using Gabbler.gApi.Models.Users;
using Newtonsoft.Json;

namespace Gabbler.gApi.Models.Gabs
{
    public class GabDetailModel
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonUnixDateConverter))]
        public DateTime CreationDate { get; set; }
       
        [JsonConverter(typeof(JsonUnixDateConverter))]
        public DateTime? ModificationDate { get; set; }
        public string Content { get; set; }
        public int NbOfLikes { get; set; }
        public int NbOfComments { get; set; }
        public UserBasicModel User { get; set; }
    }
}