using APIGastroLink.Services.Interfaces;

namespace APIGastroLink.Services {
    public class ImagemService : IImagemService {
        public async Task<string> UploadImagem(IFormFile formFile) {
            string nomeArquivo = $"{Guid.NewGuid()}_{Path.GetExtension(formFile.FileName)}";

            string pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens", "pratos");

            if (!Directory.Exists(pasta)) {
                Directory.CreateDirectory(pasta);
            }

            string caminhoCompleto = Path.Combine(pasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create)) {
                await formFile.CopyToAsync(stream);
            }

            return $"imagens/pratos/{nomeArquivo}";
        }
    }
}
