namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Models
{
    public class ProductMapper
    {
        public ProductMapper() { }

        public ProductMapper(int id,
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
        public int CategoryId {  get; set; }
        public string Name { get; set; }
        public  string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }   

    }
}
