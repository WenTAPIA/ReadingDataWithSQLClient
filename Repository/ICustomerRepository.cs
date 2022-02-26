using ReadingDataWithSQLClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDataWithSQLClient
{
    public interface ICustomerRepository
    {
    
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Customer> GetSpecificCustomer(int id);
        IEnumerable<Customer> GetCustomerByName(string customerName);

        IEnumerable<Customer> ReturnCustomerPage(int limit, int offset);
        void AddCustomer(Customer customer);
        void UpdateCustomer(string firstName, string updateName);

        IEnumerable<Count> TotalCustomerbyCountry();

        IEnumerable<HighestInvoice> HighestSpender();

        IEnumerable<Count> GetFavoritGenre(string customerName);





    }
}
