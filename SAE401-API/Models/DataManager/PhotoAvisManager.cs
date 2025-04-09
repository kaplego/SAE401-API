using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

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


        public async Task<Photoavi> AddPhotoAvisAsync(TEntity entity)
        {
            if (entity is Photoavi photoavis)
            {
                await _milibooContext.Photoavis.AddAsync(photoavis);
                await _milibooContext.SaveChangesAsync();
                return photoavis;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}
