using Ex2.Models;

namespace Ex2
{
    class ProductListVM
    {
        private List<Product> products;
        public List<Product> Products
        {
            get
            {
                if (products == null)
                {
                    NorthwindContext northwindContext = new NorthwindContext();
                    products = northwindContext.Products.ToList();
                    return products;
                }
                else
                    return products;
            }
            set
            {
                products = value;
            }
        }
    }
}
