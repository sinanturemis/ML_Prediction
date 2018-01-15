using PredictionApp.Service;
using System;
using System.Configuration.Abstractions;
using System.Threading.Tasks;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = ConfigurationManager.Instance.AppSettings.Map<Settings>();

                CustomerService customerService = new CustomerService(settings.DbSettings);
                FoodService foodService = new FoodService(settings.DbSettings);
                ProductService productService = new ProductService(settings.DbSettings);
                SupplierService supplierService = new SupplierService(settings.DbSettings);
                RestaurantService restaurantService = new RestaurantService(settings.DbSettings);
                StaffService staffService = new StaffService(settings.DbSettings);
                SupplyService supplyService = new SupplyService(settings.DbSettings);

                CustomerManager customerManager = new CustomerManager(customerService, foodService, restaurantService);
                MenuManager foodManager = new MenuManager(foodService, productService);
                ProductManager productManager = new ProductManager(productService, supplierService);
                RestaurantManager restaurantManager = new RestaurantManager(settings, restaurantService, staffService, customerService);
                SupplierManager supplierManager = new SupplierManager(supplierService);
                StockManager stockManager = new StockManager(restaurantService, productService, staffService, supplyService);




                restaurantManager.GenerateRestaurants(settings.GenerationCounts.RestaurantsCount);
                supplierManager.GenerateSuppliers(settings.GenerationCounts.SuppliersCount);
                customerManager.GenerateCustomers(settings.GenerationCounts.CustomersCount);
                customerManager.ClusterByLocation();
                productManager.GenerateProducts(settings.GenerationCounts.ProductTypesCount);
                foodManager.GenerateFoods(settings.GenerationCounts.FoodsCount);
                restaurantManager.HireStaff(settings.GenerationCounts.StaffsCount);
                stockManager.SetDefaultStockList();

                stockManager.InitializeStock(new DateTime(2017, 01, 01));

                //var firstDateOfSupply = new DateTime(2017, 01, 07);
                //var endDateOfYear = new DateTime(2017, 12, 31);

                //for (var currentDate = firstDateOfSupply; currentDate <= endDateOfYear; currentDate = currentDate.AddDays(1))
                //{
                //    stockManager.Supply(currentDate);
                //    customerManager.GenerateDailyVisits(currentDate);
                //}
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }

            System.Console.ReadKey();
        }
    }
}
