using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public GenreEnum GenreId { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Book> Books { get; set; }
    }
}
