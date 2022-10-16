using System;
using System.Collections.Generic;

#nullable disable

namespace CartAPIEntityFramwork.ViewModels
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Category1 { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
