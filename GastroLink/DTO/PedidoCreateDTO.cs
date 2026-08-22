namespace GastroLink.DTO {
    public class PedidoCreateDTO {
        public int IdMesa { get; set; }
        public List<ItemPedidoCreateDTO> Itens { get; set; } = new List<ItemPedidoCreateDTO>();
        public int IdUsuario { get; set; }
    }
}
