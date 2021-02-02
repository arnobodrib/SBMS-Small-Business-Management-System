using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using WebProject.Model.Model;
using WebProject.DatabaseContext.DatabaseContext;

namespace WebProject.Repository.Repository
{
    public class ProductRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();
        public bool Add(Product product)
        {
            _dbContext.Products.Add(product);
            return _dbContext.SaveChanges()>0;
        }


        public bool Update(Product product)
        {
            Product aproduct = _dbContext.Products.FirstOrDefault(c => c.Id == product.Id);

            if (aproduct != null)
            {
                aproduct.CategoryId = aproduct.CategoryId;
                aproduct.Code = product.Code;
                aproduct.Name = product.Name;
                aproduct.Recorder_Level = product.Recorder_Level;
                //aproduct.UnitPrice = product.UnitPrice;
                aproduct.Description = product.Description;
            }

            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Product product)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault();
            _dbContext.Products.Remove(aProduct);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }
        public Product GetById(int id)
        {
            return _dbContext.Products.FirstOrDefault(c => c.Id == id);
        }
        public Product GetByCode(string code)
        {
            return _dbContext.Products.FirstOrDefault(c => c.Code == code);
        }
        public Product GetByName(string name)
        {
            return _dbContext.Products.FirstOrDefault(c => c.Name == name);
        }
        public bool IsCodeExist(string code)
        {
            var isExist = _dbContext.Products.FirstOrDefault(c => c.Code == code);
            return isExist != null;
        }

        public bool IsNameExist(string name)
        {
            var isExist = _dbContext.Products.FirstOrDefault(c => c.Name == name);
            return isExist != null;
        }

    }
}
