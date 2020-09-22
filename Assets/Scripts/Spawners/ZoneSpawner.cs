using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class ZoneSpawner<T> : SpawnDecorator<T> where T : MonoBehaviour
    {
        private Bounds _bounds;

        public ZoneSpawner(ISpawner<T> spawner, Bounds bounds) : base(spawner)
        {
            _bounds = bounds;
        }

        public override T Spawn()
        {
            T spawnItem = spawner.Spawn();

            if (spawnItem != null)
            {
                spawnItem.transform.position = GetRandomPosition();
            }

            return spawnItem;
        }

        public Vector3 GetRandomPosition()
        {
            Vector2 extents = _bounds.extents;
            var position = _bounds.center;
            position += (Vector3)Tools.Random.Range(-extents, extents);
            return position;
        }
    }
}
