using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticulosSite.Models
{
    public class ComentarioVM
    {
        public int Id { get; set; }
        public int? Articulo_id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }
}