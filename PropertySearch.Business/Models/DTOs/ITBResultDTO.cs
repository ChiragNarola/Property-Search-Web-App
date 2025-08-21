

namespace PropertySearch.Business.Models.DTOs
{
    public class PropertySearchResultDTO<T>
    {
        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }

        public static PropertySearchResultDTO<T> Success(T result)
            => new()
            {
                IsSuccess = true,
                Data = result
            };

        public static PropertySearchResultDTO<T> Error(string message, T data)
            => new()
            {
                IsSuccess = false,
                Message = message,
                Data = data
            };
    }

    public class PropertySearchResultDTO
    {
        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public object? Data { get; private set; }

        public static PropertySearchResultDTO Error(string message)
            => new()
            {
                IsSuccess = false,
                Message = message
            };

        public static PropertySearchResultDTO Success()
            => new()
            {
                IsSuccess = true,
            };

        public static PropertySearchResultDTO Success(object data)
           => new()
           {
               IsSuccess = true,
               Data = data
           };
    }
}
