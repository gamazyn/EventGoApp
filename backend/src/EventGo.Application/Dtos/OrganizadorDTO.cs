using System.Collections.Generic;

namespace EventGo.Application.Dtos
{
    public class OrganizadorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniBio { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocialDTO> RedeSociais { get; set; }
        public IEnumerable<OrganizadorDTO> Organizadores { get; set; }
    }
}