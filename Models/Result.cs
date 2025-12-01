namespace TechInventory.Models;

public class Result<T> where T : class
{
    public bool IsSucess { get; }
    public string Message { get; }
    public T? Data { get; }

    private Result(bool sucess, string message, T? data)
    {
        IsSucess = sucess;
        Message = message;
        Data = data;
    }

    public static Result<T?> Success(T? data)
        => new Result<T?>(true, string.Empty, data);

    public static Result<T?> Failure(string message, T? data)
        => new Result<T?>(false, message, data);
}