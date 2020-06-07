using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Idsrv4
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> logger;
        public ProfileService(ILogger<ProfileService> logger)
        {
            this.logger = logger;
        }
       
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(logger);
           
            //context.IssuedClaims.Add(new System.Security.Claims.Claim(JwtClaimTypes.Name, "zzl"));
            //context.IssuedClaims.Add(new System.Security.Claims.Claim(JwtClaimTypes.Email, "zzl@qq.com"));
            context.IssuedClaims.Add(new System.Security.Claims.Claim(JwtClaimTypes.Address, "zzl@qq.com"));
            context.IssuedClaims.AddRange(context.Subject.Claims);
           
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            return Task.CompletedTask;
        }
    }
}
