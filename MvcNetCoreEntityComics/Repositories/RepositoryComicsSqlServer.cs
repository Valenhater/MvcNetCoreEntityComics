using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEntityComics.data;
using MvcNetCoreEntityComics.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace MvcNetCoreEntityComics.Repositories
{
    #region Views and procedures
    //    create view V_COMICS 
    //as 
    //select isnull(COMICS.IDCOMIC, 0) as IDCOMIC, 
    //COMICS.NOMBRE, COMICS.IMAGEN, COMICS.DESCRIPCION
    //from COMICS
    //go

    //    create procedure SP_ALL_COMICS
    //as
    //	select* from V_COMICS
    //go
    //    create procedure SP_DETAILS_COMIC
    //(@idcomic int)
    //as 
    //    select* from COMICS
    //    where IDCOMIC=@idcomic
    //go

    //    CREATE PROCEDURE SP_INSERT_COMIC
    //    @Nombre NVARCHAR(100),
    //    @Imagen NVARCHAR(200),
    //    @Descripcion NVARCHAR(MAX)
    //AS
    //BEGIN
    //    DECLARE @NextId INT;
    //    SELECT @NextId = ISNULL(MAX(IDCOMIC), 0) + 1 FROM COMICS;
    //    INSERT INTO COMICS(IDCOMIC, NOMBRE, IMAGEN, DESCRIPCION)
    //    VALUES(@NextId, @Nombre, @Imagen, @Descripcion);
    //    SELECT @NextId;
    //    END
    #endregion
    public class RepositoryComicsSqlServer : IRepositoryComics
    {
        private ComicContext context;
        public RepositoryComicsSqlServer(ComicContext context) 
        { 
            this.context = context;
        }
        public async Task<List<ComicView>> GetComicsAsync()
        {
            string sql = "SP_ALL_COMIC";
            var consulta = this.context.ComicsView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
        public async Task<ComicView> FindComicAsync(int idComic)
        {
            string sql = "SP_DETAILS_COMIC @idcomic";
            SqlParameter pamId = new SqlParameter("@idcomic", idComic);
            var consulta = this.context.ComicsView
                .FromSqlRaw(sql, pamId);
            ComicView comic = consulta.AsEnumerable().FirstOrDefault();
            return comic;
        }

        public async Task<int> InsertComicAsync(string nombre, string imagen, string descripcion)
        {
            string sql = "SP_INSERT_COMIC @Nombre, @Imagen, @Descripcion";
            SqlParameter pamNombre = new SqlParameter("@Nombre", nombre);
            SqlParameter pamImagen = new SqlParameter("@Imagen", imagen);
            SqlParameter pamDescripcion = new SqlParameter("@Descripcion", descripcion);
            var result = await context.Database.ExecuteSqlRawAsync(sql, pamNombre, pamImagen, pamDescripcion);
            // Recuperar el ID del cómic insertado
            int newComicId = context.ComicsView.OrderByDescending(c => c.IdComic).Select(c => c.IdComic).FirstOrDefault();
            return newComicId;
        }
    }
}
