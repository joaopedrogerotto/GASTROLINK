namespace APIGastroLink.Services.Interfaces {
    public interface IImagemService {
        public Task<string> UploadImagem(IFormFile formFile);
    }
}
