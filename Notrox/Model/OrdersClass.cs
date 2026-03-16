using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notrox.Model
{
    public class OrdersClass
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Phase { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AddressId { get; set; }
    }
}
