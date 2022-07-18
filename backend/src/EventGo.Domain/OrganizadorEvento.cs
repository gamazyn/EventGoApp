using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGo.Domain
{
    public class OrganizadorEvento
    {
        public int OrganizadorId { get; set; }
        public Organizador Organizador { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}