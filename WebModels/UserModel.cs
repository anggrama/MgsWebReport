using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PktViewModel
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public bool Ldap { get; set; }
        public bool Active { get; set; }
    }
   
}
