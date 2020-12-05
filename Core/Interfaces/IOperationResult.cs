namespace Core.Interfaces
{
    public interface IOperationResult<T>
    {
        string Message { get; }

        T Entity { get; }

        bool Success { get; }
    }
}
