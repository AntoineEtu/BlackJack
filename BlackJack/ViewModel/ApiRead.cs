using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModeleClasses;

namespace BlackJack.ViewModel
{
    class ApiRead
    {
        public async void GetUsers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var reponse = await client.GetAsync("/api/user/connected");
                System.Diagnostics.Debug.WriteLine("avant le test");
                String res = "";
                if (reponse.IsSuccessStatusCode)
                {
                    res = await reponse.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(res);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("appel API raté");
                    System.Diagnostics.Debug.WriteLine(res);
                    //throw new Exception(response.Content.ReadAsStringAsync());
                }
            }
        }

    }
}
