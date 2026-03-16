using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Notrox.Model
{
    public class UserAddressesClass
    {
        public List<AddressClass> shipping { get; set; } = new();
        public List<BillingAddressClass> billing { get; set; } = new();
    }
}
