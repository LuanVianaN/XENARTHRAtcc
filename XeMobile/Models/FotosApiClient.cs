using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XeMobile
{
    public class FotosApiClient
    {
        private const string apiUrl = "https://seusite.com/api/Fotos"; // Substitua pela URL correta da sua API

        public async Task EnviarLatitudeLongitude(double latitude, double longitude)
        {
            try
            {
                var fotos = new
                {
                    Latitude = latitude,
                    Longitude = longitude
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(fotos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // A solicitação foi bem-sucedida
                        // Faça o tratamento necessário aqui
                    }
                    else
                    {
                        // Ocorreu um erro na solicitação
                        // Lide com o erro aqui
                    }
                }
            }
            catch (Exception ex)
            {
                // Ocorreu uma exceção durante o processo
                // Lide com a exceção aqui
            }
        }
    }
}