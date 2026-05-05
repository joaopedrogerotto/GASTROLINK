namespace GastroLink.DTO {
    public class UsuarioCreateDTO {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int TipoUsuarioId { get; set; } = 0;
    }
}
