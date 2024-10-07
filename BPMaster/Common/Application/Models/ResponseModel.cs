using System.Net;
using Common.Application.Models;

namespace Common.Application.Models
{
    public class ResponseModel
    {
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public object? Token { get; set; }

        public static ResponseModel Success<T> (T token, HttpStatusCode statusCode = HttpStatusCode.OK) where T : class
        {
            return new ResponseModel {
                HttpStatus = statusCode,
                IsSuccess = true,
                Token = token, 
            };
        }

        public static ResponseModel Error(String errorMsg, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string errorCode = "")
        {
            return new ResponseModel
            {
                HttpStatus = statusCode,
                IsSuccess = false,
                Token = new ErrorResponseModel
                {
                    HttpStatus = statusCode,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMsg
                },
            };
        }
    }
}
