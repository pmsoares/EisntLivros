using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EisntLivros.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; } = null!;
    }
}
