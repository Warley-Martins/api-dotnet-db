using api_gestao_pizzaria.Models;

namespace api_gestao_pizzaria.Interfaces
{
    public interface ILoteRepository
    {
        public Task<long> CreateLote(Lote lote, long ingredienteId);
        public Task<IEnumerable<Lote>> GetTodosLotesPeloIngredienteId(long IngredienteId);
        public Task<Lote> GetLotePeloId(long id);
        public Task<IEnumerable<Lote>> GetTodosLotes();
        public Task<bool> UpdateLote(long id, Lote lote);
        public Task<bool> DeleteLote(long id);
    }
}
