using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.Repository
{
    public interface IProduitRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitAsync();
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitByRechercheAsync(string str, int seuil);
        Task<ActionResult<TEntity>> GetProduitByIdAsync(int id);
        

        Task AddProduitAsync(TEntity entity);
        Task UpdateProduitAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteProduitAsync(TEntity entity);
    }
}
