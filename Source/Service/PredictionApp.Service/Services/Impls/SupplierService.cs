using PredictionApp.Repository;
using System.Linq;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with suppliers
    /// </summary>
    public class SupplierService : ServiceBase
    {
        /// <summary>
        /// Repository for supplier table
        /// </summary>
        private SupplierRepository _supplierRepository;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public SupplierService(DbSettings dbSettings) : base(dbSettings)
        {
            _supplierRepository = new SupplierRepository(_dbSettings.ConnectionString);
        }

        /// <summary>
        /// Get suppliers
        /// </summary>
        /// <param name="request">request for getting suppliers</param>
        /// <returns>returns response of the operation</returns>
        public GetSupplierResponse Get(GetSupplierRequest request)
        {
            //get all suppliers
            var supplierEntities = _supplierRepository.Get();

            //Map entity to response dto
            var suppliers = supplierEntities.Select(x => new SupplierDTO
            {
                ID = x.ID,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                SatisfactionIndex = x.SatisfactionIndex
            });

            return new GetSupplierResponse
            {
                Suppliers = suppliers.ToList()
            };
        }

        /// <summary>
        /// Create suppliers
        /// </summary>
        /// <param name="request">request to create supplier</param>
        /// <returns>returns response of the operation</returns>
        public CreateSupplierResponse CreateSupplier(CreateSupplierRequest request)
        {
            //Map request to entity
            var supplierEntities = request.Suppliers.Select(staff => new SupplierEntity
            {
                ID = staff.ID,
                Latitude = staff.Latitude,
                Longitude = staff.Longitude,
                SatisfactionIndex = staff.SatisfactionIndex
            });

            //Add supplier on db
            _supplierRepository.Add(supplierEntities);

            return new CreateSupplierResponse();
        }
    }
}
