using System;
using System.Collections.Generic;
using System.Linq;
using TechShop.entity;

namespace TechShop.dao
{
    public class SearchService
    {
        private readonly ProductService _productService;

        public SearchService()
        {
            _productService = new ProductService();
        }

        public List<Products> SearchProducts(string keyword)
        {
            var allProducts = _productService.GetAllProducts();
            return allProducts
                .Where(p =>
                    (!string.IsNullOrEmpty(p.ProductName) && p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        public List<Products> GetTopOrderedProducts(int topN = 5)
        {
            var allProducts = _productService.GetAllProducts();
            return allProducts
                .OrderByDescending(p => p.No_Of_Times_Ordered)
                .Take(topN)
                .ToList();
        }
    }
}
