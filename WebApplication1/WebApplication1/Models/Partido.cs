using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Partido
    {
        public int PartidoId { get; set; }

        public string PartidoFecha { get; set; }

        public int PartidoGolesSeleccion1 { get; set; }

        public int PartidoGolesSeleccion2 { get; set; }

        public int PartidoSedesId { get; set; }

    }
}
