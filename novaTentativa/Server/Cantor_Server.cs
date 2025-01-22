using novaTentativa.Models;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;

namespace novaTentativa.Server
{
    public class Cantor_Server
    {
        private readonly HttpClient _httpClient;

        public Cantor_Server(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cantor_Model>> GetCantoresAsync()
        {
            try
            {
                var url = "https://guilhermeonrails.github.io/api-csharp-songs/songs.json";
                var response = await _httpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Cantor_Model>>(response);
            }
            catch (Exception error)
            {

                Console.WriteLine($"Erro ao localizar: {error.Message}");
                return null;
            }
        }
    }
}
