namespace api_gestao_pizzaria.Models
{
    public class Lote
    {
        public long Id { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeTotalInicial { get; set; }
        public string NumeroLote { get; set; }
        public string Descricao { get; set; }
        public DateTime Validade { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
