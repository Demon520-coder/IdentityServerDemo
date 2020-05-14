using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idsrv4
{
    public class InMemoeryConfig
    {
        /// <summary>
        /// 定义受保护的Api资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiRes1","FirstApiResource"),
                new ApiResource("ApiRes2","SecondApiResource")
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
                   AllowedScopes={ "ApiRes1" }
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
