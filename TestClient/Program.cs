using IdentityModel.Client;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {

        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //var input = Console.ReadLine();
            //if (input == "1")
            //{
            //    await ApiTest("http://localhost:5001/api/values");
            //}

            //Console.ReadKey();
            HttpClient client = new HttpClient();
            Stopwatch stopwatch = new Stopwatch();
            var result = await client.GetStreamAsync("http://localhost:5005/home/protobtest");
            stopwatch.Start();
            var students = Serializer.Deserialize<IEnumerable<Student>>(result);
            stopwatch.Stop();
            double sec = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"protobuffer.sec={sec}");
            stopwatch.Reset();
            client = new HttpClient();
            var jsonResult = await client.GetStringAsync("http://localhost:5005/home/jsontest");
            stopwatch.Start();
            students = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Student>>(jsonResult);
            stopwatch.Stop();
            sec = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"json.sec={sec}");
            //foreach (var item in students)
            //{
            //    Console.WriteLine($"{nameof(item.Name)}={item.Name},{nameof(item.Age)}={item.Age},{nameof(item.Gender)}={item.Gender}");

            //}

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

                Console.WriteLine(tokenResponse.AccessToken);

                using (var apiClient = new HttpClient())
                {
                    //设置token
                    apiClient.SetBearerToken(tokenResponse.AccessToken);
                    //请求api
                    var temp = await apiClient.GetStringAsync(discovery.UserInfoEndpoint);


                    var result = await apiClient.GetStringAsync(apiUrl);

                    Console.WriteLine("======apiResult=======" + nl);
                    Console.WriteLine($"======{result}======" + nl);
                    Console.WriteLine("======apiResult=======" + nl);
                }
            }
        }
    }
}
