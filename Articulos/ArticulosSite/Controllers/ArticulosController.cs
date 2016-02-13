using Articulos.BL;
using Articulos.DAC;
using Articulos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticulosSite.Controllers
{
    [Authorize]
    public class ArticulosController : Controller
    {
        private Articulos.BL.IArticuloService ArticuloService;

        public ArticulosController()
        {
            var rep = new ArticuloRepositorio();
            var GrammarService = new GrammarService();
            this.ArticuloService = new ArticuloService(rep, GrammarService);
        }

        public ArticulosController(IArticuloService ArticuloService)
        {
            this.ArticuloService = ArticuloService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarArticulo(AnexGRID grid ){
        return Json(ArticuloService.Listar(grid));
        }


        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult Details(int id)
        {
            Session["Id"] = id; 
            var model = this.ArticuloService.GetOne(id);
            return this.View(model);

        }
        //[ValidateInput(false)] // es una manera facil pero vulnerable a la seguridad por que le das a toda la accion mejor en el atributo
        // allowhtml
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Articulo Model)
        {
            if (this.ModelState.IsValid)
            {
                this.ArticuloService.Create(Model);
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(Model);
            }
        }

        public ActionResult Edit(int id)
        {
            var model = this.ArticuloService.GetOne(id);
            return this.View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Articulo Model)
        {
            if (this.ModelState.IsValid)
            {
                this.ArticuloService.Edit(Model);
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(Model);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = this.ArticuloService.GetOne(id);
            return this.View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (this.ModelState.IsValid)
            {
                this.ArticuloService.Delete(id);
                return this.RedirectToAction("Index");
            }
            else
            {
                return View(id);
            }
        }
    }
}