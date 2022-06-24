using api_gestao_pizzaria.Models;

namespace api_gestao_pizzaria.Interfaces
{
    public interface IItemNotaFiscalRepository
    {
        public Task<List<ItemNotaFiscal>> GetItensNotaFiscalPeloIdNotaFiscal(long id);
        public Task<ItemNotaFiscal> GetItemNotaFiscalPeloId(long id);
        public Task<long> CreateItemNotaFiscal(ItemNotaFiscal itemNotaFiscal, long idLote, long idNotaFiscal);
        public Task<bool> UpdateItemNotaFiscal(long id, ItemNotaFiscal itemNotaFiscal);
        public Task<bool> DeleteItemNotaFiscal(long id);
    }
}
