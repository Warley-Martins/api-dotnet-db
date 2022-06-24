using api_gestao_pizzaria.Models;

namespace api_gestao_pizzaria.Interfaces
{
    public interface INotaFiscalRepository
    {
        public Task<IEnumerable<NotaFiscal>> GetTodasNotaFiscais();
        public Task<NotaFiscal> GetNotaFiscalPeloId(long id);
        public Task<long> AdicionaNotaFiscal(NotaFiscal notaFiscal);
        public Task<bool> UpdateNotaFiscal(long id, NotaFiscal notaFiscal);
        public Task<bool> DeleteNotaFiscal(long id);
    }
}
