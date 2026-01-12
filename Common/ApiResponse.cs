namespace KutuphaneAPI.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> CreateSuccess(T data, string message) => 
        new ApiResponse<T> { Success = true, Message = message, Data = data };

    public static ApiResponse<T> CreateFail(string message) => 
        new ApiResponse<T> { Success = false, Message = message, Data = default };
}