using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class PoolLazyItems : IPool, IDisposable
    {
        private readonly IPoolable[] _created;

        private PoolStack _poolStack;
        private int _createdCount;
        private bool _disposedValue = false;

        public PoolLazyItems(int poolCapacity)
        {
            _poolStack = new PoolStack(poolCapacity);
            _created = new IPoolable[poolCapacity];
        }

        ~PoolLazyItems()
        {
            Dispose(false);
        }

        public bool IsFull => _createdCount == _poolStack.Capacity;
        public int Capacity => _created.Length;
        public bool IsEmpty => _createdCount == 0;

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

            }

            Clear();

            _disposedValue = true;
        }

        #endregion

        private bool AlreadyContain(IPoolable poolItem) =>
            Array.IndexOf(_created, poolItem) != -1;

        public bool Registrate(IPoolable poolItem)
        {
            lock (_poolStack)
            {
                if (TryPush(poolItem))
                {
                    _poolStack.TryPop(out var registrated);
                    if (poolItem.Equals(registrated) == false)
                        throw new InvalidOperationException("Registrate lazy pool item error");

                    return true;
                }
            }
            
            return false;
        }

        public bool TryPush(IPoolable poolItem)
        {
            if (IsFull 
                || AlreadyContain(poolItem)
                || _poolStack.TryPush(poolItem) == false)
            {
                return false;
            }

            poolItem.PoolReturning += PoolItemReturn;
            poolItem.PoolRemoving += Remove;

            _created[_createdCount] = poolItem;
            _createdCount++;

            return true;
        }

        public bool TryPop(out IPoolable poolItem)
        {
            return _poolStack.TryPop(out poolItem);
        }

        private void PoolItemReturn(IPoolable poolItem)
        {
            _poolStack.TryPush(poolItem);
        }

        public void Remove(IPoolable poolItem)
        {
            var i = Array.IndexOf(_created, poolItem);
            if (i == -1)
                return;

            _poolStack.Remove(poolItem);

            _createdCount--;

            if (i < _created.Length)
                _created[i] = _created[_createdCount];

            _created[_createdCount] = null;
        }

        public void Clear()
        {
            for (int i = 0; i < _createdCount; i++)
            {
                _created[i].PoolReturning -= PoolItemReturn;
                _created[i].PoolRemoving -= Remove;
                _created[i] = null;
            }

            _createdCount = 0;

            if (_poolStack != null)
            {
                _poolStack.Clear();
                _poolStack = null;
            }
        }
    }
}