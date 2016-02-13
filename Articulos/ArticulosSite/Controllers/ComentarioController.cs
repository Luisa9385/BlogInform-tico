using Articulos.BL;
using Articulos.DAC;
using Articulos.Entities;
using ArticulosSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticulosSite.Controllers
{
    [Authorize]
    public class ComentarioController : Controller
    {

        private IArticuloService ArticuloService;

        public ComentarioController()
        {
            var rep = new ArticuloRepositorio();
            var GrammarService = new GrammarService();
            this.ArticuloService = new ArticuloService(rep, GrammarService);
        }

        public ComentarioController(IArticuloService ArticuloService)
        {
            this.ArticuloService = ArticuloService;
        }

        // GET: Comentario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int Articulo)
        {
            Session["Articulo_id"] = Articulo;
            //ViewData["Articulo_id"] = Articulo.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int Articulo,ComentarioVM Model)
        {
            //[Bind(Include="Nombre,Contenido,Fecha")]
            if (this.ModelState.IsValid)
            {
                Model.Articulo_id = (int)Session["Articulo_id"];
                var input = AutoMapper.Mapper.Map<Comentario>(Model);
                this.ArticuloService.CreateComentario(input);
                Session["Articulo_id"] = null;
                return this.RedirectToAction("Details", "Articulos", new { id = Model.Articulo_id });
            }
            else
            {
                return View(Model);
            }
        }

        public ActionResult DetailsComent(int id)
        {
            var model = this.ArticuloService.Details(id);

            return this.View(model);
        }

        public ActionResult EditComentario(int id)
        {
            var model = this.ArticuloService.Details(id);
            return this.View(model);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditComentario(Comentario Model)
        {
            if (this.ModelState.IsValid)
            {
                this.ArticuloService.EditComentario(Model);
                return this.RedirectToAction("Details", "Articulos", new { id = Model.Articulo_id });
                
            }
            else
            {
                return View(Model);
            }
        }

        public ActionResult DeleteComentario(int id)
        {
            var model = this.ArticuloService.Details(id);
            //Session["IdArticulo"] = id;
            return this.View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteComentario")]
        public ActionResult DeleteConfirmed(int id)
        {
             //var valor = (int)Session["Articulo_id"];      
            var valor = (int)Session["Id"];
            if (this.ModelState.IsValid)
            {
                this.ArticuloService.DeleteComentario(id);
                Session["Id"] = null;
                return this.RedirectToAction("Details", "Articulos", new {Id = valor});
                
            }
            else
            {
                return View(id);
            }
            
        }
    }
}