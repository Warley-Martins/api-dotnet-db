using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteController : Controller
    {
        public readonly IngredienteService _ingredienteService;

        public IngredienteController()
        {
            _ingredienteService = new IngredienteService();
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _ingredienteService.GetTodosIngredientes();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var list = await _ingredienteService.GetIngredienteLotePeloId(id);
            return Ok(list);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Ingrediente ingrediente)
        {
            try
            {
                await _ingredienteService.CreateIngrediente(ingrediente);
                return Ok();
            }
            catch
            {
                return BadRequest("Erro ao criar ingrediente");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _ingredienteService.DeleteIngrediente(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] Ingrediente ingrediente)
        {
            try
            {
                await _ingredienteService.UpdateIngrediente(id, ingrediente);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
