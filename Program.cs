using System;
using System.Data.SqlClient;

namespace ReadingDataWithSQLClient // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository customerrepository = new CustomerRepository();
            var result = customerrepository.GetAllCustomers();
            var getCustomerByID = customerrepository.GetSpecificCustomer(10);
            var getCustomerByName = customerrepository.GetCustomerByName($"Luis");
            var getPage = customerrepository.ReturnCustomerPage(10, 20);
            Customer newCustomer = new Customer { CustomerId = 1, FirstName = "Per Kristian", LastName = "Kronborg", Country = "Norway", PostalCode = "6230", Phone = "+47 47295789", Email = "pkkkk@gmail.com" };
            customerrepository.AddCustomer(newCustomer);
            customerrepository.UpdateCustomer($"Per Kristian", $"Wendy");
            customerrepository.TotalCustomerbyCountry();
            customerrepository.HighestSpender();
            customerrepository.GetFavoritGenre($"Kathy");
            Console.WriteLine();
        }

    }
}