using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Domain.Application
{

    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default, IEnumerable<string> errors = null) =>
            new Response<T>(data, message, true, errors);

        public static Response<T> Ok<T>(T data, string message = "") =>
            new Response<T>(data, message, false, null);
    }

    public class Response<T>: IWithErrors
    {
        public Response()
        {
            Data = default;
            Message = string.Empty;
            Error = false;
            Errors = new List<string>();

        }

        public Response(T data, string msg, bool error, IEnumerable<string> errors)
        {
            Data = data;
            Message = msg;
            Error = error;
            Errors = errors ?? new List<string>();
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }

        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }

    public interface IWithErrors
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
