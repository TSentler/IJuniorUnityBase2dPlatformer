using Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class LazyPoolSpawner<T> : SpawnDecorator<T>, IDisposable where T : MonoBehaviour, IPoolable
    {

        private PoolLazyItems _pool;
        private bool _disposedValue = false;

        public LazyPoolSpawner(ISpawner<T> spawner, int poolCapacity) : base(spawner)
        {
            _pool = new PoolLazyItems(poolCapacity);
        }

        ~LazyPoolSpawner()
        {
            Dispose(false);
        }

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;

            if (disposing)
            {
                _pool.Dispose();
                _pool = null;
            }

            _disposedValue = true;
        }
#endregion

        public override T Spawn()
        {
            T item = default;

            if (_pool.TryPop(out var poolItem))
            {
                item = (T)poolItem;
            }
            else if (_pool.IsFull == false)
            {
                item = spawner.Spawn();
                _pool.Registrate(item);
            }

            return item;
        }
    }
}
