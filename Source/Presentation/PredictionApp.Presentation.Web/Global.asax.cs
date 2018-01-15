using PredictionApp.Service;
using System;
using System.Configuration.Abstractions;
using System.Web.Mvc;
using System.Web.Routing;

namespace PredictionApp.Presentation.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            try
            {
                var settings = ConfigurationManager.Instance.AppSettings.Map<Settings>();
                //RestaurantService restaurantService = new RestaurantService(settings.PredictionApplicationConnectionString);
                //StaffService staffService = new StaffService(settings.PredictionApplicationConnectionString);
                //SupplierService supplierService = new SupplierService(settings.PredictionApplicationConnectionString);
                //ProductService productService = new ProductService(settings.PredictionApplicationConnectionString);
                //FoodService foodService = new FoodService(settings.PredictionApplicationConnectionString);
                //CustomerService customerService = new CustomerService(settings.DbSettings.ConnectionString);
                //CustomerVisitService customerVisitService = new CustomerVisitService(settings);

                //restaurantService.CreateRestaurant(10);
                //staffService.CreateStaff(100);
                //supplierService.CreateSupplier(10);
                //productService.CreateProduct(100);
                //foodService.CreateFood(100);
                //customerService.CreateCustomer(5000);

                //customerVisitService.Visit();
                //customerService.Get();

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
