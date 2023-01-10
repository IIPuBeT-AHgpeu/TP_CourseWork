namespace TP_CourseWork.Models
{
    public interface IPostgreSQLRepository
    {
        void Create(Recognize item);
        void Delete(int id);
        IEnumerable<Recognize> GetAll();
        Recognize GetOne(int id);
        void Save();
        void Update(Recognize item);
    }
}