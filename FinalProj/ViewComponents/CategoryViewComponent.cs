using FinalProj.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalProj.Components
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ECContext _context;

        public CategoryViewComponent(ECContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string viewName = "Default")
        {
            var categories = _context.Categories.ToList();
            return View(viewName,categories);
        }


    }
}
