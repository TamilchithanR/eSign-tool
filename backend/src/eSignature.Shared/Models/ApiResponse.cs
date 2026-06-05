using System;
using System.Collections.Generic;

namespace eSignature.Shared.Models
{
    /// <summary>
    /// Standardized API response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ErrorDetails Error { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.UtcNow;
            Metadata = new Dictionary<string, object>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, string code = "ERROR", object details = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Error = new ErrorDetails
                {
                    Code = code,
                    Details = details
                }
            };
        }
    }

    /// <summary>
    /// Non-generic API response
    /// </summary>
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorDetails Error { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.UtcNow;
            Metadata = new Dictionary<string, object>();
        }

        public static ApiResponse SuccessResponse(string message = "Success")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message
            };
        }

        public static ApiResponse ErrorResponse(string message, string code = "ERROR", object details = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Error = new ErrorDetails
                {
                    Code = code,
                    Details = details
                }
            };
        }
    }

    /// <summary>
    /// Error details within response
    /// </summary>
    public class ErrorDetails
    {
        public string Code { get; set; }
        public object Details { get; set; }
        public Dictionary<string, string> ValidationErrors { get; set; }

        public ErrorDetails()
        {
            ValidationErrors = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Paginated result wrapper
    /// </summary>
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedResult()
        {
            Data = new List<T>();
        }

        public PaginatedResult(List<T> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    /// <summary>
    /// Pagination request parameters
    /// </summary>
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; } = true;
        public string SearchTerm { get; set; }

        public void Validate()
        {
            if (PageNumber < 1)
                PageNumber = 1;

            if (PageSize < 1)
                PageSize = 20;

            if (PageSize > 100)
                PageSize = 100;
        }
    }
}
