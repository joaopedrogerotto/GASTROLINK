using GastroLink.Models;
using System.Text;
using System.Text.Json;

namespace GastroLink.Client {
    public class TipoUsuarioClient {
        private HttpClient _httpClient;

        public TipoUsuarioClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<TipoUsuario>> SelecionarTodosTipoUsuario() {
            var response = await _httpClient.GetAsync("TipoUsuario");
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return await JsonSerializer.DeserializeAsync<List<TipoUsuario>>(new MemoryStream(Encoding.UTF8.GetBytes(content))   , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<TipoUsuario>();
        }
    }
}
