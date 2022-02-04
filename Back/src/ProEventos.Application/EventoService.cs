using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Domain;
using ProEventos.Application.Dtos;
using ProEventos.Application.Contratos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersit;
        private readonly IEventoPersist _eventoPersit;
        private readonly IMapper _mapper;

        public EventoService(
            IGeralPersist geralPersist, 
            IEventoPersist eventoPersist,
            IMapper mapper)
        {
            _geralPersit = geralPersist;
            _eventoPersit = eventoPersist;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(int userId, EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                evento.UserId = userId;

                _geralPersit.Add<Evento>(evento);

                if (await _geralPersit.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersit.GetEventoByIdAsync(userId, evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersit.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;

                _mapper.Map(model, evento);
                _geralPersit.Update<Evento>(evento);

                if (await _geralPersit.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersit.GetEventoByIdAsync(userId, evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
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
                var evento = await _eventoPersit.GetEventoByIdAsync(userId, eventoId, false);

                if (evento == null) throw new Exception("Evento para delete não encontrado");

                _geralPersit.Delete<Evento>(evento);
                return await _geralPersit.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(int userId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersit.GetAllEventosAsync(userId, includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersit.GetAllEventosByTemaAsync(userId, tema, includePalestrantes);

                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersit.GetEventoByIdAsync(userId, eventoId, includePalestrantes);

                if (evento == null) return null;

                var resultado = _mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
