using PredictionApp.Common;
using PredictionApp.Common.Helpers;
using PredictionApp.Service;
using System;
using System.Collections.Generic;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    public class ProductManager
    {
        #region Fields

        /// <summary>
        /// a service to manage product operations on datasource 
        /// </summary>
        ProductService _productService;

        /// <summary>
        /// a service to manage supplier operations on datasource 
        /// </summary>
        SupplierService _supplierService;

        #endregion Fields

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService">productService</param>
        /// <param name="supplierService">supplierService</param>
        public ProductManager(ProductService productService, SupplierService supplierService)
        {
            _productService = productService;
            _supplierService = supplierService;
        }

        #endregion Constructor

        #region Managers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        public void GenerateProducts(int count)
        {
            var suppliers = _supplierService.Get(new GetSupplierRequest());

            CreateProductRequest request = new CreateProductRequest() { Products = PrepareProducts(count, suppliers.Suppliers) };

            var response = _productService.CreateProduct(request);
            if (response == null)
                throw new Exception("Unsuccessful Create Operation");

        }

        #endregion Managers

        #region Helpers
        private List<ProductDTO> PrepareProducts(int count, List<SupplierDTO> suppliers)
        {
            var products = new List<ProductDTO>();

            for (int i = 0; i < count; i++)
            {
                products.Add(GenerateProduct(suppliers));
            }

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppliers"></param>
        /// <returns></returns>
        private ProductDTO GenerateProduct(List<SupplierDTO> suppliers)
        {
            var supplierIndex = RandomHelper.RandomInteger(0, suppliers.Count);
            return new ProductDTO
            {
                ID = Guid.NewGuid(),
                SupplierID = suppliers[supplierIndex].ID,
                Name = RandomHelper.RandomString(10),
                UnitPrice = RandomHelper.RandomDouble(Constants.ProductUnitPrice.Min, Constants.ProductUnitPrice.Max)
            };

        }

        #endregion Helpers
    }
}
