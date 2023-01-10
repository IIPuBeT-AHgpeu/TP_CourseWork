using Microsoft.EntityFrameworkCore;

namespace TP_CourseWork.Models
{
    public class HistoryContext : DbContext
    {
        public DbSet<Recognize> Recognizes { get; set; }
        public HistoryContext(DbContextOptions<HistoryContext> options) : base(options)
        {
        }
    }
}
