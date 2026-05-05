using GastroLink.DTO;
using GastroLink.Models;

namespace GastroLink.Client {
    public class UsuarioClient {
        private HttpClient _HttpClient;

        public UsuarioClient(HttpClient httpClient) {
            _HttpClient = httpClient;
        }

        public async Task<bool> CadastrarUsuario(UsuarioCreateDTO UsuarioCreateDTO) {
            var response = await _HttpClient.PostAsJsonAsync("Usuario", UsuarioCreateDTO);
            if (response.IsSuccessStatusCode) {
                return true;    
            }
            return false;
        }
    }
}
