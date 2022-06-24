using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Repositories;

namespace api_gestao_pizzaria.Services
{
    public class ItemNotaFiscalService
    {
        private readonly IItemNotaFiscalRepository _itemNotaFiscalRepository;
        private readonly ILoteRepository _loteRepository;
        public ItemNotaFiscalService()
        {
            _itemNotaFiscalRepository = new ItemNotaFiscalRepository();
            _loteRepository = new LoteRepository();
        }
        public async Task<bool> AdicionaItemNotaFiscal(ItemNotaFiscal itemNotaFiscal, long idNotaFiscal, long ingredienteId)
        {
            var idLote = await _loteRepository.CreateLote(itemNotaFiscal.Lote, ingredienteId);
            await _itemNotaFiscalRepository.CreateItemNotaFiscal(itemNotaFiscal, idLote, idNotaFiscal);
            return true;
        }
        public async Task<bool> UpdateItemNotaFiscal(long id, ItemNotaFiscal itemNotaFiscal)
        {
            await _itemNotaFiscalRepository.UpdateItemNotaFiscal(id, itemNotaFiscal);
            // await _loteRepository.UpdateLote(id, itemNotaFiscal.Lote);
            return true;
        }  
        public async Task<bool> DeleteItemNotaFiscal(long id)
        {
            await _itemNotaFiscalRepository.DeleteItemNotaFiscal(id);
            return true;
        }
        public async Task<ItemNotaFiscal> GetItemNotaFiscalPeloId(long id)
        {
            return await _itemNotaFiscalRepository.GetItemNotaFiscalPeloId(id);
            
        }
    }
}
