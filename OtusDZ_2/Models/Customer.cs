using System.ComponentModel.DataAnnotations;

namespace OtusDZ_2.Models
{
    public class Customer
    {
        public long Id { get; init; }

        [Required]
        public string Firstname { get; init; }

        [Required]
        public string Lastname { get; init; }
    }
}
