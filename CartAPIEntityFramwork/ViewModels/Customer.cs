﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.ViewModels
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
