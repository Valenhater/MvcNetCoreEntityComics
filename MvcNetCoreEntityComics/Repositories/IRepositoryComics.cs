using MvcNetCoreEntityComics.Models;

namespace MvcNetCoreEntityComics.Repositories
{
    public interface IRepositoryComics
    {
        Task<List<ComicView>> GetComicsAsync();
        Task<ComicView> FindComicAsync(int idComic);
        Task<int> InsertComicAsync(string nombre, string imagen, string descripcion);
    }
}
