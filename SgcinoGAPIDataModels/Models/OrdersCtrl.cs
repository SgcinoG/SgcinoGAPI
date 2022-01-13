using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataModels.Models
{
    public class OrdersCtrl
    {
        public int Id { get; set; }
        public List<ProductsCtrl> Products { get; set; }
        public DateTime Created { get; set; }
        public int OrderNum { get; set; }
        public string UserId { get; set; }
    }
}
