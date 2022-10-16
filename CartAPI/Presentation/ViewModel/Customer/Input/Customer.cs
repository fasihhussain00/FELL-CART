using System;
using System.ComponentModel.DataAnnotations;

namespace CartAPI.Presentation.ViewModel.Customer.Input
{
    public class Customer
    {
        public Guid ID { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
        public Domain.Model.Customer ToDomain()
        {
            return new Domain.Model.Customer(
                id: ID,
                name: Name,
                address: Address,
                email: Email,
                phone: Phone,
                password: Password);
        }
    }
}