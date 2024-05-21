using System;
using System.Collections.Generic;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
