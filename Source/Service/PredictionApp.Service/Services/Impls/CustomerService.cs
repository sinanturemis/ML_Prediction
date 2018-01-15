using PredictionApp.Common.Helpers;
using PredictionApp.Repository;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with customers
    /// </summary>
    public class CustomerService : ServiceBase
    {
        /// <summary>
        /// Repository for customer table
        /// </summary>
        private CustomerRepository _customerRepository;

        /// <summary>
        ///  Repository for customer visit transaction table
        /// </summary>
        private CustomerVisitTransactionRepository _customerVisitRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public CustomerService(DbSettings dbSettings) : base(dbSettings)
        {
            _customerRepository = new CustomerRepository(dbSettings.ConnectionString);
            _customerVisitRepository = new CustomerVisitTransactionRepository(dbSettings.ConnectionString);
        }

        /// <summary>
        /// Returns filtered customers by request parameter
        /// </summary>
        /// <param name="request">request for getting customers</param>
        /// <returns>returns response of the operation</returns>
        public GetCustomerResponse Get(GetCustomerRequest request)
        {
            //Convert entity to request
            var customers = _customerRepository.Get().Select(customer => new CustomerDTO
            {
                ID = customer.ID,
                Age = customer.Age,
                GenderType = customer.GenderType,
                Latitude = customer.Latitude,
                Longitude = customer.Longitude,
                LocationGroupId = customer.LocationGroupId
            }).ToList();

            //Return response
            return new GetCustomerResponse()
            {
                Customers = customers
            };
        }

        /// <summary>
        /// Creates customer
        /// </summary>
        /// <param name="request">request to create customer</param>
        /// <returns>returns response of the operation</returns>
        public CreateCustomerResponse CreateCustomer(CreateCustomerRequest request)
        {
            //Convert request to entity
            var customers = request.CustomersToCreate.Select(customerToCreate => new CustomerEntity
            {
                ID = customerToCreate.ID,
                Age = customerToCreate.Age,
                GenderType = customerToCreate.GenderType,
                Latitude = customerToCreate.Latitude,
                Longitude = customerToCreate.Longitude,
                LocationGroupId = customerToCreate.LocationGroupId,
                VisitCountInYear = default(int)
            });

            //Execute db operation
            _customerRepository.Add(customers);

            //Return response
            return new CreateCustomerResponse();
        }

        /// <summary>
        /// Adds customer visit
        /// </summary>
        /// <param name="request">request to create visit transaction</param>
        /// <returns>returns response of the operation</returns>
        public CreateVisitResponse Visit(CreateVisitRequest request)
        {
            CreateVisitResponse response = new CreateVisitResponse();

            //Convert request to entity
            var visitTransactionEntities = request.CustomerIDList.Select(customerId => new CustomerVisitTransactionEntity
            {
                ID = Guid.NewGuid(),
                CustomerID = customerId,
                ReservationID = request.ReservationID,
                DateIn = request.DateIn
            }).ToList();

            //Start a new transaction scope
            using (TransactionScope scope = CreateTransactionScope())
            {
                //Add visit transaction
                _customerVisitRepository.Add(visitTransactionEntities);

                //when a customer visits a restaurant, visit count of the customer must be increased to track visit count
                _customerRepository.IncreaseVisitCount(request.CustomerIDList);

                //Commit the transaction
                scope.Complete();
            }

            //Return response
            return new CreateVisitResponse
            {
                VisitTransactionIdList = visitTransactionEntities.Select(transaction => transaction.ID).ToList()
            };
        }

        /// <summary>
        /// Update visit record as leave
        /// </summary>
        /// <param name="request">request to leave</param>
        /// <returns>returns response of the operation</returns>
        public CreateLeaveTransactionResponse Leave(CreateLeaveTransactionRequest request)
        {
            //Get visit record
            var entity = _customerVisitRepository.GetById(request.VisitTransactionId);

            //Update leave fields of entity 
            entity.DateOut = request.DateOut;
            entity.SatisfactionFeedback = RandomHelper.RandomString(15);

            //update visit transaction
            _customerVisitRepository.Update(entity);

            //Return response
            return new CreateLeaveTransactionResponse();
        }

        /// <summary>
        /// Updates customer
        /// </summary>
        /// <param name="request">updated customer request</param>
        /// <returns>returns the response of the operation</returns>
        public UpdateCustomerResponse Update(UpdateCustomerRequest request)
        {
            //Convert request to entity
            var entity = new CustomerEntity
            {
                ID = request.ID,
                GenderType = request.GenderType,
                Age = request.Age,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                VisitCountInYear = request.VisitCountInYear,
                LocationGroupId = request.LocationGroupId
            };

            //update customer
            _customerRepository.Update(entity);

            //Return response
            return new UpdateCustomerResponse();
        }
    }
}
