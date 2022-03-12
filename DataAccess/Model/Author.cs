using Common.Entities;


namespace DataAccess.Model
{
    public class Author: BaseEntities
    {
        public string Name { get; set; }
        public GenreEnum GenreId { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
