using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace devApiOneTest
{
    public class TestingApiOne
    {
        public class Taxa
        {
            public float Juros { get; set; }
        }

        [Fact]
        public void TestingApiOne_Response()
        {
            Taxa taxa = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44316/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiTaxajuros = httpClient.GetAsync("Taxajuros");
                apiTaxajuros.Wait();

                var responseApi = apiTaxajuros.Result;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseBody = responseApi.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(responseBody.Result);
                    taxa = JsonConvert.DeserializeObject<Taxa>(obj.ToString());
                }
            }

            Assert.True(taxa.Juros == 0.01f);
        }
    }
}
