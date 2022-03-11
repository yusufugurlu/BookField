using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // builder.HasOne(x => x.Author).WithMany(p => p.Books).HasConstraintName("FK_Books_Author_AuthorId");
            builder.HasOne(x => x.Author).WithMany(p => p.Books).HasForeignKey(x=>x.AuthorId).HasConstraintName("FK_Books_Author_AuthorId");
        }
    }
}
