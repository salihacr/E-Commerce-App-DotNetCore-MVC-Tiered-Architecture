using E_Commerce.Business.Abstract;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartRepository _cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void InitializeCart(string userId)
        {
            _cartRepository.Add(new Cart() { UserId = userId });
        }
    }
}