using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using ShareViewModel.DTO;


namespace API.Services
{
    public class CategoryServices : ICategoryService
    {
        private ShopDbContext _context;
        public CategoryServices(ShopDbContext context)
        {
            _context = context;
        }

        //category
        public async Task<List<Category>> GetAllCategories() =>  _context.Category.ToList();
        public async Task<AdminCategoryDTO> AddCategory(AdminCategoryDTO addcategory)
        {
            var category = new Category();
            category.ID = addcategory.ID;
            category.Name = addcategory.Name;
            category.Description = addcategory.Description;
            category.Created_by = addcategory.Created_by;
            category.Created_at = DateTime.Now;
            category.Updated_at = DateTime.Now;

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return new AdminCategoryDTO()
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                Created_by = category.Created_by,
                Created_at = category.Created_at,
                Updated_at = category.Updated_at,
            };
        }

        public async Task<AdminCategoryDTO> UpdateCategory(AdminCategoryDTO updatecategory, int ID)
        {
            var category = _context.Category.Where(c => c.ID == ID).FirstOrDefault();
            if (updatecategory.Name != "") { category.Name = updatecategory.Name; }
            if (updatecategory.Description != "") { category.Description = updatecategory.Description; }
            category.Updated_at = DateTime.Now;
            await _context.SaveChangesAsync();


            return new AdminCategoryDTO()
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                Created_by = category.Created_by,
                Created_at = category.Created_at,
                Updated_at = category.Updated_at,
            };
        }

        public int DeleteCategory(int ID)
        {

            var category = _context.Category.Where(c => c.ID == ID).FirstOrDefault();
            if (category != null)
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
            return 0;
        }

        public Category GetCategoryByID(int ID) => _context.Category.Where(c => c.ID == ID).FirstOrDefault();
        public List<Products> GetProductByCategory(int ID)
        {
            //var pageNumber = page ?? 1;
            //var pageSize = 10;
            //var productList= _context.Products.OrderByDescending(x => x.ID).Where(x => x.CategoryID == ID).ToPagedList(pageNumber, pageSize).ToList();
            var productList = _context.Products.OrderByDescending(x => x.ID).Where(x => x.CategoryID == ID).ToList();
            return productList;
        }

    }
}
