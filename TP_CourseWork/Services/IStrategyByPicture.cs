using Image = TP_CourseWork.Models.Image;

namespace TP_CourseWork.Services
{
    public interface IStrategyByPicture
    {
        public int GetObjectsCount(Image image);
    }
}
