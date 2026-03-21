namespace GastroLink.Models {
    public class Usuario {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public TipoUsuario Tipo { get; set; } = new TipoUsuario();
    }
}
