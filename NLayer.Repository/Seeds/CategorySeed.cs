using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = 1, Name = "Elektronik", SubId = 1 },
                            new Category { Id = 2, Name = "Ev, Bahçe, Ofis, Yapı Market", SubId = 2 },
                            new Category { Id = 3, Name = "Kozmetik, Kişisel Bakım", SubId = 3 }, 
                            new Category { Id = 4, Name = "Anne, Bebek", SubId = 4 }, 
                            new Category { Id = 5, Name = "Süpermarket, Evcil Hayvan", SubId = 5 },
                            new Category { Id = 6, Name = "Kitap, Müzik, Oyuncak, Hobi", SubId = 6 },
                             new Category { Id = 7, Name = "Otomobil, Motosiklet", SubId = 7 },
                             new Category { Id = 8, Name = "Spor, Outdoor", SubId = 8 });
        }
    }
}
