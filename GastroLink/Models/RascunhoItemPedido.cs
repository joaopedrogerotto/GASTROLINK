namespace GastroLink.Models {
    public class RascunhoItemPedido {
        public Prato Prato { get; set; }
        public int Quantidade { get; set; } = 1;
        public string? Observacao { get; set; }
        public decimal Preco { get; set; }
    }
}
