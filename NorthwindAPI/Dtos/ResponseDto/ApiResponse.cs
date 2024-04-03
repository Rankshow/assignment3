namespace NorthwindAPI.Dtos.ResponseDto;

public class ApiResponse<T>
{
    public ApiResponse()
    {
        IsSuccess = false;
    }
    public T Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}
