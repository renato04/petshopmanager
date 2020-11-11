using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Domain.Application
{

    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data, IEnumerable<string>? errors = null) where T : notnull =>
            new Response<T>(data, message, true, errors);

        public static Response<T> Ok<T>(T data, string message = "") where T : notnull =>
            new Response<T>(data, message, false, null);
    }

    public record Response<T>: IWithErrors
        where T : notnull
    {
        public Response(T data, string msg, bool error, IEnumerable<string>? errors) 
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
