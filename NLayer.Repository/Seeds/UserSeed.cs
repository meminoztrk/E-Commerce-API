using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasData(new User { Id = 1, Name = "Admin Oz", Username = "admin", Password = "123", Email = "admin@admin.com", CreatedDate = DateTime.Now },
            //                new User { Id = 2, Fullname = "Memin Oz" ,Username = "memin", Password = "123", Email = "memin@memin.com", CreatedDate = DateTime.Now });
        }
    }
}
