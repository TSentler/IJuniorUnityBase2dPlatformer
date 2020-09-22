using UnityEngine;

namespace Spawner
{
    public interface ISpawner<T>
    {
        T Spawn();
    }
}