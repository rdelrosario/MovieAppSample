﻿using System;
using System.Collections.Generic;

namespace MovieTimeApp.Services
{
    public class Response<TResponse>
    {
        public bool SuccessResult { get; }
        public List<string> Errors { get; set; } = new List<string>();
        public TResponse Result { get; set; }

        public Response<TResponse> Success(Action<TResponse> callback)
        {
            if (SuccessResult)
                callback(Result);
            return this;
        }
        public TResult Success<TResult>(Func<TResponse, TResult> callback)
        {
            if (SuccessResult)
                return callback(Result);
            return default(TResult);
        }

        public Response<TResponse> Error(Action<List<string>> callback)
        {
            if (!SuccessResult)
                callback(Errors);
            return this;
        }

        public static Response<TResponse> Error(List<string> errors) =>
            new Response<TResponse>(errors);
        public static Response<TResponse> Error() =>
            new Response<TResponse>(new List<string>());

        public static Response<TResponse> Ok(TResponse response) =>
            new Response<TResponse>(response);
        public static Response<TResponse> Error(string error) =>
            new Response<TResponse>(new List<string> { error });
        public static Response<TResponse> Void() =>
           new Response<TResponse>();

        protected Response(TResponse response)
        {
            SuccessResult = true;
            Result = response;
        }

        protected Response()
        {

        }

        protected Response(List<string> errors)
        {
            SuccessResult = false;
            Errors = new List<string>();
            Errors.AddRange(errors);
        }
    }
}