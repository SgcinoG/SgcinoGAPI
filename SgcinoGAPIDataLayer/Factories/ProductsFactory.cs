using SgcinoGAPIDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataLayer.Factories
{
    public class ProductsFactory
    {
        private readonly string dbConnectionString;
        public ProductsFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public Products Create(Action<Products> initializer)
        {
            var newProducts = new Products(this.dbConnectionString);
            initializer(newProducts);
            return newProducts;
        }

    }
}
