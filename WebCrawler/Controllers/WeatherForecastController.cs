using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WebCrawler.Data;
using WebCrawler.Model;

namespace WebCrawler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <param name="cidade">(Exemplo: Rio de Janeiro)</param>
        /// <param name="data">(Opicional - Exemplo: 2023-12-06 12:00)</param>
        [HttpGet]
        [Route("GetWeatherForecast")]
        public async Task<IActionResult> WeatherForecast(string? cidade, DateTime? data)
        {
            string url = $"http://api.weatherapi.com/v1/current.json?key=2945dd3625a848df88f134123230612&q={cidade}";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {

                    string conteudo = await response.Content.ReadAsStringAsync();
                    var respostaApi = JsonConvert.DeserializeObject<Content>(conteudo);
                    var respostaFormatada = new
                    {
                        Cidade = respostaApi.Location.name,
                        Temperatura = respostaApi.Current.temp_c + " Graus",
                        País = respostaApi.Location.country,
                        Horas = respostaApi.Location.localTime,
                        Timezone = respostaApi.Location.tz_id,
                        Periodo = respostaApi.Current.is_day == 1 ? "Diurno" : "Noturno",
                        Sensação  = respostaApi.Current.feelslike_c + " Graus",
                        Umidade = respostaApi.Current.humidity

                    };
                    return Ok(respostaFormatada);
                }
                else
                {

                    return BadRequest($"Erro na requisição: {response.StatusCode}");
                }
            }

        }

        /// <param name="cidade">(Exemplo: Rio de Janeiro)</param>
        /// <param name="data">(Opicional - Exemplo: 2023-12-06 12:00)</param>
        [HttpPost]
        [Route("PostWeatherForecast")]
        public IActionResult PostWheatherForecast(string? cidade, DateTime? data)
        {
            var a = "jean";
            return Ok(a);
        }
    }
}