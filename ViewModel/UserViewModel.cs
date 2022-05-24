using FidelityTest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityTest.ViewModel
{
    public class UserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

    }

    public class AccountsViewModel
    {
        public int Id { get; set; }

        [MaxLength(128), Required]
        public string CompanyName { get; set; }
        public string Website { get; set; }

        public List<UserDetail> Users { get; set; }

    }
    public class UpdateAccountViewModel
    {
        [MaxLength(128), Required]
        public string CompanyName { get; set; }
        public string Website { get; set; }
    }
}
