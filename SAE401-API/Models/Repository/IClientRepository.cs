using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.Repository
{
    public interface IClientRepository<TEntity>
    {
        Task<TEntity?> GetClientByLoginAsync(string email, string password);

        Task<ActionResult<TEntity?>> GetClientByIdAsync(int id);

        Task AddClientAsync(TEntity entity);
        Task UpdateClientAsync(TEntity entityToUpdate, ClientDTO entity);
    }
}
