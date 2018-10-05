using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static System.Console;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            try
            {
                const string jwtUrl = "https://localhost:5001/api/jwt";
                var jwt = client.GetStringAsync(jwtUrl).GetAwaiter().GetResult();

                WriteLine(jwt);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwt);

                const string protectedUrl = "https://localhost:5001/api/values";
                var result = client.GetStringAsync(protectedUrl).GetAwaiter().GetResult();

                WriteLine(result);
            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }
        }
    }
}
