using Hotel.BL.Model;

namespace Hotel.BL.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        IReadOnlyList<Customer> GetCustomers(string filter);
    }
}
