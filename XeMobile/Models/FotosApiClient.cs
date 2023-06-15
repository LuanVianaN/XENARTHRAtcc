using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XeMobile
{
    public class FotosApiClient
    {
        // primeira api de teste
        /*private const string apiUrl = "http://10.0.2.2:44342/api/Fotos";

        public async Task EnviarLatitudeLongitude(double latitude, double longitude)
        {
            try
            {
                var data = new Dictionary<string, string>
                {
                    { "Latitude", latitude.ToString() },
                    { "Longitude", longitude.ToString() }
                };

                var content = new FormUrlEncodedContent(data);

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
        }*/
    }
}
