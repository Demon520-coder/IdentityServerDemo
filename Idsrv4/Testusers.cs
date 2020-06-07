using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Idsrv4
{
    public class Testusers
    {
        public static IEnumerable<TestUser> GetTestusers()
        {
            return new List<TestUser>
            {
                   new TestUser
                    {
                        SubjectId = "1",
                        Username = "alice",
                        Password = "password",
                        Claims = new List<Claim>(){new Claim(JwtClaimTypes.Address,"superadmin") }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "bob",
                        Password = "password",
                        Claims = new List<Claim>(){new Claim(JwtClaimTypes.Address, "admin") }
                    }
            };
        }
   
    }
}
