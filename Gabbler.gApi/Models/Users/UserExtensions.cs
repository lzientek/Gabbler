using System.Collections.Generic;
using System.Linq;
using Gabbler.Core;
using Gabbler.gApi.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;

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

        internal static User ToUser(this UserInscriptionModel uim, string foreignId)
        {
            return new User()
            {
                FirstName = uim.FirstName,
                LastName = uim.LastName,
                Mail = uim.Mail,
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
                NbGab = usr.Gabs.Count,
                NbFollowers = usr.Followers.Count,
                NbFollows = usr.Follows.Count,
                BackgroundImagePath = usr.BackgroundImagePath,
                ProfilImagePath = usr.ProfilePhotoPath,
                CreationDate = usr.CreationDate,
                ModificationDate = usr.ModificationDate,

            };
        }

        internal static UserBasicModel ToUserBasicModel(this User usr)
        {

            return new UserBasicModel()
            {
                Id = usr.Id,
                Pseudo = usr.Pseudo,
                NbFollowers = usr.Followers.Count,
                ProfilImagePath = usr.ProfilePhotoPath,
            };
        }

        internal static IEnumerable<UserBasicModel> ToUserBasicModel(this IEnumerable<User> usr)
        {

            return usr.Select(ToUserBasicModel);
        }

        internal static IEnumerable<UserBasicModel> FollowsToUserBasicModel(this IEnumerable<Follow> follows)
        {

            return follows.Select(f => f.User.ToUserBasicModel());
        }

        internal static IEnumerable<UserBasicModel> FollowersToUserBasicModel(this IEnumerable<Follow> followers)
        {

            return followers.Select(f => f.Follower.ToUserBasicModel());
        }
    }
}