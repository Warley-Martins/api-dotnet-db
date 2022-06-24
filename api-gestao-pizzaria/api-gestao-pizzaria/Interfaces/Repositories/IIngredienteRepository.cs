using api_gestao_pizzaria.Models;

namespace api_gestao_pizzaria.Interfaces
{
    public interface IIngredienteRepository
    {
        public Task<IEnumerable<Ingrediente>> GetTodosIngredientes();
        public Task<Ingrediente> GetIngredientePeloId(long id);
        public Task<bool> DeleteIngrediente(long id);
        public Task<long> CreateIngrediente(Ingrediente ingrediente);
        public Task<bool> UpdateIngrediente(long id, Ingrediente ingrediente);
    }
}
