using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataModels.Models
{
    public class ProductResponseCtrl : GenericResponse
    {
        public int OrderId { get; set; }
        public List<ProductsCtrl> Products;
        public double Total;
    }
}
