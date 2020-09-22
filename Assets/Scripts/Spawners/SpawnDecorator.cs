using Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public abstract class SpawnDecorator<T> : ISpawner<T> where T : MonoBehaviour
    {
        protected ISpawner<T> spawner;

        public SpawnDecorator(ISpawner<T> spawner)
        {
            this.spawner = spawner;
        }

        public abstract T Spawn();
    }
}