using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Repositories;

namespace api_gestao_pizzaria.Services
{
    public class LoteService
    {
        private readonly ILoteRepository _loteRepository;
        public LoteService()
        {
            _loteRepository = new LoteRepository();
        }
        public async Task<long> CreateLote(Lote lote, long ingredienteId)
        {
            return await _loteRepository.CreateLote(lote, ingredienteId);
        }
        public async Task<Lote> GetLotePeloId(long id)
        {
            return await _loteRepository.GetLotePeloId(id);
        }
        public async Task<bool> UpdateLote(long id, Lote lote)
        {
            return await _loteRepository.UpdateLote(id, lote);
        }
        public async Task<bool> DeleteLote(long id)
        {
            return await _loteRepository.DeleteLote(id);
        }

    }
}
