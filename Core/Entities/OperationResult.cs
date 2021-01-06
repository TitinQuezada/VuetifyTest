using Core.Interfaces;

namespace Core.Entities
{
    public sealed class OperationResult<T> : IOperationResult<T>
    {
        private OperationResult(string message, T entity, bool success)
        {
            Message = message;
            Entity = entity;
            Success = success;
        }

        public string Message { get; }

        public T Entity { get; }

        public bool Success { get; }

        public static OperationResult<T> Ok(T entity) => new OperationResult<T>(string.Empty, entity, true);

        public static OperationResult<T> Ok() => new OperationResult<T>(string.Empty, default, true);

        public static OperationResult<T> Fail(string message) => new OperationResult<T>(message, default, false);
    }
}
