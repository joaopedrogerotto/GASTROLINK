using GastroLink.DTO;
using GastroLink.Models;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<List<Usuario>> ObterTodosUsuarios() {
            var response = await _HttpClient.GetAsync("Usuario");
            if (response.IsSuccessStatusCode) {
                var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>();
                return usuarios ?? new List<Usuario>();
            }
            return new List<Usuario>();
        }

        public async Task<Usuario> ObterUsuarioPeloId(int idUsuario) {
            var response = await _HttpClient.GetAsync($"Usuario/{idUsuario}");
            if (response.IsSuccessStatusCode) {
                var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
                return usuario ?? new Usuario();
            }
            return new Usuario();
        }

        [HttpPut]
        public async Task<bool> AtualizarStatusUsuario([FromBody]UsuarioStatusUpdateDTO usuarioStatusUpdateDTO) {
            var response = await _HttpClient.PutAsJsonAsync("Usuario/AlterarStatus", usuarioStatusUpdateDTO);
            return response.IsSuccessStatusCode;
        } 
    }
}
