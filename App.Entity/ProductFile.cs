using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class ProductFile
    {
        [Key]
        public int id { get; set; }
        public string jobTitleName { get; set; }
        public string firstName { get; set;}
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
    }
}