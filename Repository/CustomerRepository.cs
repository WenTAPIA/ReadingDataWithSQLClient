using ReadingDataWithSQLClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDataWithSQLClient
{
    public class CustomerRepository : ICustomerRepository
    {
        //This methode will read and print  specific information for all the customers in DB customers 
        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {

                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                var sql = "SELECT * FROM Customer";
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                Console.WriteLine($"ID FirstName tLast Name  Country  PostalCode  Phone\t\tEmail\t\t");
                Console.WriteLine($"-----------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    {
                        customer.CustomerId = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.Country = reader[7] as string;
                        customer.PostalCode = reader[8] as string;
                        customer.Phone = reader[9] as string;
                        customer.Email = reader.GetString(11);

                        Console.WriteLine($"{customer.CustomerId}\t {customer.FirstName}\t {customer.LastName}\t\t{customer.Country}\t\t {customer.PostalCode}\t\t{customer.Phone}\\t\tt{customer.Email}\t");
                    };

                    customers.Add(customer);
                }
            }
            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }

            return customers;

        }

         //This methode will read and print  specific information for an specific customers in DB customers 
        public IEnumerable<Customer> GetSpecificCustomer(int id)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                Customer customer = new Customer();
                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                var sql = "SELECT *  FROM Customer WHERE CustomerId = @Id";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                using SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                Console.WriteLine($"ID FirstName tLast Name  Country  PostalCode  Phone\t\tEmail\t\t");
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                        customer.CustomerId = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.Country = reader[7] as string;
                        customer.PostalCode = reader[8] as string;
                        customer.Phone = reader[9] as string;
                        customer.Email = reader.GetString(11);

                        Console.WriteLine($"{customer.CustomerId} {customer.FirstName} {customer.LastName}\t\t{customer.Country}\t\t {customer.PostalCode}\t\t{customer.Phone}\\t\tt{customer.Email}\t");
                   
                    customers.Add(customer);
                }
            }
            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }

            return customers;

        }

        //This methode will return and print a customer by Name 
        public IEnumerable<Customer> GetCustomerByName(string customerName)
        {
            List<Customer> customers = new List<Customer>();
            
            try
            {
                Customer customer = new Customer();
                string sql = "SELECT * FROM Customer WHERE FirstName LIKE @FirstName";
                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstName", customerName);
                using SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"-----This is the information of the user you are looking for--------------------------");
              
                while (reader.Read())
                {
                      customer.CustomerId = reader.GetInt32(0);
                      customer.FirstName = reader.GetString(1);
                      customer.LastName = reader.GetString(2);
                      customer.Company = reader[3] as string;
                      customer.Address = reader.GetString(4);
                      customer.City = reader.GetString(5);
                      customer.State = reader[6] as string; 
                      customer.Country = reader[7] as string;
                      customer.PostalCode = reader[8] as string;
                      customer.Phone = reader[9] as string;
                      customer.Fax = reader[10] as string;
                      customer.Email = reader.GetString(11);

                      Console.WriteLine($"{customer.CustomerId} {customer.FirstName} {customer.LastName}{customer.Company}{customer.Address}");
                   
                }
                        
                    
                
            }

            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }

            return customers;

        }
        //This methode will return a customer paging using a limit and offset
        public IEnumerable<Customer> ReturnCustomerPage(int limit, int offset)
        {
            List<Customer> customers = new List<Customer>();
            
            try
            {
                Customer customer = new Customer();
                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                string sql = "SELECT * FROM Customer as c "
                   + "ORDER BY c.CustomerId "
                   + "OFFSET(@limit)  ROWS FETCH NEXT @offset ROWS ONLY";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@limit", limit);
                command.Parameters.AddWithValue("@offset", offset);
                using SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                Console.WriteLine($"ID FirstName tLast Name  Country  PostalCode  Phone\t\tEmail\t\t");
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                while (reader.Read())
                { 
                   
                    customer.CustomerId = reader.GetInt32(0);
                    customer.FirstName = reader.GetString(1);
                    customer.LastName = reader.GetString(2);
                    customer.Country = reader[7] as string;
                    customer.PostalCode = reader[8] as string;
                    customer.Phone = reader[9] as string;
                    customer.Email = reader.GetString(11);

                    Console.WriteLine($"{customer.CustomerId} {customer.FirstName} {customer.LastName}\t\t{customer.Country}\t\t {customer.PostalCode}\t\t{customer.Phone}\\t\tt{customer.Email}\t");

                    customers.Add(customer);
                }
            }
            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }

            return customers;

        }
        //This methode will add a customer to the table customer in DB

        public void AddCustomer(Customer customer)
        {
            try
            {
                using var connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                var sql = "INSERT INTO Customer (FirstName,LastName, Country, PostalCode, Phone, Email)"+
                           "VALUES (@first_name, @last_name, @country,@postal_code,@phone, @email)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@first_name", customer.FirstName);
                command.Parameters.AddWithValue("@last_name", customer.LastName);
                command.Parameters.AddWithValue("@country", customer.Country);
                command.Parameters.AddWithValue("@postal_code", customer.PostalCode);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@email", customer.Email);

                command.ExecuteNonQuery();

                Console.WriteLine($"-----------------------------------{customer.FirstName} was added successfully----------------------------------------------------");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //This methode will update a customer to the table customer in DB

        public void UpdateCustomer(string firstName, string updateName)
        {
            try
            {
                using var connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                string sql = "UPDATE Customer SET FirstName= @updateFirstName Where FirstName Like @firstName";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@firstName",firstName);
                command.Parameters.AddWithValue("@updateFirstName",updateName);
  
                command.ExecuteNonQuery();
                Console.WriteLine($"-----------------------------------User Name is Updated-----------------------------------------------------------------");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //This methode will return the quantity of customers in each country from high to low
        public IEnumerable<Count> TotalCustomerbyCountry()
        {
            List<Count> countCountry = new List<Count>();

            try
            {
                Count sumCountry = new Count();
                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                string sql = "SELECT COUNT(CustomerID), Country FROM Customer GROUP BY Country ORDER BY COUNT(CustomerID) DESC ";
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"-----------------------------------customer by country-----------------------------------------------------------------");
                while (reader.Read())
                {
                  
                    
                    sumCountry.countResult = reader.GetInt32(0);
                    sumCountry.countObject = reader[1] as string;

                    Console.WriteLine($"{sumCountry.countObject} : {sumCountry.countResult}");
                
                    countCountry.Add(sumCountry);

                }
            }
            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }

            return countCountry;

        }

        //This methode will return the higher spender customers with their respective invoice from high to low 
        public IEnumerable<HighestInvoice> HighestSpender()
        {
            List<HighestInvoice> highInvoice = new List<HighestInvoice>();

            try
            {
                HighestInvoice customerSpender = new HighestInvoice();
                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();

                string sql = "SELECT C.CustomerId , C.FirstName, C.LastName, I.Total FROM Customer as C Inner join Invoice as I ON I.CustomerId = C.CustomerId order by total DESC ";
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {


                    customerSpender.Customer_Id = reader.GetInt32(0);
                    customerSpender.Customer_Name = reader.GetString(1);
                    customerSpender.Last_Name = reader.GetString(2);
                    customerSpender.Invoice_total = reader.GetDecimal(3);


                    Console.WriteLine($"Customer: {customerSpender.Customer_Name} {customerSpender.Last_Name}\t\t Total Invoice:  {customerSpender.Invoice_total}");

                    highInvoice.Add(customerSpender);

                }
            }
            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }

            return highInvoice;

        }
        //This methode will return customer's favourite Genre by Name
        public IEnumerable<Count> GetFavoritGenre(string customerName)
        {
            List<Count> totalGenre = new List<Count>();

            try
            {
                Count sumGenre = new Count();
                string sql = $"SELECT COUNT (T.TrackId), G.Name FROM Customer as c Inner join Invoice as I ON I.CustomerId = C.CustomerId Inner join InvoiceLine as IL ON IL.InvoiceId = I.InvoiceId Inner join Track as T ON T.TrackId = IL.TrackId Inner join Genre as G ON G.GenreId = T.GenreId Where FirstName like @firstName GROUP BY G.Name ORDER BY COUNT(T.TrackId) DESC";
                using SqlConnection connection = new SqlConnection(DbConfig.Config());
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@firstName", customerName);
                using SqlDataReader reader = command.ExecuteReader();
                int compareGenre = 0;
                Console.WriteLine($"----------------------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                    sumGenre.countResult = reader.GetInt32(0);
                    sumGenre.countObject = reader[1] as string;
                    totalGenre.Add(sumGenre);

                    if (compareGenre == 0)
                    {
                        Console.WriteLine($"{customerName}  favorite(s) genre is(are):{sumGenre.countObject} ");
                        compareGenre = sumGenre.countResult;
                    }
                    else if (compareGenre == sumGenre.countResult)
                     {
                        Console.WriteLine($"{sumGenre.countObject} ");

                    }
                }

            }

            catch (SqlException ex)

            {
                Console.WriteLine(ex.Message);
            }
            

            return totalGenre;

        }
    }
}
