using Pickupable;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;


namespace Spawner
{
    public class PickupableItemSpawner : MonoBehaviour, ISpawner<PickupableItem>
    { 
        [SerializeField] private PickupableItem _prefab;

        private void Awake()
        {
            _prefab.gameObject.SetActive(false);
        }

        public PickupableItem Spawn()
        {
            PickupableItem spawnItem = Instantiate(_prefab, transform);

            return spawnItem;
        }
    }
}
