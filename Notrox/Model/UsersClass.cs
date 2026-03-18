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

        public string PfpFull
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Pfp)) return "https://i.pravatar.cc/150";

                if (Pfp.StartsWith("http")) return Pfp;

                return "https://notrox.hu" + Pfp;
            }
        }

        [JsonIgnore]
        public bool isAdmin { get; set; }

    }
}
