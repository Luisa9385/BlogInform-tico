using Articulos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articulos.BL
{
    public interface IArticuloService
    {
        Articulo GetOne(int Id);
        Comentario Details(int Id);
        IEnumerable<Articulo> GetAll();
        void Create(Articulo Model);
        void CreateComentario(Comentario Model);
        void Edit(Articulo Model);
        void EditComentario(Comentario Model);
        void Delete(int Id);
        void DeleteComentario(int Id);
        AnexGRIDResponde Listar(AnexGRID grid);
     
    }
}
