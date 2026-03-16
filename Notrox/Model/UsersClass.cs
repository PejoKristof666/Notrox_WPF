using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Notrox.Model
{
    public class UsersClass
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Pfp { get; set; }

        [JsonIgnore]
        public bool isAdmin { get; set; }

    }
}
