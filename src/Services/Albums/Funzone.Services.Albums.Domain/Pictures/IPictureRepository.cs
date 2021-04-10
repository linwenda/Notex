using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funzone.Services.Albums.Domain.Pictures
{
    public interface IPictureRepository
    {
        Task<Picture> GetByIdAsync(PictureId id);
        
        Task AddAsync(Picture picture);

        void Delete(Picture picture);
    }
}