using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IClientRepository<TEntity>
    {
        Task<TEntity?> GetClientByLoginAsync(string email, string password);

        Task<ActionResult<TEntity?>> GetClientByIdAsync(int id);

        Task<Client> AddClientAsync(TEntity entity);
        Task<Client> UpdateClientAsync(TEntity entityToUpdate, ClientDTO entity);
    }
}
