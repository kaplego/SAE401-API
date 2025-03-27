using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.DataManager
{
    public class PhotoManager : IPhotoRepository<Photo>
    {
        readonly _DBMilibooContext milibooContext;

        public PhotoManager() { }

        public PhotoManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<Photo?>> GetPhotoByIdAsync(int id)
        {
            return await milibooContext.Photos
                .Include(p => p.PhotoavisNavigation)
                .Include(p => p.PhotocolsNavigation)
                .Include(p => p.CategoriesNavigation)
                .FirstOrDefaultAsync(c => c.Idphoto == id);
        }

        public async Task AddPhotoAsync(Photo entity)
        {
            milibooContext.Photos.Add(entity);
            await milibooContext.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(Photo photo)
        {
            foreach (Photoavi pa in photo.PhotoavisNavigation)
            {
                milibooContext.Photoavis.Remove(pa);
            }
            foreach (Photocoloration pcl in photo.PhotocolsNavigation)
            {
                milibooContext.Photocolorations.Remove(pcl);
            }
            foreach (Categorieproduit c in photo.CategoriesNavigation)
            {
                c.Idphoto = null;
                milibooContext.Categorieproduits.Update(c);
            }
            milibooContext.Photos.Remove(photo);
            await milibooContext.SaveChangesAsync();
        }
    }
}
