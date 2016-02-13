using Articulos.Entities;
using ArticulosSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticulosSite.App_Start
{
    public class MapperConfig
    {
        public static void Config()
        {
            AutoMapper.Mapper.CreateMap<ComentarioVM, Comentario>();
            AutoMapper.Mapper.CreateMap<Comentario, ComentarioVM>();
        }
    }
}