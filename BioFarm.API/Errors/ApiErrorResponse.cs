using System;
using System.Net;

namespace BioFarm.API.Errors;

public class ApiErrorResponse(HttpStatusCode statusCode, string message, string? details)
{
    public HttpStatusCode StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
