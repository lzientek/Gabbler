﻿using System;
using System.Threading.Tasks;
using Gabbler.gApi.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gabbler.gApi.Helpers
{

    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserInscriptionModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.Pseudo,
                Email = userModel.Mail,
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            
            return result;
        }

        public async Task<IdentityUser> FindUserByLogin(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<IdentityUser> FindUser(string usrName)
        {
            IdentityUser user = await _userManager.FindByNameAsync(usrName);
            return user;
        }
        public async Task<IdentityResult> ChangePassword(string id, string oldPass, string newPass)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(id,oldPass,newPass);
            return result;
        }

        public async Task<IdentityResult> ChangeMail(string id, string mail)
        {
            IdentityResult result = await _userManager.SetEmailAsync(id, mail);
            return result;
        } 

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }

}