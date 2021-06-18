namespace Hahn.ApplicationProcess.February2021.Domain
{
    /// <summary>
    /// Contract for return data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOperationResult<T>
    {
        /// <summary>
        /// Returned data if succesfull
        /// </summary>
        T Data { get; }
        
        /// <summary>
        /// Tells whether request was or not succesfull
        /// </summary>
        bool Success { get; }
        
        /// <summary>
        /// Response Message
        /// </summary>
        string Message { get; }
    }
}