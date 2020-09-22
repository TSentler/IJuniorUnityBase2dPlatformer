namespace Pool
{
    public interface IPool
    {
        int Capacity { get; }
        bool IsEmpty { get; }
        bool IsFull { get; }

        bool TryPush(IPoolable item);
        bool TryPop(out IPoolable item);
        void Remove(IPoolable removing);
        void Clear();
    }
}