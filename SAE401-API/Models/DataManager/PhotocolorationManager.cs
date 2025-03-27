using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class PhotocolorationManager<TEntity> : IPhotocolorationRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public PhotocolorationManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task AddPhotocolorationAsync(TEntity entity)
        {
            if (entity is Photocoloration photocoloration)
            {
                _milibooContext.Photocolorations.Add(photocoloration);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}
