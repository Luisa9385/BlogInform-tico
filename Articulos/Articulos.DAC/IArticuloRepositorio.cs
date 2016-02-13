using Articulos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Articulos.DAC
{
    public interface IArticuloRepositorio
    {
        IEnumerable<Articulo> GetAll();
        Articulo GetOne(int Id);
        Comentario Details(int Id);
        void Create(Articulo Model);
        void CreateComentario(Comentario Model);
        void Edit(Articulo Model);
        void EditComentario(Comentario Model);
        void Delete(int Id);
        // ---
        void DeleteComentario(int id);
        int Count();
        AnexGRIDResponde Listar(AnexGRID grid);
        
    }
}
