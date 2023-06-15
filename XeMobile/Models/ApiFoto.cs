using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XeMobile.Models
{
    public class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService()
        {
            httpClient = new HttpClient();
        }
        //Envio dos dados de FotoT para a api do site (basta alterar para Foto para enviar os dados originais)
        public async Task<bool> EnviarFoto(FotoT foto)
        {
            try
            {
                string url = "http://10.0.3.2:44342/api/Fotos";

                var json = JsonConvert.SerializeObject(foto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
