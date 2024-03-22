using Ecom.core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ecom.Core.Dtos
{

    public class BaseProduct
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(1, 9999, ErrorMessage= "Price Limited By {0} and {1}")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage ="{0} must be Number !")]
        public decimal Price { get; set; }
    }
    public class ProductDto : BaseProduct
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductPicture { get; set; }
    }
    public class CreateProductDto : BaseProduct
    {
        public int CategoryId { get; set; }
        [AllowNull]
        public IFormFile Image { get; set; }
    }

    public class UpdateProductDto : BaseProduct
    {
        public int CategoryId { get; set; }
        public string OldImage { get; set; }
        [AllowNull]
        public IFormFile Image { get; set; }
    }

}
