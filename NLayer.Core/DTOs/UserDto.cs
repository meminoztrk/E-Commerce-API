using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class UserDto:BaseDto
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
