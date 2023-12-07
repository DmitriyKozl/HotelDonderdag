using Hotel.BL.Exceptions;
using Hotel.BL.Interfaces;
using Hotel.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch(Exception ex)
            {
                // Log the detailed exception message and stack trace for debugging
                Console.WriteLine(ex.ToString());
                throw new CustomerManagerException("Error in GetCustomers: " + ex.Message, ex);
            }
        }
    }
}
