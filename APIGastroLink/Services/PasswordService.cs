namespace APIGastroLink.Services {
    public class PasswordService {
        public const int DificuldadeSenha = 12;

        public string HashPassword(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password, DificuldadeSenha);
        }

        public bool VerifyPassword(string password, string hash) {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
