namespace GastroLink.DTO {
    public class PedidoCreateChatbotDTO {
        public string numeroMesa{ get; set; }
        public List<ItemPedidoCreateDTO> itens { get; set; }
    }
}
