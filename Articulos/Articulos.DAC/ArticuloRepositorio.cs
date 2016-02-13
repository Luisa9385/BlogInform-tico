using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Entity;
using Articulos.Entities;

namespace Articulos.DAC
{
    public class ArticuloRepositorio : IArticuloRepositorio
    {
        private string ConnectionString = null;
        public ArticuloRepositorio()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["Articulos"].ConnectionString;
        }
        public IEnumerable<Entities.Articulo> GetAll()
        {
            IEnumerable<Entities.Articulo> result = new List<Entities.Articulo>();
            using (var con = new SqlConnection(this.ConnectionString))
            {
                result = con.Query<Entities.Articulo>("usp_articulos_get", commandType: System.Data.CommandType.StoredProcedure);

            }
            return result;
        }

        public Entities.Articulo GetOne(int Id)
        {
            Entities.Articulo result = null;
            var context = new ArticuloDbContext();
            
            result = (from c in context.Articulos.Include("Comentarios")
                        where c.Id == Id    
                        select c).FirstOrDefault();

          

            //using (var con = new SqlConnection(this.ConnectionString))
            //{
            //    result = con.Query<Entities.Articulo>("usp_articulos_get_one", 
            //        new {Id = Id},
            //        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            //}
            return result;
        }

        public Comentario Details(int Id)
        {
            Comentario result = null;
            var context = new ArticuloDbContext();
            result = (from c in context.Comentarios
                      where c.Id == Id
                      select c).FirstOrDefault();

            return result;
        }

        public int Count()
        {
            int result = 0;
            using (var con = new SqlConnection(this.ConnectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "usp_articulos_count";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                result = (int)cmd.ExecuteScalar();
            }
            return result;
        }


        public void Create(Entities.Articulo Model)
        {
            var context = new ArticuloDbContext();
            context.Articulos.Add(Model);
            context.SaveChanges();
        }


        public void CreateComentario(Entities.Comentario Model)
        {
            var context = new ArticuloDbContext();
            context.Comentarios.Add(Model);
            context.SaveChanges();
        }


        public void Edit(Entities.Articulo Model)
        {
            var context = new ArticuloDbContext();
            context.Entry(Model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var context = new ArticuloDbContext();
            Articulo Model = context.Articulos.Find(Id);
            context.Articulos.Remove(Model);
            context.SaveChanges();
        }

        public void EditComentario(Comentario Model)
        {
            var context = new ArticuloDbContext();
            context.Entry(Model).State = EntityState.Modified;
            context.SaveChanges();
        }


        public void DeleteComentario(int id)
        {
            var context = new ArticuloDbContext();
            Comentario Model = context.Comentarios.Find(id);
            context.Comentarios.Remove(Model);
            context.SaveChanges();
        }


        public AnexGRIDResponde Listar(AnexGRID grid)
        {
            IEnumerable<Entities.Articulo> query = new List<Entities.Articulo>();
            //  Entities.Articulo query = null;
            //var context = new ArticuloDbContext();
            using (var con = new SqlConnection(this.ConnectionString))
            {
                query = con.Query<Entities.Articulo>("usp_articulos_get", commandType: System.Data.CommandType.StoredProcedure);

                if (grid.columna == "Id")
                {
                    query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Id)
                                                         : query.OrderBy(x => x.Id);
                }

                if (grid.columna == "Titulo")
                {
                    query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Titulo)
                                                         : query.OrderBy(x => x.Titulo);
                }

                if (grid.columna == "Autor")
                {
                    query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Autor)
                                                         : query.OrderBy(x => x.Autor);
                }

                if (grid.columna == "Contenido")
                {
                    query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Contenido)
                                                         : query.OrderBy(x => x.Contenido);
                }

                if (grid.columna == "Tags")
                {
                    query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Tags)
                                                         : query.OrderBy(x => x.Tags);
                }
                if (grid.columna == "Fecha")
                {
                    query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Fecha)
                                                         : query.OrderBy(x => x.Fecha);
                }

                // id, Nombre, Titulo, Desde, Hasta

                var Articulos = query.Skip(grid.pagina)
                                        .Take(grid.limite)
                                         .ToList();

                var total = query.Count();

                grid.SetData(Articulos, total);

                return grid.responde();
            }
        }         
                                    
    } 
    }
    
