using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducgADONet.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        
      
        public Product(int Id, string Name, string Description, decimal Price, string ImageUrl)
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.ImageUrl = ImageUrl;
           
            this.ImageUrl = ImageUrl;
        }

        public Product()
        {
        }

        public override string ToString()
        {
            return String.Format("{0,-20}{1,-10}{2,-10}{3,-10}{4,-10}", Id, Name, Description, Price, ImageUrl);
        }

    }
}
