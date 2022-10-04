using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProducgADONet.Models
{
    internal class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Product> products { get; set; }


        public Category()
        {
            this.CategoryName = CategoryName;
            this.products = new List<Product>();

        }


        //method to add product to category.
       
        

    }
}
