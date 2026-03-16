using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notrox.Model
{
    public class AddressClass
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Address1 { get; set; }
        public int UserId { get; set; }
    }
}
