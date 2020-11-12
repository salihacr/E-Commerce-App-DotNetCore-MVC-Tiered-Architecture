using System.Collections.Generic;

namespace E_Commerce.Entities.Concrete
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}