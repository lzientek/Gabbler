using System;
using Gabbler.gApi.Helpers.Json;
using Gabbler.gApi.Models.Users;
using Newtonsoft.Json;

namespace Gabbler.gApi.Models.Comments
{
    public class CommentModel
    {
        public int Id { get; set; }
        public UserBasicModel User { get; set; }
        public string Message { get; set; }
        
        [JsonConverter(typeof(JsonUnixDateConverter))]
        public DateTime CreationDate { get; set; }

        [JsonConverter(typeof(JsonUnixDateConverter))]        
        public DateTime? ModificationDate { get; set; }
    }
}