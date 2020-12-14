using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace devApiTwoTest
{
    public class TestingApiTwo
    {
        public class Calculajuros
        {
            public float CalculajurosReturn { get; set; }
        }

        [Fact]
        public void TestingApiTwo_EndPointsOne_Response()
        {
            Calculajuros juros = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44345/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiTaxajuros = httpClient.GetAsync("Calculajuros?valorinicial=100&meses=5");
                apiTaxajuros.Wait();

                var responseApi = apiTaxajuros.Result;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseBody = responseApi.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(responseBody.Result);
                    juros = JsonConvert.DeserializeObject<Calculajuros>(obj.ToString());
                }
            }

            Assert.True(juros.CalculajurosReturn == 105.1f);
        }

        [Fact]
        public void TestingApiTwo_EndPointsTwo_Response()
        {
            string codeUri = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44345/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiTaxajuros = httpClient.GetAsync("Showmethecode");
                apiTaxajuros.Wait();

                var responseApi = apiTaxajuros.Result;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseBody = responseApi.Content.ReadAsStringAsync();
                    codeUri = responseBody.Result;
                }
            }

            Assert.True(codeUri != "");
        }
    }
}
