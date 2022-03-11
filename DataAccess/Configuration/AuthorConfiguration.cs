using Common.Entities;
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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //Otomatik olarak veri eklendiğinde GenreId Deriveded eşit olan 1 yazacaktır.
            builder.Property(e => e.GenreId).HasDefaultValueSql(((int)GenreEnum.Deriveded).ToString());
            builder.Property(c => c.CreateDate).HasDefaultValueSql("GETDATE()");
            //Seed eklemek için 
            builder.HasData(new Author()
            {
                Name = "Admin",
                GenreId = GenreEnum.Deriveded,
                Id=3
            });

            //property veri tipini berlirleme
            // builder.Property(x => x.Name).HasColumnType("date");
            builder.HasIndex(x => x.Name).HasName("IX_NameBook");
        }
    }
}
