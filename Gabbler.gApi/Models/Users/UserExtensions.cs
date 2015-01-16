using Gabbler.Core;

namespace Gabbler.gApi.Models.Users
{
    internal static class UserExtensions
    {
        internal static User ToUser(this UserInscriptionModel uim)
        {
            return new User()
            {
                FirstName = uim.FirstName,
                LastName = uim.LastName,
                Mail = uim.LastName,
                Pseudo = uim.Pseudo,
            };
        }
        
        internal static User ToUser(this UserInscriptionModel uim,string foreignId)
        {
            return new User()
            {
                FirstName = uim.FirstName,
                LastName = uim.LastName,
                Mail = uim.LastName,
                Pseudo = uim.Pseudo,
                ConnectionId = foreignId,
            };
        }

        internal static UserDetailModel ToUserDetailModel(this User usr)
        {
            return new UserDetailModel()
            {
                Id = usr.Id,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Mail = usr.Mail,
                Pseudo = usr.Pseudo,
            };
        }
    }
}