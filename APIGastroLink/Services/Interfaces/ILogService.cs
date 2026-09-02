namespace APIGastroLink.Services.Interfaces {
    public interface ILogService {
        void Error(Exception ex, string mensagem);
    }
}
