using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model.Model;
using WebProject.DatabaseContext.DatabaseContext;

namespace WebProject.Repository.Repository
{
    public class SupplierRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
            return _dbContext.SaveChanges() > 0;
        }
        public List<Supplier> GetAll()
        {
            List<Supplier> suppliers = new List<Supplier>();

            try
            {
                return _dbContext.Suppliers.ToList();
            }
            catch
            {
                return suppliers;
            }
        }
        public Supplier GetById(int id)
        {
            return _dbContext.Suppliers.FirstOrDefault(c => c.Id == id);
        }
        public Supplier GetByName(string name)
        {
            return _dbContext.Suppliers.FirstOrDefault(c => c.Name == name);
        }
        public bool Update(Supplier supplier)
        {
            var bSupplier = _dbContext.Suppliers.FirstOrDefault(s => s.Id == supplier.Id);
            bSupplier.Code = supplier.Code;
            bSupplier.Name = supplier.Name;
            bSupplier.Address = supplier.Address;
            bSupplier.Email = supplier.Email;
            bSupplier.Contact = supplier.Contact;
            bSupplier.ContactPerson = supplier.ContactPerson;

            return _dbContext.SaveChanges() > 0;
        }

        public string UniqueTest(Supplier supplier)
        {
            bool isNUnq,isCUnq;
            string errString = "";

            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Name == supplier.Name)) == null)
            {
                isNUnq = true;
                errString += "";
            }
            else
            {
                isNUnq = false;
                errString += "Name is not Unique ";
            }


            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Code == supplier.Code)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Code is not Unique";
            }

            return errString;


            if ((_dbContext.Suppliers.FirstOrDefault(c => c.Contact == supplier.Contact)) == null)
            {
                isCUnq = true;
                errString += "";
            }
            else
            {
                isCUnq = false;
                errString += " Contact is not Unique";
            }

            return errString;
        }






        public bool IsCodeExist(string code)
        {
            var isExist = _dbContext.Suppliers.FirstOrDefault(c => c.Code == code);
            return isExist != null;
        }

        public bool IsNameExist(string name)
        {
            var isExist = _dbContext.Suppliers.FirstOrDefault(c => c.Name == name);
            return isExist != null;
        }


        public bool IsContactExist(string contact)
        {
            var isExist = _dbContext.Suppliers.FirstOrDefault(c => c.Contact == contact);
            return isExist != null;
        }


    }
}
