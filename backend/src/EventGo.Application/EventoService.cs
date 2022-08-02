using System;
using System.Threading.Tasks;
using AutoMapper;
using EventGo.Application.Contracts;
using EventGo.Application.Dtos;
using EventGo.Domain;
using EventGo.Persistence.Contracts;

namespace EventGo.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersistence geralPersistence,
                            IEventoPersistence eventoPersistence,
                            IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;


        }
        public async Task<EventoDTO> AddEventos(int userId, EventoDTO model)
        {

            try
            {
                var evento = _mapper.Map<Evento>(model);
                evento.UserId = userId;

                _geralPersistence.Add<Evento>(evento);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    var retorno = await _eventoPersistence.GetEventoByIdAsync(userId, evento.Id, false);

                    return _mapper.Map<EventoDTO>(retorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO> UpdateEvento(int userId, int eventoId, EventoDTO model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;


                _mapper.Map(model, evento);

                _geralPersistence.Update<Evento>(evento);

                if (await _geralPersistence.SaveChangesAsync())
                {
                    var retorno = await _eventoPersistence.GetEventoByIdAsync(userId, evento.Id, false);

                    return _mapper.Map<EventoDTO>(retorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) throw new Exception("Evento não foi encontrado!");

                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO[]> GetAllEventosAsync(int userId, bool includeOrganizadores = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(userId, includeOrganizadores);
                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDTO[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO[]> GetAllEventosByTemaAsync(int userId, string tema, bool includeOrganizadores = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(userId, tema, includeOrganizadores);
                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDTO[]>(eventos);

                return resultado;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDTO> GetEventoByIdAsync(int userId, int eventoId, bool includeOrganizadores = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(userId, eventoId, includeOrganizadores);
                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDTO>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}