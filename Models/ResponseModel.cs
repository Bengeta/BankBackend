using VTBlockBackend.Enums;

namespace VTBlockBackend.Models;

public class ResponseModel<T>
{
    public ResultCode ResultCode { get; set; }
    public T? Data { get; set; }
}