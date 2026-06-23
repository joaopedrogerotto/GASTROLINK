namespace GastroLink.DTO {
    public class UsuarioUpdateDTO {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public int TipoUsuarioId { get; set; } = 0;
    }
}
