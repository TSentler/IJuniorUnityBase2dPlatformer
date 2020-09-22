using UnityEngine.Events;

namespace Pool
{
    public interface IPoolable
    {
        event UnityAction<IPoolable> PoolReturning;
        event UnityAction<IPoolable> PoolRemoving;
    }
}
