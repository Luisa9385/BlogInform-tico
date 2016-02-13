using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;


namespace Articulos.Entities
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        [AllowHtml]
        public string Contenido { get; set; }
        public string Tags { get; set; }
        public string Fecha { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
        
