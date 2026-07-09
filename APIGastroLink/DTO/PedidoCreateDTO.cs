namespace APIGastroLink.DTO {
    public class PedidoCreateDTO {
        public int IdMesa { get; set; }
        public List<ItemPedidoCreateDTO> Itens { get; set; }
        public int IdUsuario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
