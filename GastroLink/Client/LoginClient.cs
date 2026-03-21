using GastroLink.Models;

namespace GastroLink.Service {
    public class LoginClient {
        private HttpClient _httpClient;

        public LoginClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<Usuario?> Login(Login Login) {
            var response = await _httpClient.PostAsJsonAsync("Login", Login);

            if (!response.IsSuccessStatusCode) {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Usuario>();
        }
    }
}
