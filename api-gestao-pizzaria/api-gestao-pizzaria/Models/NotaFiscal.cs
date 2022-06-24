namespace api_gestao_pizzaria.Models
{
    public class NotaFiscal
    {
        public long Id { get; set; }
        public string NumeroNota { get; set; }
        public virtual double ValorTotalNota { get
            {
                double valorTotalNota = 0;
                foreach (var itemNotaFiscal in ItensNotaFiscal)
                    valorTotalNota += (itemNotaFiscal.Lote.QuantidadeTotalInicial *
                                        itemNotaFiscal.ValorUnitarioPago);
                    return valorTotalNota;
            }
        }
        public List<ItemNotaFiscal> ItensNotaFiscal { get; set; } = new List<ItemNotaFiscal>();
        public DateTime DataCompra { get; set; }

    }
}
