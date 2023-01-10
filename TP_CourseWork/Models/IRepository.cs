namespace TP_CourseWork.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(); // получение всех объектов
        T GetOne(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
