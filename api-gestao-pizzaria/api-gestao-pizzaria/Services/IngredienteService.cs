using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Repositories;

namespace api_gestao_pizzaria.Services
{
    public class IngredienteService
    {
        public readonly IIngredienteRepository _ingredienteRepository;
        public readonly ILoteRepository _loteRepository;
        public IngredienteService()
        {
            _ingredienteRepository = new IngredienteRepository();
            _loteRepository = new LoteRepository();
        }
        public async Task<Ingrediente> GetIngredienteLotePeloId(long id)
        {
            var ingrediente = await _ingredienteRepository.GetIngredientePeloId(id);
            if(ingrediente != null) { 
                var listaLotes = await _loteRepository.GetTodosLotesPeloIngredienteId(ingrediente.Id);
                foreach(var lote in listaLotes)
                    ingrediente.Lotes.Add(lote);
            }
            return ingrediente;                
        }
        public async Task<IEnumerable<Ingrediente>> GetTodosIngredientes()
        {
            var ingredientes = await _ingredienteRepository.GetTodosIngredientes();
            foreach(var ingrediente in ingredientes)
            {
                var listaLotes = await _loteRepository.GetTodosLotesPeloIngredienteId(ingrediente.Id);
                foreach (var lote in listaLotes)
                    ingrediente.Lotes.Add(lote);
            }
            return ingredientes;
        }
        public async Task<bool> CreateIngrediente(Ingrediente ingrediente)
        {
            var idNovoIngrediente = await _ingredienteRepository.CreateIngrediente(ingrediente);
            if(ingrediente.Lotes.Count != 0)
                foreach(var item in ingrediente.Lotes)
                    _loteRepository.CreateLote(item, idNovoIngrediente);
            return true;
        }
        public async Task<bool> DeleteIngrediente(long id)
        {
            return await _ingredienteRepository.DeleteIngrediente(id);
        }
        public async Task<bool> UpdateIngrediente(long id, Ingrediente ingrediente)
        {
            if (ingrediente.Lotes.Count != 0)
                foreach (var item in ingrediente.Lotes)
                    _loteRepository.UpdateLote(item.Id, item);
            return await _ingredienteRepository.UpdateIngrediente(id, ingrediente);
        }

    }
}
