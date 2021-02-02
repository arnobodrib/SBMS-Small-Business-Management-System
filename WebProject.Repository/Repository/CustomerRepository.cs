using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model.Model;
using WebProject.DatabaseContext.DatabaseContext;

namespace WebProject.Repository.Repository
{
    public class CustomerRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            return _dbContext.SaveChanges() > 0;
        }
        public List<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public bool Update(Customer customer)
        {
            var acustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == customer.Id);

            acustomer.Code = customer.Code;
            acustomer.Name = customer.Name;
            acustomer.Address = customer.Address;
            acustomer.Email = customer.Email;
            acustomer.Contact = customer.Contact;
            acustomer.LoyaltyPoint = customer.LoyaltyPoint;
            
            return _dbContext.SaveChanges() > 0;

        }
    }
}
