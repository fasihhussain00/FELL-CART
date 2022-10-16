using System;

namespace CartAPI.Presentation.ViewModel.Cart.Input
{
    public class CartDetail
    {
        public Guid ID { get; set; } = Guid.Empty;
        public Guid ProductID { get; set; }
        public decimal Quantity { get; set; } = 0.0M;

        public Domain.Model.CartDetail ToDomain()
        {
            return new Domain.Model.CartDetail
            {
                ID = ID,
                Product = new Domain.Model.Product
                {
                    ID = ProductID
                },
                Quantity = Quantity
            };
        }
    }
}
