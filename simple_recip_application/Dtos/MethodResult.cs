namespace simple_recip_application.Dtos;

public record class MethodResult<T>(bool Success, T? Item, string? Message);
public record class MethodResult(bool Success, string? Message);