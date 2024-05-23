using System.ComponentModel.DataAnnotations;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Models
{
    public class CreateProductModel
    {

        public int ProductId { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public string Weight { get; set; } = null!;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a positive number.")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Units in stock must be a non-negative number.")]
        public int UnitsInStock { get; set; }
    }
}
