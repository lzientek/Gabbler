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
    }
}