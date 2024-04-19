using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();

        private CustomerDAO()
        {
        }

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }

            }
        }

        public void Add(Customer customer)
        {
            try
            {
                Customer p = FindOne(item => item.CustomerId.Equals(customer.CustomerId));
                if (p == null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        hotelContext.Customers.Add(customer);
                        hotelContext.SaveChanges();
                    }

                }
                else
                {
                    throw new Exception("The customer is already exist");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Customer customer)
        {
            try
            {
                Customer p = FindOne(item => item.CustomerId.Equals(customer.CustomerId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        if (!hotelContext.BookingReservations.Any(b => b.CustomerId.Equals(customer.CustomerId)))
                        {
                            hotelContext.Customers.Remove(customer);
                        }
                        else
                        {
                            customer.CustomerStatus = 0;
                            hotelContext.Entry<Customer>(customer).State = EntityState.Modified;
                        }
                        hotelContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The customer does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Customer FindOne(Expression<Func<Customer, bool>> predicate)
        {
            Customer customer = null;
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    customer = hotelContext.Customers.SingleOrDefault(predicate);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return customer;
        }

        public List<Customer> FindAll(Expression<Func<Customer, bool>> predicate)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    customers = hotelContext.Customers.Where(predicate).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return customers;
        }

        public List<Customer> List()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    customers = hotelContext.Customers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return customers;
        }

        public void Update(Customer customer)
        {
            try
            {
                Customer p = FindOne(item => item.CustomerId.Equals(customer.CustomerId));
                if (p != null)
                {
                    using (var hotelContext = new FUMiniHotelManagementContext())
                    {
                        hotelContext.Entry<Customer>(customer).State = EntityState.Modified;
                        hotelContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The customer does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Customer FindEmailAndPass(string email, string password)
        {
            try
            {
                using (var hotelContext = new FUMiniHotelManagementContext())
                {
                    return hotelContext.Customers.SingleOrDefault(c => c.EmailAddress.Equals(email) && c.Password.Equals(password) && c.CustomerStatus != 0);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
