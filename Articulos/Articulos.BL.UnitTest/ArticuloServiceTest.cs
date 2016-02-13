using Articulos.DAC;
using Articulos.Entities;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articulos.BL.UnitTest
{
    [TestFixture]
    public class ArticuloServiceTest
    {
        [Test]
        public void CreateComentarioTest()
        {
            var model = new Comentario();
            model.Id = 0;
            model.Articulo = new Articulos.Entities.Articulo { Id = 0, Titulo = "Test", Autor = "Test", Contenido = "test", Tags = "Test", Fecha = "Test" };
            model.Nombre = "Luisa";
            model.Contenido = "this is a great article";
            model.Fecha = "2013-09-09";

            var repository = Substitute.For<IArticuloRepositorio>();

            var grammar = Substitute.For<IGrammarService>();

            var badwords = new List<String>() { "culebras" };

            grammar.GetBadWords().Returns(badwords);

            //var service = new ArticuloService(repository, grammar);

            var service = new ArticuloService(repository, grammar);

            service.CreateComentario(model);

            //Assert
            repository.Received().CreateComentario(model);

        }
        [Test]
        public void CreateComentarioTestFail()
        {
            var model = new Comentario();
            model.Id = 0;
            model.Articulo = new Articulos.Entities.Articulo { Id = 0, Titulo = "Test", Autor = "Test", Contenido = "test", Tags = "Test", Fecha = "Test" };
            model.Nombre = "Luisa";
            model.Contenido = "this is a great article  culebras";
            model.Fecha = "2011-02-02";

            var repository = Substitute.For<IArticuloRepositorio>();

            var grammar = Substitute.For<IGrammarService>();

            var badwords = new List<String>() { "culebras" };

            grammar.GetBadWords().Returns(badwords);

            var service = new ArticuloService(repository, grammar);

            try
            {
                service.CreateComentario(model);

                Assert.Fail("the validation is not complete");
            }
            catch (ApplicationException)
            {

            }
            //Assert
            repository.DidNotReceive().CreateComentario(model);

        }



    }
}
