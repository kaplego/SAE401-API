using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface ICartebancaireRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<Cartebancaire>>> GetAllCartebancaireByClientAsync(int idclient);
        Task<ActionResult<TEntity?>> GetCartebancaireByIdAsync(int idcartebancaire);
        Task<Cartebancaire> AddCartebancaireAsync(TEntity entity);
        Task<Cartebancaire> UpdateCartebancaireAsync(Cartebancaire cartebancaire, CartebancaireDTO entity);
        Task DeleteCartebancaireAsync(Cartebancaire cartebancaire);
    }
}
