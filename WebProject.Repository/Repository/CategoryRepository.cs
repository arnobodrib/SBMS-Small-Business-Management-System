using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model.Model;
using WebProject.DatabaseContext.DatabaseContext;

namespace WebProject.Repository.Repository
{
    public class CategoryRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges() > 0;
        }
        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c=>c.Id==id);
        }

        public Category GetByName(string name)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Name == name);
        }
        public bool Update(Category category)
        {
            var acategory = _dbContext.Categories.FirstOrDefault(c=>c.Id==category.Id);
            acategory.Code = category.Code;
            acategory.Name = category.Name;

            return _dbContext.SaveChanges() > 0;
        }

        public string UniqueTest(Category category)
        {
            bool isNUnq,isCUnq;
            string errString = "";

            if ((_dbContext.Categories.FirstOrDefault(c => c.Name == category.Name)) == null)
            {
                isNUnq = true;
                errString += "";
            }
            else
            {
                isNUnq = false;
                errString += "Name is not Unique ";
            }
                
             
            if ((_dbContext.Categories.FirstOrDefault(c => c.Code == category.Code)) == null)
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
        }


        public bool IsCodeExist(string code)
        {
            var isExist = _dbContext.Categories.FirstOrDefault(c => c.Code == code);
            return isExist != null;
        }

        public bool IsNameExist(string name)
        {
            var isExist = _dbContext.Categories.FirstOrDefault(c => c.Name == name);
            return isExist != null;
        }

    }
}
