using GastroLink.DTO;
using GastroLink.Exceptions;
using GastroLink.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GastroLink.Client {
    public class CategoriaPratoClient {
        private HttpClient _httpClient;

        public CategoriaPratoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<CategoriaPratoQuantidadeDTO>> CategoriasQuantidadePratos() {
            var response = await _httpClient.GetAsync("CategoriaPrato/QuantidadePratos");
            if(response.IsSuccessStatusCode) {
                var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaPratoQuantidadeDTO>>();
                return categorias ?? new List<CategoriaPratoQuantidadeDTO>();
            }
            throw new InvalidOperationException("Falha ao recuperar categorias e suas contagens de pratos.");
        }

        public async Task<List<CategoriaPrato>> SelecionarCategorias() {
            var response = await _httpClient.GetAsync("CategoriaPrato/TodasCategorias");
            if (response.IsSuccessStatusCode) {
                var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaPrato>>();
                return categorias ?? new List<CategoriaPrato>();
            }
            throw new InvalidOperationException("Falha ao recuperar categorias de pratos.");
        }

        public async Task<bool> SalvarCategoriaPrato(CategoriaPrato CategoriaPrato) {
            var response = await _httpClient.PostAsJsonAsync("CategoriaPrato", CategoriaPrato);
            if(response.StatusCode == System.Net.HttpStatusCode.Conflict) {
                throw new EntityAlreadyExistsException("Categoria já cadastrada");
            }
            return response.IsSuccessStatusCode;
        }
    }
}
