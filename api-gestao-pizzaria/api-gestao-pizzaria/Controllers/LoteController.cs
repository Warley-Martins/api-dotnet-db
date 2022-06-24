using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoteController : Controller
    {
        private readonly LoteService _loteService;
        public LoteController()
        {
            _loteService = new LoteService();
        }

        [HttpPost("ingrediente/{id}")]
        public async Task<ActionResult> Create(Lote lote, long id)
        {
            try
            {
                await _loteService.CreateLote(lote, id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, Lote lote)
        {
            await _loteService.UpdateLote(id,lote);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await _loteService.DeleteLote(id);
            return Ok();
         
        }
    }
}
