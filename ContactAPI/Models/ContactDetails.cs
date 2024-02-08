﻿namespace ContactAPI.Models
{
    public class ContactDetails
    {
        public Guid Id {get; set;} 
        public string FullName { get; set;}
        public string Email { get; set;}
        public long PhoneNumber { get; set;}
        public string Address { get; set;}
    }
}
