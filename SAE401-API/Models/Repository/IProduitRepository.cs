using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IProduitRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitAsync();
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitByRechercheAsync(string str, int seuil);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitByRegroupementAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitByCategorieAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllProduitByTypeAsync(int id);
        Task<ActionResult<TEntity?>> GetProduitByIdAsync(int id);
        

        Task<Produit> AddProduitAsync(TEntity entity);
        Task<Produit> UpdateProduitAsync(TEntity entityToUpdate, ProduitDTO entity);
        Task DeleteProduitAsync(TEntity entity);
    }
}
