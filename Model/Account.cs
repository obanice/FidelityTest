using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityTest.Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128), Required]
        public string CompanyName { get; set; }

        public string Website { get; set; }
    }
}
