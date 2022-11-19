using CrudRazorPages.Database;
using CrudRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CrudRazorPages.Pages.Categories
{
    [BindProperties] // this binds all the properties inside this class to the UI
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        //[BindProperty] // It binds the UI to the property
        public Category Categories { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            //Categories = _db.Category.FirstOrDefault(Categories => Categories.Id == id);
            Categories = _db.Category.Find(id);
        }

        public async Task<IActionResult> Onpost()
        {
            var obj = _db.Category.Find(Categories.Id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Category.Remove(obj);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");
        }

        public IActionResult OnpostBack()
        {
            return RedirectToPage("Index");
        }
    }
}
