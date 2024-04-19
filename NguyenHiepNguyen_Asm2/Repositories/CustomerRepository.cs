using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Add(Customer customer)
        {
            CustomerDAO.Instance.Add(customer);
        }

        public void Delete(Customer customer)
        {
            CustomerDAO.Instance.Delete(customer);
        }

        public List<Customer> FindAllBy(CustomerFilter filter)
        {
            if (filter != null)
            {
                return CustomerDAO.Instance.FindAll(customer => (filter.CustomerId == null || customer.CustomerId.Equals(filter.CustomerId)) &&
                                                              (filter.CustomerFullName == null || customer.CustomerFullName.ToLower().Contains(filter.CustomerFullName.ToLower())) &&
                                                              (filter.Telephone == null || customer.Telephone.Contains(filter.Telephone)) &&
                                                              (filter.EmailAddress == null || customer.EmailAddress.ToLower().Contains(filter.EmailAddress.ToLower())) &&
                                                              (filter.CustomerBirthday == null || customer.CustomerBirthday == filter.CustomerBirthday) &&
                                                              (filter.CustomerStatus == null || customer.CustomerStatus.Equals(filter.CustomerStatus)));
            }
            return List();
        }

        public List<Customer> List()
        {
            return CustomerDAO.Instance.List();
        }

        public void Update(Customer customer)
        {
            CustomerDAO.Instance.Update(customer);
        }

        public Customer FindEmailAndPass(string email, string password)
        {
            return CustomerDAO.Instance.FindEmailAndPass(email, password);
        }

        public Customer FindOne(Expression<Func<Customer, bool>> predicate)
        {
            return CustomerDAO.Instance.FindOne(predicate);
        }
    }

    public class CustomerFilter
    {
        public int? CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }
    }
}
