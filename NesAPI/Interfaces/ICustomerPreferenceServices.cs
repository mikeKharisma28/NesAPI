using Mike.API.Entities;

namespace NesAPI.Interfaces
{
    public interface ICustomerPreferenceServices
    {
        public Task<List<Customer>> GetCustomerDetailsByEID(string eid);
    }
}
