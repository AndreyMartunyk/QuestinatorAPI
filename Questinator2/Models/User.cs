using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questinator2.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}
