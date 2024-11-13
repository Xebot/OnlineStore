namespace OnlineStore.AppServices.Carts.Helpers
{
    /// <summary>
    /// Хэлпер ключей редиса для корзины.
    /// </summary>
    public static class CartRedisKeyHelper
    {
        public static string GetCartItemsCountKey(int userId) 
            => $"CartItemsCount:User_{userId}";
    }
}
