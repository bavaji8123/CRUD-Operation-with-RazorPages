using CrudRazorPages.Database;
using CrudRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudRazorPages.Pages.Categories
{
    [BindProperties] // this binds all the properties inside this class to the UI
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        //[BindProperty] // It binds the UI to the property
        public Category Categories { get; set; }


        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Categories = _db.Category.FirstOrDefault(Categories => Categories.Id == id);
            //TempCategory = Categories;
        }

        public async Task<IActionResult> Onpost()
        {
            if (Categories.Name == Categories.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Categories.Name", "Name and Display Order Cannot be same.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(Categories);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnpostBack()
        {
            return RedirectToPage("Index");
        }
    }
}
