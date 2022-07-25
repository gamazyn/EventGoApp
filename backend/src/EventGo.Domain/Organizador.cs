using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Domain.Identity;

namespace EventGo.Domain
{
    public class Organizador
    {
        public int Id { get; set; }
        public string MiniBio { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public IEnumerable<RedeSocial> RedeSociais { get; set; }
        public IEnumerable<OrganizadorEvento> OrganizadoresEventos { get; set; }

    }
}