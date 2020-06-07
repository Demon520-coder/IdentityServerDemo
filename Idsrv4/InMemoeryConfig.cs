using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idsrv4
{
    public class InMemoeryConfig
    {       
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customeProfile = new IdentityResource("my.age", "age", new[] { JwtClaimTypes.Address });
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                customeProfile
            };
        }


        /// <summary>
        /// 定义受保护的Api资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiRes1",new[]{JwtClaimTypes.Address })
               
            };
        }

        /// <summary>
        /// 定义被授权的客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new Client[] {
               new Client
               {
                   ClientId="api1",
                   ClientName="API1",
                   ClientSecrets={new Secret("api1".Sha256())},
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes={ "ApiRes1"},
                   AlwaysSendClientClaims=true,
                   
               },
               new Client
               {
                   ClientId="api2",
                   ClientName="API2",
                   ClientSecrets={new Secret("api2".Sha256())},
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes={"Api2Res2"}
               }
            };
        }
    }
}
