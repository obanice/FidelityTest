using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityTest.Model
{
    public class UserDetail
    {


        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; } = "";

        public int AccountId { get; set; }
        //[ForeignKey("AccountId")]
        public virtual Account Accounts { get; set; }
    }
}
