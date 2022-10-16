using System;
using System.Collections.Generic;
using System.Linq;

namespace CartAPI.Presentation.ViewModel.Cart.Input
{
    public class Cart
    {
        public Guid ID { get; set; } = Guid.Empty;
        public Guid CustomerID { get; set; }
        public List<CartDetail> LineItems { get; set; }
        
        public Domain.Model.Cart ToDomain()
        {
            return new Domain.Model.Cart
            {
                ID = ID,
                Customer = new Domain.Model.Customer
                {
                    ID = CustomerID
                },
                LineItems = LineItems.Select(x => x.ToDomain()).ToList()            
            };
        }
    }
}
