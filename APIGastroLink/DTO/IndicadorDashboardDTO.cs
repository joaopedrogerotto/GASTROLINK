namespace APIGastroLink.DTO {
    public class IndicadorDashboardDTO {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public List<DadosDashboardDTO> Dados { get; set; } = new List<DadosDashboardDTO>();
    }
}
