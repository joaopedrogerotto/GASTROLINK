using GastroLink.DTO;
using GastroLink.Models;
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
                content.Add(new StringContent(pratoCreateDTO.Preco.ToString(new CultureInfo("pt-BR"))), nameof(pratoCreateDTO.Preco));
                content.Add(new StringContent(pratoCreateDTO.TempoMedioPreparo.ToString()), nameof(pratoCreateDTO.TempoMedioPreparo));
                content.Add(new StringContent(pratoCreateDTO.IdCategoriaPrato.ToString()), nameof(pratoCreateDTO.IdCategoriaPrato));

                if (pratoCreateDTO.formFile != null) {
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
            if (filtroPesquisaDTO == null) {
                throw new InvalidOperationException("Filtro de pesquisa não pode ser vazio");
            }

            var parametrosPesquisa = new Dictionary<string, string>() {
                ["Nome"] = filtroPesquisaDTO.Nome,
                ["Descricao"] = filtroPesquisaDTO.Descricao,
                ["Preco"] = filtroPesquisaDTO.Preco?.ToString(CultureInfo.InvariantCulture),
                ["IdCategoria"] = filtroPesquisaDTO.IdCategoria?.ToString(),
                ["Disponibilidade"] = filtroPesquisaDTO.Disponibilidade.ToString()
            };
            var url = QueryHelpers.AddQueryString("Prato/TodosPratos", parametrosPesquisa);

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var listPratos = await response.Content.ReadFromJsonAsync<List<Prato>>();
                return listPratos ?? new List<Prato>();
            }

            throw new InvalidOperationException("Falha ao buscar pratos pelo filtro");
        }

        public async Task<List<Prato>> TodosPrato(FiltroPesquisaDTO filtroPesquisaDTO) {

            var parametrosPesquisa = new Dictionary<string, string>();
            var url = QueryHelpers.AddQueryString("Prato/TodosPratos", parametrosPesquisa);

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                var listPratos = await response.Content.ReadFromJsonAsync<List<Prato>>();
                return listPratos ?? new List<Prato>();
            }

            throw new InvalidOperationException("Falha ao buscar pratos pelo filtro");
        }

        public async Task<bool> AtualizarPrato(PratoEditarDTO pratoEditarDTO) {
            using (var content = new MultipartFormDataContent()) {
                content.Add(new StringContent(pratoEditarDTO.Id.ToString()), nameof(pratoEditarDTO.Id));
                content.Add(new StringContent(pratoEditarDTO.Nome), nameof(pratoEditarDTO.Nome));
                content.Add(new StringContent(pratoEditarDTO.Descricao), nameof(pratoEditarDTO.Descricao));
                content.Add(new StringContent(pratoEditarDTO.Preco.ToString(new CultureInfo("pt-BR"))), nameof(pratoEditarDTO.Preco));
                content.Add(new StringContent(pratoEditarDTO.TempoMedioPreparo.ToString()), nameof(pratoEditarDTO.TempoMedioPreparo));
                content.Add(new StringContent(pratoEditarDTO.IdCategoriaPrato.ToString()), nameof(pratoEditarDTO.IdCategoriaPrato));
                content.Add(new StringContent(pratoEditarDTO.UrlImagem), nameof(pratoEditarDTO.UrlImagem));
                if (pratoEditarDTO.formFile != null) {
                    var streamContent = new StreamContent(pratoEditarDTO.formFile.OpenReadStream());
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(pratoEditarDTO.formFile.ContentType);
                    content.Add(streamContent, nameof(pratoEditarDTO.formFile), pratoEditarDTO.formFile.FileName);
                }
                var response = await _httpClient.PutAsync("Prato/AtualizarPrato", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Resposta: {responseContent}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
