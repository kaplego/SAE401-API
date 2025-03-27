using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class DetailcommandeManager<TEntity> : IDetailcommandeRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public DetailcommandeManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }


        public async Task AddDetailcommandeAsync(TEntity entity)
        {
            if (entity is Detailcommande detailcommande)
            {
                _milibooContext.Detailcommandes.Add(detailcommande);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }


    }
}
