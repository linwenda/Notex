using System.Threading.Tasks;

namespace Funzone.Services.Albums.Domain.PictureComments
{
    public interface IPictureCommentRepository
    {
        Task<PictureComment> GetByIdAsync(PictureCommentId id);

        void Delete(PictureComment pictureComment);
    }
}