using BusinessObjects;
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
    public interface ICustomerRepository
    {
        List<Customer> List();
        void Add(Customer member);
        void Update(Customer member);
        void Delete(Customer member);
        List<Customer> FindAllBy(CustomerFilter filter);
        Customer FindOne(Expression<Func<Customer, bool>> predicate);
        Customer FindEmailAndPass(string email, string password);
    }
}
