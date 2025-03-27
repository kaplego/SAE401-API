using SAE401_API.Models.Repository;
using SAE401_API.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DataManager
{
    public class PhotoAvisManager<TEntity> : IPhotoAvisRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public PhotoAvisManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<Avisproduit?>> GetAvisByIdAsync(int id)
        {
            return await _milibooContext.Avisproduits
                .FirstOrDefaultAsync(c => c.Idavis == id);
        }


        public async Task AddPhotoAvisAsync(TEntity entity)
        {
            if (entity is Photoavi photoavis)
            {
                _milibooContext.Photoavis.Add(photoavis);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}
