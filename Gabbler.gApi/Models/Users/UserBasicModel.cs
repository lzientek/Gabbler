namespace Gabbler.gApi.Models.Users
{
    public class UserBasicModel
    {
        public int Id { get; set; }

        public string Pseudo { get; set; }

        public int NbFollowers { get; set; }

        public string ProfilImagePath { get; set; }
    }
}