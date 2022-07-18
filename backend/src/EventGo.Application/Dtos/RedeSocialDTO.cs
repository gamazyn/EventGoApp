namespace EventGo.Application.Dtos
{
    public class RedeSocialDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public EventoDTO Evento { get; set; }
        public int? OrganizadorId { get; set; }
        public OrganizadorDTO Organizador { get; set; }
    }
}