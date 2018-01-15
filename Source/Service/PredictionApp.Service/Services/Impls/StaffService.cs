using PredictionApp.Repository;
using System.Linq;

namespace PredictionApp.Service
{
    /// <summary>
    ///  Manages all transactional operations related with staffs
    /// </summary>
    public class StaffService : ServiceBase
    {
        /// <summary>
        /// Repository for staff table
        /// </summary>
        private StaffRepository _staffRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings"></param>
        public StaffService(DbSettings dbSettings) : base(dbSettings)
        {
            _staffRepository = new StaffRepository(_dbSettings.ConnectionString);
        }

        /// <summary>
        /// Creates staffs
        /// </summary>
        /// <param name="request">request to create staff</param>
        /// <returns>returns response of the operation</returns>
        public CreateStaffResponse CreateStaff(CreateStaffRequest request)
        {
            // Map request to entities
            var staffEntities = request.Staffs.Select(staff => new StaffEntity
            {
                ID = staff.ID,
                WorkplaceID = staff.WorkPlaceID
            });

            //Add staffs
            _staffRepository.Add(staffEntities);

            return new CreateStaffResponse();
        }

        /// <summary>
        /// Returns staffs
        /// </summary>
        /// <param name="request">request for getting staffs</param>
        /// <returns>returns response of the operation</returns>
        public GetStaffResponse Get(GetStaffRequest request)
        {
            //If WorkplaceID(restaurantId) exists in request, get staffs by workplaceId. Otherwise, get all staffs
            var staffEntities = request.WorkplaceID.HasValue ? _staffRepository.Get(request.WorkplaceID.Value) : _staffRepository.Get();

            return new GetStaffResponse
            {
                Staffs = staffEntities.Select(entity => new StaffDTO { ID = entity.ID, WorkPlaceID = entity.WorkplaceID }).ToList()
            };
        }
    }
}
