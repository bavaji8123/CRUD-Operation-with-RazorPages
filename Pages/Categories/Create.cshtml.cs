using CrudRazorPages.Database;
using CrudRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudRazorPages.Pages.Categories
{
    [BindProperties] // this binds all the properties inside this class to the UI
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        //[BindProperty] // It binds the UI to the property
        public Category Categories { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> Onpost()
        {
            if (Categories.Name == Categories.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Categories.Name", "Name and Display Order Cannot be same.");
            }
            if (ModelState.IsValid)
            {
                var obj = _db.Category.Where(cat => cat.Name == Categories.Name && cat.DisplayOrder == Categories.DisplayOrder);
                if (obj.Count() > 0)
                {
                    TempData["Failure"] = "Name and Display Order already Exists..!!";
                    return RedirectToPage("Index");
                }
                await _db.Category.AddAsync(Categories);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category created Successfully";
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
