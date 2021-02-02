using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model.Model;
using WebProject.Repository.Repository;


namespace WebProject.BLL.BLL
{
   public class CategoryManager
    {
        CategoryRepository _categoryRepository = new CategoryRepository();
        public bool Add(Category category)
        {
            return _categoryRepository.Add(category);
        }
        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }
        public Category GetByName(string name)
        {
            return _categoryRepository.GetByName(name);
        }
        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }
        public bool Update(Category category)
        {
            return _categoryRepository.Update(category);
        }
        
        public string UniqueTest(Category category)
        {
            return _categoryRepository.UniqueTest(category);
        }

        public bool IsCodeExist(string code)
        {
            return _categoryRepository.IsCodeExist(code);
        }

        public bool IsNameExist(string name)
        {
            return _categoryRepository.IsNameExist(name);
        }
    }
}
