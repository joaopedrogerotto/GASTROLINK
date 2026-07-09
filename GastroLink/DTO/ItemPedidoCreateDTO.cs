namespace GastroLink.DTO {
    public class ItemPedidoCreateDTO {
        public int IdPrato { get; set; }
        public int Quantidade { get; set; }
        public string? Observacao { get; set; }
        public decimal Preco { get; set; }
    }
}
