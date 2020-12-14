using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using devApiTwo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace devApiTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculajurosController
    {
        public class Taxa
        {
            public float Juros { get; set; }
        }
        /// <summary>
        /// Metodo Calcula Juros com dois parametros de entrada.
        /// </summary>
        /// <returns>
        /// Retorna o calculo de juros.
        /// </returns>
        [HttpGet]
        public Calculajuros Get([FromQuery(Name = "valorinicial")] float valorinicial, [FromQuery(Name = "meses")] int meses)
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
                else
                {
                    Console.WriteLine("Erro no servidor. Contate o Administrador.");
                }
            }

            double resultado = valorinicial * Math.Pow((1 + taxa.Juros), meses);
            double x = Math.Truncate(resultado * 100) / 100;

            return new Calculajuros { CalculajurosReturn = x };
        }
    }
}
