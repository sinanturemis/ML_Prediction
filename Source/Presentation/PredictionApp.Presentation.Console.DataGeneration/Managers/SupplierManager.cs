using PredictionApp.Common;
using PredictionApp.Common.Extension;
using PredictionApp.Common.Helpers;
using PredictionApp.Service;
using System;
using System.Collections.Generic;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    /// <summary>
    /// Manages supplier operations
    /// </summary>
    public class SupplierManager
    {
        #region Fields

        SupplierService _supplierService;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="supplierService">supplierService</param>
        public SupplierManager(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        #endregion Constructor

        #region Managers

        /// <summary>
        /// Produces new suppliers and add them to data source
        /// </summary>
        /// <param name="count">how many suppliers will be created</param>
        public void GenerateSuppliers(int count)
        {
            CreateSupplierRequest request = PrepareCreateSupplierRequest(count);
            _supplierService.CreateSupplier(request);
        }

        #endregion Managers

        #region Helpers

        /// <summary>
        /// Creates request that contains random generated supplier list
        /// </summary>
        /// <param name="count">how many supplier will be created</param>
        /// <returns>CreateSupplierRequest</returns>
        private CreateSupplierRequest PrepareCreateSupplierRequest(int count)
        {
            List<SupplierDTO> suppliers = new List<SupplierDTO>();
            for (int i = 0; i < count; i++)
            {
                suppliers.Add(GenerateSupplier());
            }
            return new CreateSupplierRequest() { Suppliers = suppliers };
        }

        /// <summary>
        /// Returns random generated supplierDto
        /// </summary>
        /// <returns>random generated supplierDto</returns>
        private SupplierDTO GenerateSupplier()
        {
            return new SupplierDTO
            {
                ID = Guid.NewGuid(),
                Latitude = RandomHelper.RandomDouble(Constants.LatitudeRange.Min, Constants.LatitudeRange.Max).ToGPSFormat(),
                Longitude = RandomHelper.RandomDouble(Constants.LongitudeRange.Min, Constants.LongitudeRange.Max).ToGPSFormat(),
                SatisfactionIndex = RandomHelper.RandomDouble(Constants.SatisfactionIndex.Min, Constants.SatisfactionIndex.Max).ToSatisfactionIndexFormat()
            };
        }


        #endregion Helpers
    }
}
