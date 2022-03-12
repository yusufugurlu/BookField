using System;
using System.Collections.Generic;


namespace DataAccess.Model
{
    public class Book : BaseEntities
    {
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
