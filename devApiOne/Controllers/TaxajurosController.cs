using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devApiOne.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace devApiOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxajurosController
    {
        /// <summary>
        /// Metodo Taxa de juros.
        /// </summary>
        /// <returns>
        /// Retorna a taxa de juros.
        /// </returns>
        [HttpGet]
        public Taxajuros Get()
        {
            return new Taxajuros { Juros = 0.01f };
        }
    }
}
