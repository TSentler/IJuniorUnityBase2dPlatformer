using Pool;
using Switchers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pickupable
{
    public abstract class PickupableItem : MonoBehaviour, IActivatable, IPoolable
    {
        public bool IsActive => gameObject.activeSelf;

        public event UnityAction<IPoolable> PoolReturning;
        public event UnityAction<IPoolable> PoolRemoving;

        protected virtual void OnDestroy()
        {
            PoolRemoving?.Invoke(this);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            PoolReturning?.Invoke(this);
        }
    }
}