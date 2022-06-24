namespace api_gestao_pizzaria.Models
{
    public class Ingrediente
    {
        public Ingrediente()
        {
            Lotes = new List<Lote>();
        }
        public long Id { get; set; } 
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual int QuantidadeDisponivel { 
            get
            {
                int quantidade = 0;
                foreach (var lote in Lotes)
                    if(lote.Validade > DateTime.Now)
                        quantidade += lote.QuantidadeDisponivel;
                return quantidade;
            }
        }
        public List<Lote> Lotes { get; set; }
    }
}
