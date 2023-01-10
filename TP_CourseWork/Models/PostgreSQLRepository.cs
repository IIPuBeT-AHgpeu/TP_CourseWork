using Microsoft.EntityFrameworkCore;

namespace TP_CourseWork.Models
{
    public class PostgreSQLRepository : IRepository<Recognize>
    {
        private HistoryContext _db;

        public PostgreSQLRepository(HistoryContext context)
        {
            _db = context;
        }

        public void Create(Recognize item)
        {
            _db.Recognizes.Add(item);
        }

        public void Delete(int id)
        {
            Recognize recognize = _db.Recognizes.Find(id);
            if (recognize != null)
                _db.Recognizes.Remove(recognize);
        }

        public IEnumerable<Recognize> GetAll()
        {
            return _db.Recognizes;
        }

        public Recognize GetOne(int id)
        {
            return _db.Recognizes.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Recognize item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
