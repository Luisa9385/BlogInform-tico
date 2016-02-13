using Articulos.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articulos.BL
{
    public class ArticuloService : IArticuloService
    {
        private DAC.IArticuloRepositorio ArticuloRepositorio;
        private DAC.IGrammarService GrammarService;


        public ArticuloService(IArticuloRepositorio ArticuloRepositorio,
            IGrammarService GrammarService
            )
        {
            this.GrammarService = GrammarService;
            this.ArticuloRepositorio = ArticuloRepositorio;
        }


        public IEnumerable<Entities.Articulo> GetAll()
        {
            return this.ArticuloRepositorio.GetAll();
        }


        public void Create(Entities.Articulo Model)
        {
            this.ArticuloRepositorio.Create(Model);
            
        }

        public Entities.Articulo GetOne(int Id)
        {
            return this.ArticuloRepositorio.GetOne(Id);
        }


        public void CreateComentario(Entities.Comentario Model)
        {
            var words = this.GrammarService.GetBadWords();
            var badwords = words.Where(c => Model.Contenido.Contains(c)).ToList();
            if (badwords.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in badwords)
                {
                    sb.AppendLine(item);
                }


                throw new ApplicationException(
                    string.Format("this comment is not valid, because contains this bad word {0}",
                        sb.ToString()
                    ));
            }
            this.ArticuloRepositorio.CreateComentario(Model);
        }

        public void Edit(Entities.Articulo Model)
        {
            this.ArticuloRepositorio.Edit(Model);
        }

        public void Delete(int Id)
        {
            this.ArticuloRepositorio.Delete(Id);
        }

        public Entities.Comentario Details(int Id)
        {
            return this.ArticuloRepositorio.Details(Id);
        }

        public void EditComentario(Entities.Comentario Model)
        {
            this.ArticuloRepositorio.EditComentario(Model);
        }


        public void DeleteComentario(int Id)
        {
            this.ArticuloRepositorio.DeleteComentario(Id);
        }


        public Entities.AnexGRIDResponde Listar(Entities.AnexGRID grid)
        {
            return this.ArticuloRepositorio.Listar(grid);
        }
    }
}
