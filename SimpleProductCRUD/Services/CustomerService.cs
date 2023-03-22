using SimpleProductCRUD.Models;
using SimpleProductCRUD.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleProductCRUD.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string CS = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public async Task DeleteCustomer(Guid? id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    var cmd = new SqlCommand("delete from customer where customerid=@customerid", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.Parameters.AddWithValue("@customerid", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Customer> GetCustomerById(Guid? id)
        {
            try
            {
                Customer customers = new Customer();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("select * from customer where customerid=@customerid", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("customerid", id);
                    con.Open();
                    SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        customers.CustomerId = Guid.Parse(rdr["CustomerId"].ToString());
                        customers.FirstName = Convert.ToString(rdr["FirstName"]);
                        customers.LastName = Convert.ToString(rdr["LastName"]);
                        customers.Phone = Convert.ToString(rdr["Phone"]);
                        customers.Email = Convert.ToString(rdr["Email"]);
                        customers.Street = Convert.ToString(rdr["Street"]);
                        customers.City = Convert.ToString(rdr["City"]);
                        customers.State = Convert.ToString(rdr["State"]);
                        customers.Zipcode = Convert.ToString(rdr["Zipcode"]);

                    }
                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IList<Customer>> GetCustomers()
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("select * from customer", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        var customer = new Customer()
                        {
                            CustomerId = Guid.Parse(rdr["CustomerId"].ToString()),
                            FirstName = Convert.ToString(rdr["FirstName"]),
                            LastName = Convert.ToString(rdr["LastName"]),
                            Phone = Convert.ToString(rdr["Phone"]),
                            Email = Convert.ToString(rdr["Email"]),
                            Street = Convert.ToString(rdr["Street"]),
                            City = Convert.ToString(rdr["City"]),
                            State = Convert.ToString(rdr["State"]),
                            Zipcode = Convert.ToString(rdr["Zipcode"]),
                        };

                        customers.Add(customer);
                    }
                    return (customers);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                try
                {
                    var query = "insert into customer (customerId,firstname,lastname,phone,email";
                    query += ",street,city,state,zipcode) values (@customerId,@firstname,@lastname";
                    query += ",@phone,@email,@street,@city,@state,@zipcode)";
                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@customerId", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@firstname", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", customer.LastName);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@street", customer.Street);
                    cmd.Parameters.AddWithValue("@city", customer.City);
                    cmd.Parameters.AddWithValue("@state", customer.State);
                    cmd.Parameters.AddWithValue("@zipcode", customer.Zipcode);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    var query = "update customer set firstname=@firstname";
                    query += ",lastname=@lastname,phone=@phone,email=@email,street=@street,";
                    query += "city=@city,state=@state,zipcode=@zipcode where customerId=@customerId";

                    var cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@customerId",customer.CustomerId);
                    cmd.Parameters.AddWithValue("@firstname", customer.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", customer.LastName);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@email", customer.Email);
                    cmd.Parameters.AddWithValue("@street", customer.Street);
                    cmd.Parameters.AddWithValue("@city", customer.City);
                    cmd.Parameters.AddWithValue("@state", customer.State);
                    cmd.Parameters.AddWithValue("@zipcode", customer.Zipcode);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}