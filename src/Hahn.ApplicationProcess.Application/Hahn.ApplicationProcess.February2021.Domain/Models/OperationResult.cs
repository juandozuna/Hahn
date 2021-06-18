namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using FluentValidation.Results;

    /// <summary>
    /// Implementation of <see cref="IOperationResult{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OperationResult<T> : IOperationResult<T>
    {
        /// <inheritdoc />
        public T Data { get;}

        /// <inheritdoc />
        public bool Success { get;}

        /// <inheritdoc />
        public string Message { get;}

        private OperationResult(T data, string message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        /// <summary>
        /// Creates a new successfully instance of <see cref="OperationResult{T}"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static OperationResult<T> Ok(T data) => new(data, string.Empty, true);

        /// <summary>
        /// Successful instance without data
        /// </summary>
        /// <returns></returns>
        public static OperationResult<T> Ok() => new(default(T), string.Empty, true);

        /// <summary>
        /// Created failed instance
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperationResult<T> Fail(string message) => new(default(T), message, false);

        /// <summary>
        /// Creates a failed instance with data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperationResult<T> Fail(T data, string message) => new(data, message, false);

        /// <summary>
        /// Creates a failed instance based on a validation result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OperationResult<T> ValidationFailed(ValidationResult result)
        {
            string message = result.Errors.Aggregate(string.Empty, 
                (current, failure) => current + $"* {failure.PropertyName}: ${failure.ErrorMessage} \n");

            return new OperationResult<T>(default(T), message, false);
        }
    }
}