using System.ComponentModel.DataAnnotations;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Models
{
    public class CreateProductMapper
    {
        public CreateProductMapper() { }
        public CreateProductMapper(int id,
            int categoryId,
            string name,
            string weight,
            decimal unitPrice,
            int unitsInStock)
        {
            this.Id = id;
            this.CategoryId = categoryId;
            this.Name = name;
            this.Weight = weight;
            this.UnitsInStock = unitsInStock;
            this.UnitPrice = unitPrice;

        }
        public int Id { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
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
