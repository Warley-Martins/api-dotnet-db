using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Repositories;
using api_gestao_pizzaria.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors()]
    public class NotaFiscalController : ControllerBase
    {
        private readonly NotaFiscalService _notaFiscalService;
        public NotaFiscalController()
        {
            _notaFiscalService = new NotaFiscalService();
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _notaFiscalService.GetTodasNotasFiscais();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaFiscalItemNotaFiscalLotaPeloIdNotaFiscal(id);
            return Ok(notaFiscal);
        }
        [HttpPost]
        public async Task<ActionResult> Post(NotaFiscal notaFiscal)
        {
            try
            {
                 await _notaFiscalService.AdicionaNotaFiscal(notaFiscal);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("ingrediente/{ingredienteId}")]
        public async Task<ActionResult> Post(NotaFiscal notaFiscal, long ingredienteId)
        {
            try
            {
                 await _notaFiscalService.AdicionaNotaFiscalItemNotaFiscalLote(notaFiscal, ingredienteId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, NotaFiscal notaFiscal)
        {
            try
            {
                await _notaFiscalService.UpdateNotaFiscal(id, notaFiscal);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _notaFiscalService.DeleteNotaFiscal(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
