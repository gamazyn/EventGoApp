namespace EventGo.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
        public int? OrganizadorId { get; set; }
        public Organizador Organizador { get; set; }
    }
}