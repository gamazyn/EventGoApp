using Microsoft.AspNetCore.Mvc;
using EventGo.Domain;
using EventGo.Application.Contracts;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using EventGo.Application.Dtos;

namespace EventGo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;
        public LotesController(ILoteService loteService)
        {
            _loteService = loteService;

        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return NoContent();

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar lotes. Error: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]

        public async Task<IActionResult> SaveLote(int eventoId, LoteDTO[] models)
        {
            try
            {
                var lotes = await _loteService.SaveLote(eventoId, models);
                if (lotes == null) return NoContent();

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar atualizar lotes. Error: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                var lote = await _loteService.GetLotebyIdsAsync(eventoId, loteId);
                if (lote == null) return NoContent();

                return await _loteService.DeleteLote(lote.EventoId, lote.Id) ?
                        Ok(new { message = "Lote deletado" }) :
                        throw new Exception("Ocorreu algum erro ao processar sua solicitação.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar deletar lotes. Error: {ex.Message}");
            }
        }
    }
}
