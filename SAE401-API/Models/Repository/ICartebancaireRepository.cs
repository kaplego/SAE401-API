using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface ICartebancaireRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<Cartebancaire>>> GetAllCartebancaireByClientAsync(int idclient);
        Task<ActionResult<TEntity?>> GetCartebancaireByIdAsync(int idcartebancaire);
        Task AddCartebancaireAsync(TEntity entity);
        Task UpdateCartebancaireAsync(Cartebancaire cartebancaire, TEntity entity);
        Task DeleteCartebancaireAsync(Cartebancaire cartebancaire);
    }
}
