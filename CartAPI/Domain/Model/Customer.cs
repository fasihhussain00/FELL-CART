using System;

namespace CartAPI.Domain.Model
{
    public class Customer
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Customer()
        {

        }
        public Customer(Guid id, string name, string address, string email, string phone, string password)
        {
            ID = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
            Password = password;
        }
        public Customer(Guid id, string name, string address, string email, string phone)
        {
            ID = id;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }

        public void Deconstruct(out Guid id, out string name, out string address, out string email, out string phone)
        {
            id = ID;
            name = Name;
            address = Address;
            email = Email;
            phone = Phone;
        }
        public void Deconstruct(out Guid id, out string name, out string address, out string email, out string phone, out string password)
        {
            id = ID;
            name = Name;
            address = Address;
            email = Email;
            phone = Phone;
            password = Password;
        }
    }
}