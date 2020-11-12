using E_Commerce.Entities.Concrete;

namespace E_Commerce.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
    }
}