using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInfo.Entity.Model
{
    public class Address : BaseEntity
    {
        [Key]
        public int AddressId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        [Required]
        public string Country { get; set; } 
    }
}
