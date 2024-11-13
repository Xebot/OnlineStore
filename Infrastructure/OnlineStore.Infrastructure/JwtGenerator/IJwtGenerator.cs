namespace OnlineStore.Infrastructure.JwtGenerator
{
    /// <summary>
    /// Интерфейс сервиса генерации токенов.
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// Создает и возвращает токен доступа.
        /// </summary>
        /// <returns></returns>
        string GenerateToken();
    }
}
