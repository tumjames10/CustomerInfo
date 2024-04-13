using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInfo.Entity.Model
{
    public class Customer : BaseEntity
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }  
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
    }
}
