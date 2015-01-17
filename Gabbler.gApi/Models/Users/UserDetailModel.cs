using System;
using Gabbler.gApi.Helpers.Json;
using Newtonsoft.Json;

namespace Gabbler.gApi.Models.Users
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        
        public string Mail { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Pseudo { get; set; }
        
        [JsonConverter(typeof(JsonUnixDateConverter))]
        public DateTime CreationDate { get; set; }

        [JsonConverter(typeof(JsonUnixDateConverter))]
        public DateTime? ModificationDate { get; set; }

        public string BackgroundImagePath { get; set; }

        public string ProfilImagePath { get; set; }

        public int NbGab { get; set; }
       
        public int NbFollowers { get; set; }
       
        public int NbFollows { get; set; }
    }
}