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
        public async void ApiConnect(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14144/");

                var json = JsonConvert.SerializeObject(user);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("uri/to/call", itemJson);
                if (response.IsSuccessStatusCode)
                {
                }
            }
        }
    }
}
