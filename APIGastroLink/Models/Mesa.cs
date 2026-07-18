namespace APIGastroLink.Models {
    public class Mesa {
        public int Id { get; set; }
        public string Numero { get; set; }
        public StatusMesa Status { get; set; } = new StatusMesa();
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
    }
}