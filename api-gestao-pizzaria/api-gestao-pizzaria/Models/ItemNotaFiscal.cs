namespace api_gestao_pizzaria.Models
{
    public class ItemNotaFiscal
    {
        public long Id { get; set; }
        public double ValorUnitarioPago { get; set; }
        public Lote Lote { get; set; } = new Lote();
        public virtual double ValorTotalPago { get => (Lote.QuantidadeTotalInicial * ValorUnitarioPago); }
    }
}