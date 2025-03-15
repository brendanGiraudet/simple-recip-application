namespace simple_recip_application.Dtos;

public record class MethodResult<T>(bool Success, T? Item, string? Message = null);
public record class MethodResult(bool Success, string? Message = null);