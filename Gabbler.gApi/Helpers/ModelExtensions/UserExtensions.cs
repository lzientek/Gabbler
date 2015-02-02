using System.Collections.Generic;
using System.Linq;
using Gabbler.Core;
using Gabbler.gApi.Models.Users;

namespace Gabbler.gApi.Helpers.ModelExtensions
{
    internal static class UserExtensions
    {
        internal static User ToUser(this UserInscriptionModel uim)
        {
            return new User()
            {
                FirstName = uim.FirstName,
                LastName = uim.LastName,
                Mail = uim.Mail,
                Pseudo = uim.Pseudo,
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
                BackgroundImagePath = usr.UserImage.BackgroundImage,
                ProfilImagePath = usr.UserImage.ProfileImage,
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
                ProfilImagePath = usr.UserImage.BackgroundImage,
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