using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Repositories;

namespace api_gestao_pizzaria.Services
{
    public class NotaFiscalService
    {
        public readonly IItemNotaFiscalRepository _itemNotaFiscalRepository;
        public readonly INotaFiscalRepository _notaFiscalRepository;
        public readonly ItemNotaFiscalService _itemNotaFiscalService;
        private readonly LoteService _loteService;
        public NotaFiscalService()
        {
            _itemNotaFiscalRepository = new ItemNotaFiscalRepository();
            _notaFiscalRepository = new NotaFiscalRepository();
            _loteService = new LoteService();
            _itemNotaFiscalService = new ItemNotaFiscalService();
        }
        public async Task<IEnumerable<NotaFiscal>> GetTodasNotasFiscais()
        {
            var notasFiscais = await _notaFiscalRepository.GetTodasNotaFiscais();
            foreach(var item in notasFiscais)
                item.ItensNotaFiscal = await _itemNotaFiscalRepository.GetItensNotaFiscalPeloIdNotaFiscal(item.Id);
            return notasFiscais;
        }
        public async Task<NotaFiscal> GetNotaFiscalItemNotaFiscalLotaPeloIdNotaFiscal(long id)
        {
            var notaFiscal = await _notaFiscalRepository.GetNotaFiscalPeloId(id);
            var itensNotaFiscal = await _itemNotaFiscalRepository.GetItensNotaFiscalPeloIdNotaFiscal(id);
            if(itensNotaFiscal != null)
                notaFiscal.ItensNotaFiscal = itensNotaFiscal;
            return notaFiscal;
        }
        public async Task<bool> AdicionaNotaFiscal(NotaFiscal notaFiscal)
        {
            var idNovaNotaFiscal = await _notaFiscalRepository.AdicionaNotaFiscal(notaFiscal);
            return true;
        }
        public async Task<bool> AdicionaNotaFiscalItemNotaFiscalLote(NotaFiscal notaFiscal, long ingredienteId)
        {
            var idNovaNotaFiscal = await _notaFiscalRepository.AdicionaNotaFiscal(notaFiscal);
            if (notaFiscal.ItensNotaFiscal.Count != 0)
                foreach (var item in notaFiscal.ItensNotaFiscal)
                {
                    await _itemNotaFiscalService.AdicionaItemNotaFiscal(item, idNovaNotaFiscal, ingredienteId);
                }
            return true;
        }
        public async Task<bool> AdcionaItemNotaFiscalEmNotaFiscal(ItemNotaFiscal itemNotaFiscal, long idNotaFiscal, long ingredienteId)
        {
            await _itemNotaFiscalService.AdicionaItemNotaFiscal(itemNotaFiscal, idNotaFiscal, ingredienteId);
            return true;
        }
        public async Task<bool> UpdateNotaFiscal(long id, NotaFiscal notaFiscal)
        {
            await _notaFiscalRepository.UpdateNotaFiscal(id, notaFiscal);
            if (notaFiscal.ItensNotaFiscal.Count != 0)
                foreach (var item in notaFiscal.ItensNotaFiscal)
                    await _itemNotaFiscalService.UpdateItemNotaFiscal(item.Id, item);
            return true;
        }
        public async Task<bool> DeleteNotaFiscal(long id)
        {
            await _notaFiscalRepository.DeleteNotaFiscal(id);
            return true;
        }
    }
}
