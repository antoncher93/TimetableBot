using System.Net;

namespace YandexCloudApiClient;

public class ApiCloudResponse<TResult>
{
    private SuccessResponse? _success;
    private readonly ErrorResponse? _error;

    private ApiCloudResponse(SuccessResponse? success)
    {
        _success = success;
    }

    private ApiCloudResponse(
        ErrorResponse error)
    {
        _error = error;
    }

    public static ApiCloudResponse<TResult> FromSuccess(TResult result)
    {
        return new ApiCloudResponse<TResult>(success: new SuccessResponse(result));
    }

    public static ApiCloudResponse<TResult> FromError(
        HttpStatusCode statusCode,
        string? message)
    {
        return new ApiCloudResponse<TResult>(
            error: new ErrorResponse(
                statusCode: statusCode,
                message: message));
    }

    // метод Matching-паттерна
    public T Match<T>(
        Func<TResult, T> onSuccess,
        Func<HttpStatusCode, string?, T> onError)
    {
        if (_success != null)
        {
            return onSuccess(_success.Result);
        }

        if (_error != null)
        {
            return onError(_error.StatusCode, _error.Message);
        }

        throw new NotSupportedException();
    }

    public TResult ResultOrException() => this.Match(
        onSuccess: result => result,
        onError: (s, message) => throw new HttpRequestException(
            message: message,
            inner: null,
            statusCode: s));

    private class SuccessResponse
    {
        public SuccessResponse(TResult result)
        {
            Result = result;
        }

        public TResult Result { get; }
    }

    private class ErrorResponse
    {
        public ErrorResponse(HttpStatusCode statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public HttpStatusCode StatusCode { get; }
        public string? Message { get; }
    }
}