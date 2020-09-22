using Pickupable;
using Sirenix.OdinInspector;
using Spawner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickupableItemSpawner), typeof(Collider2D))]
public class HouseSafe : MonoBehaviour
{
    [SerializeField] private int _poolCapacity;

    private PickupableItemSpawner _itemSpawner;
    private ZoneSpawner<PickupableItem> _zoneSpawner;
    private LazyPoolSpawner<PickupableItem> _poolSpawner;
    
    private void Awake()
    {
        _itemSpawner = GetComponent<PickupableItemSpawner>();
        var collider = GetComponent<Collider2D>();
        _poolSpawner = new LazyPoolSpawner<PickupableItem>(_itemSpawner, _poolCapacity);
        _zoneSpawner = new ZoneSpawner<PickupableItem>(_poolSpawner, collider.bounds);
    }

    private void OnDestroy()
    {
        _poolSpawner.Dispose();
        _poolSpawner = null;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, 0.5f);
    }

    [Button]
    private void Spawn()
    {
        var item = _zoneSpawner.Spawn();
        
        if (item == null)
            return;

        item.gameObject.SetActive(true);
    }
}
