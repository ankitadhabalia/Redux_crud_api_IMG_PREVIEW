using App.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public class ProductContext1 : DbContext
    {
        public DbSet<ProductFile> ProductInformation { get; set; }
        public DbSet<ProductImage> ImageTable { get; set; }
        public ProductContext1() : base("DefaultConnection") { }
        
    }
}