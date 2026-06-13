namespace APIGastroLink.Models {
    public class CategoriaPrato {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public List<Prato> Pratos { get; set; } = new List<Prato>();
    }
}
