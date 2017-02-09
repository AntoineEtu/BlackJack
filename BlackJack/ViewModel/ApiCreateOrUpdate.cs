using ModeleClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModel
{
    class ApiCreateOrUpdate
    {
        public async void ApiCreateUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re/");
                var json = JsonConvert.SerializeObject(user);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/auth/register", itemJson);
                String res = "";
                if (response.IsSuccessStatusCode)
                {
                    //System.Diagnostics.Debug.WriteLine("1er :"+response.Content.ToString());
                    res = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("2eme :"+res);

                }else
                {
                    //messages d'érreurs
                    System.Diagnostics.Debug.WriteLine("création raté - erreurs :");
                    System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
                    System.Diagnostics.Debug.WriteLine(response.Headers.ToString());
                    System.Diagnostics.Debug.WriteLine(response.ReasonPhrase.ToString());
                    System.Diagnostics.Debug.WriteLine(response.RequestMessage.ToString());
                }
            }
        }
    }
}
