using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pool 
{
    public class PoolStack
    {
        private Stack<IPoolable> _pool = new Stack<IPoolable>();

        public PoolStack(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; private set; }
        public bool IsEmpty => _pool.Count == 0;
        public bool IsFull => _pool.Count == Capacity;

        public bool TryPush(IPoolable item)
        {
            if (IsFull)
                return false;

            _pool.Push(item);

            return true;
        }

        public bool TryPop(out IPoolable item)
        {
            item = default;

            if (IsEmpty)
                return false;

            item = _pool.Pop();

            return true;
        }

        public void Remove(IPoolable removing)
        {
            var buffer = _pool.ToArray();
            _pool.Clear();
            buffer.AsParallel().Where(item => item != removing)
                .ForAll(_pool.Push);
        }

        public void Clear()
        {
            _pool.Clear();
        }
    }
}