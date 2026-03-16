using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notrox.Model
{
    public class ProductsClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Ammount { get; set; }
        public int CompanyId { get; set; }
        public string IMGURL { get; set; }

        public CompanyClass Company { get; set; }
    }
}
