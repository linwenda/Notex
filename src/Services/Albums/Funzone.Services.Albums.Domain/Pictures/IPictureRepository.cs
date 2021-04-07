using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funzone.Services.Albums.Domain.Pictures
{
    public interface IPictureRepository
    {
        Task<Picture> GetById(PictureId id);
        
        Task AddAsync(Picture picture);
    }
}