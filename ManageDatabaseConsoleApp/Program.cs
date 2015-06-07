using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ManageDatabaseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dateUTC = DateTime.UtcNow.AddHours(-24);

            using (var client = new HttpClient())
            {
                HttpResponseMessage msg;
                Task.Run(async () =>
                    msg = await client.PostAsJsonAsync("https://secretmessagewebsite.azurewebsites.net/Home/RemoveOldMessages", new { date = dateUTC })
                    //msg = await client.PostAsJsonAsync("https://localhost:44306/Home/RemoveOldMessages", new { date = dateUTC })
                ).Wait();
            }

        }
    }
}
