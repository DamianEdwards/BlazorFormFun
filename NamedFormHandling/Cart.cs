using System.Collections.Concurrent;

namespace NamedFormHandling
{
    public class Cart
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<int, CartItem>> _cartStore = new();

        public async Task AddToCartAsync(string userId, int itemId)
        {
            // Simulate remote call
            await Task.Delay(20);

            var cartItems = _cartStore.GetOrAdd(userId, new ConcurrentDictionary<int, CartItem>());
            var newCount = cartItems.AddOrUpdate(itemId, new CartItem(itemId, 1), (id, cartItem) =>
            {
                cartItem.Count++;
                return cartItem;
            });
        }

        public async Task<int> GetItemCountAsync(string userId)
        {
            // Simulate remote call
            await Task.Delay(20);

            if (_cartStore.TryGetValue(userId, out var cartItems))
            {
                return cartItems.Sum(ci => ci.Value.Count);
            }

            return 0;
        }

        class CartItem(int itemId, int count)
        {
            public int ItemId { get; set; } = itemId;
            public int Count { get; set; } = count;
        }
    }
}
