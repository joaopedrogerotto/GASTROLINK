using GastroLink.Exceptions;

namespace GastroLink.Service {
    public static class ValidadorImagem {
        private static readonly string[] ExtensoesPermitidas = [
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        ]; 

        public static void ValidarExtensaoImagem(IFormFile file) {
            var extensao = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!ExtensoesPermitidas.Contains(extensao)) {
                throw new InvalidExtensionException("Extensão de arquivo inválida");
            }
        }
    }
}
