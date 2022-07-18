using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventGo.Application.Contracts;
using EventGo.Application.Dtos;
using EventGo.Domain;
using EventGo.Persistence.Contracts;

namespace EventGo.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly ILotePersistence _lotePersistence;
        private readonly IMapper _mapper;
        public LoteService(IGeralPersistence geralPersistence,
                            ILotePersistence lotePersistence,
                            IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _lotePersistence = lotePersistence;
            _mapper = mapper;


        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete não encontrado.");

                _geralPersistence.Delete<Lote>(lote);
                return await _geralPersistence.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO> GetLotebyIdsAsync(int eventoId, int loteIds)
        {
            try
            {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, loteIds);
                if (lote == null) return null;

                var result = _mapper.Map<LoteDTO>(lote);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var result = _mapper.Map<LoteDTO[]>(lotes);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddLote(int eventoId, LoteDTO model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _geralPersistence.Add<Lote>(lote);

                await _geralPersistence.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO[]> SaveLote(int eventoId, LoteDTO[] models)
        {
            try
            {
                var lotes = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _geralPersistence.Update<Lote>(lote);

                        await _geralPersistence.SaveChangesAsync();
                    }
                }

                var loteRetorno = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);

                return _mapper.Map<LoteDTO[]>(loteRetorno);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}