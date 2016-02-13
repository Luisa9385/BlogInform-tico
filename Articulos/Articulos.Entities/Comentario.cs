using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articulos.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public int? Articulo_id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public string Fecha { get; set; }

        public virtual Articulo Articulo { get; set; }

    }
}
