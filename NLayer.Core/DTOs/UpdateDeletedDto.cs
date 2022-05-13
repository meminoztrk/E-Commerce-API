using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class UpdateDeletedDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
