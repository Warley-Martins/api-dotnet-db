using api_gestao_pizzaria.Interfaces;
using api_gestao_pizzaria.Models;
using api_gestao_pizzaria.Repositories;
using api_gestao_pizzaria.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_gestao_pizzaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemNotaFiscalController : Controller
    {
        private readonly ItemNotaFiscalService _itemNotaFiscalService;
        public ItemNotaFiscalController()
        {
            _itemNotaFiscalService = new ItemNotaFiscalService();
        }
        [HttpPost]
        public async Task<ActionResult> Create(ItemNotaFiscal itemNotaFiscal, long idNotaFiscal, long ingredienteId)
        {
            try
            {
                await _itemNotaFiscalService.AdicionaItemNotaFiscal(itemNotaFiscal, idNotaFiscal, ingredienteId);
                return Ok();
            }
            catch
            {
                return View();
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(long idNotaFiscal, ItemNotaFiscal itemNotaFiscal)
        {
            try
            {
                await _itemNotaFiscalService.UpdateItemNotaFiscal(idNotaFiscal, itemNotaFiscal);
                return Ok();
            }
            catch
            {
                return View();
            }
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(long idItemNotaFiscal)
        {
            try
            {
                await _itemNotaFiscalService.DeleteItemNotaFiscal(idItemNotaFiscal);
                return Ok();
            }
            catch
            {
                return View();
            }
        }
    }
}
