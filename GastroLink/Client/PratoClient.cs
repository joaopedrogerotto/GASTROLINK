using GastroLink.DTO;
using GastroLink.Mapper;
using GastroLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;

namespace GastroLink.Client {
    public class PratoClient {
        private HttpClient _httpClient;

        public PratoClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<bool> CadastrarPrato(PratoCreateDTO pratoCreateDTO) {

            using (var content = new MultipartFormDataContent()) {
                content.Add(new StringContent(pratoCreateDTO.Nome), nameof(pratoCreateDTO.Nome));
                content.Add(new StringContent(pratoCreateDTO.Descricao), nameof(pratoCreateDTO.Descricao));
                content.Add(new StringContent(pratoCreateDTO.Preco.ToString(CultureInfo.InvariantCulture)), nameof(pratoCreateDTO.Preco));
                content.Add(new StringContent(pratoCreateDTO.TempoMedioPreparo.ToString()), nameof(pratoCreateDTO.TempoMedioPreparo));
                content.Add(new StringContent(pratoCreateDTO.IdCategoriaPrato.ToString()), nameof(pratoCreateDTO.IdCategoriaPrato));

                if(pratoCreateDTO.formFile != null) {
                    var streamContent = new StreamContent(pratoCreateDTO.formFile.OpenReadStream());

                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(pratoCreateDTO.formFile.ContentType);

                    content.Add(streamContent, nameof(pratoCreateDTO.formFile), pratoCreateDTO.formFile.FileName);
                }

                var response = await _httpClient.PostAsync("Prato", content);
                return response.IsSuccessStatusCode;

            }
        }
        public async Task<List<Prato>> TodosPratos() {
            var response = await _httpClient.GetAsync("Prato/TodosPratos");
            if (response.IsSuccessStatusCode) {
                var listPratos = await response.Content.ReadFromJsonAsync<List<Prato>>();
                return listPratos ?? new List<Prato>();
            }
            throw new InvalidOperationException("Falha ao recuperar todos os pratos");
        }

        public async Task<Prato> BuscarPratoPorId(int id) {
            var response = await _httpClient.GetAsync($"Prato/{id}");
            if (response.IsSuccessStatusCode) {
                var prato = await response.Content.ReadFromJsonAsync<Prato>();
                return prato;
            }
            throw new InvalidOperationException("Falha ao buscar prato");
        }

        public async Task<bool> AtualizarDisponibilidade(PratoStatusUpdateDTO pratoStatusUpdateDTO) {
            var response = await _httpClient.PostAsJsonAsync("Prato/AtualizarDisponibilidade", pratoStatusUpdateDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Prato>> SelicionarPesquisaPrato(FiltroPesquisaDTO filtroPesquisaDTO) {
            if(filtroPesquisaDTO == null) {
                throw new InvalidOperationException("Filtro de pesquisa não pode ser vazio");
            }

            var parametrosPesquisa = new Dictionary<string, string>() {
                ["Nome"] = filtroPesquisaDTO.Nome,
                ["Descricao"] = filtroPesquisaDTO.Descricao,
                ["Preco"] = filtroPesquisaDTO.Preco?.ToString(CultureInfo.InvariantCulture),
                ["IdCategoria"] = filtroPesquisaDTO.IdCategoria?.ToString()
            };
            var url = QueryHelpers.AddQueryString("Prato/TodosPratos", parametrosPesquisa);

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var listPratos = await response.Content.ReadFromJsonAsync<List<Prato>>();
                return listPratos ?? new List<Prato>();
            }

            throw new InvalidOperationException("Falha ao buscar pratos pelo filtro");
        }
    }
}
