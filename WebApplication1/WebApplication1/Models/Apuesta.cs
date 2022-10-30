using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Apuesta
    {
        public int ApuestaId { get; set; }

        public int ApuestaGolesSeleccion1 { get; set; }

        public int ApuestaGolesSeleccion2 { get; set; }

        public int ApuestaPartidoId { get; set; }

    }
}
