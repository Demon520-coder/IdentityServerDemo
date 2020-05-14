using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await ApiTest("http://localhost:5001/api/values");
        }


        static async Task ApiTest(string apiUrl)
        {
            
            using (var discoveryClient = new HttpClient())
            {
                DiscoveryDocumentRequest req = new DiscoveryDocumentRequest();
                req.Address = "http://localhost:5000";
                req.Method = HttpMethod.Get;
                //发现认证服务
                var discovery = await discoveryClient.GetDiscoveryDocumentAsync(req);
                var nl = Environment.NewLine;
                if (discovery.IsError)
                {
                    Console.WriteLine("======discoveryError=======" + nl);
                    Console.WriteLine($"======{discovery.Error}======" + nl);
                    Console.WriteLine("======discoveryError=======" + nl);
                    return;
                }

                ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest
                {
                    Address = discovery.TokenEndpoint,
                    ClientId = "api1",
                    ClientSecret = "api1",
                    Scope = "ApiRes1",
                    GrantType = IdentityModel.OidcConstants.GrantTypes.ClientCredentials
                };

                //获取token
                var tokenResponse = await discoveryClient.RequestClientCredentialsTokenAsync(tokenRequest);

                if (tokenResponse.IsError)
                {
                    Console.WriteLine("======tokenResponseError=======" + nl);
                    Console.WriteLine($"======{tokenResponse.Error}======" + nl);
                    Console.WriteLine("======tokenResponseError=======" + nl);
                    return;
                }

                using (var apiClient = new HttpClient())
                {
                    //设置token
                    apiClient.SetBearerToken(tokenResponse.AccessToken);
                    //请求api
                    var result = await apiClient.GetStringAsync(apiUrl);

                    Console.WriteLine("======apiResult=======" + nl);
                    Console.WriteLine($"======{result}======" + nl);
                    Console.WriteLine("======apiResult=======" + nl);
                }
            }
        }
    }
}
