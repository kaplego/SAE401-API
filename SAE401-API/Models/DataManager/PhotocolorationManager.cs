using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class PhotocolorationManager<TEntity> : IPhotocolorationRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public PhotocolorationManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<Photocoloration> AddPhotocolorationAsync(TEntity entity)
        {
            if (entity is Photocoloration photocoloration)
            {
                await _milibooContext.Photocolorations.AddAsync(photocoloration);
                await _milibooContext.SaveChangesAsync();
                return photocoloration;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}
