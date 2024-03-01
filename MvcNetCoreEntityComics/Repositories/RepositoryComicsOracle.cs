using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEntityComics.data;
using MvcNetCoreEntityComics.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace MvcNetCoreEntityComics.Repositories
{
    #region procedures
    //    create or replace procedure SP_ALL_COMICS
    //(p_cursor_comics out sys_refcursor)
    //as
    //begin
    //  open p_cursor_comics for
    //select* from COMICS;
    //end;
    //create or replace procedure SP_DETAILS_COMIC
    //(p_cursor_comics out sys_refcursor,
    //p_idcomic COMICS.IDCOMIC%TYPE)
    //as 
    //begin
    //  open p_cursor_comics for 
    //  select* from COMICS
    //  where IDCOMIC=p_idcomic;
    //end;
    #endregion
    public class RepositoryComicsOracle : IRepositoryComics
    {
        private ComicContext context;
        public RepositoryComicsOracle(ComicContext context) 
        { 
            this.context = context;
        }
        public async Task<List<ComicView>> GetComicsAsync()
        {
            string sql = "begin ";
            sql += " SP_ALL_COMICS(:p_cursor_comics);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_comics";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            //COMO ES UN TIPO DE ORACLE PROPIO (Cursor) DEBEMOS
            //PONERLO DE FORMA MANUAL
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.ComicsView
                .FromSqlRaw(sql, pamCursor);
            return await consulta.ToListAsync();
        }
        public async Task<ComicView> FindComicAsync(int idComic)
        {
            string sql = "begin ";
            sql += " SP_DETAILS_COMIC (:p_cursor_comics, :p_idcomic);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_comics";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            OracleParameter pamId = new OracleParameter("p_idcomic", idComic);
            var consulta = this.context.ComicsView
            .FromSqlRaw(sql, pamCursor, pamId);
            ComicView comic = consulta.AsEnumerable().FirstOrDefault();
            return comic;
        }

        public Task<int> InsertComicAsync(string nombre, string imagen, string descripcion)
        {
            throw new NotImplementedException();
        }
    }
}
