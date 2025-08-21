namespace DonghuaFlix.Backend.src.Core.Application.Helpers;

public abstract class BaseResult
{
    public bool IsSucess { get; }
    public string? Message { get; }
    public string? ErrorCode { get; }
    public DateTime Timestamp { get; }

    protected BaseResult(bool isSuccess, string? message, string? errorCode  = null)
    {
        IsSucess = isSuccess;
        Message = message;
        ErrorCode = errorCode;
        Timestamp = DateTime.UtcNow;
    }


}

public abstract class BaseResult<T> :  BaseResult
{
    public  T? Data { get; }

    protected BaseResult(bool isSuccess,string? message  , string? errorCode = null  , T? data = default)
        : base(isSuccess, message, errorCode)
    {
        Data = data;
    }


}