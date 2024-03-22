using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.core.Entities
{
    public class Product: BaseEntity<int>
    {
        public string Name { get; set; }   
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductPicture { get; set; }
        public int CategoryId { get; set; }                           
        public virtual Category Category { get; set; }
       
    }
}
