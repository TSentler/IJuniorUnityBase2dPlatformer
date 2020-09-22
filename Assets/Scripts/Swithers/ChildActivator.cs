using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Switchers
{
    public class ChildActivator : SerializedMonoBehaviour, IActivatable
    {
        private IActivatable[] _activators;

        public bool IsActive { get; private set; }

        private void Awake()
        {
            _activators = GetComponentsInChildren<IActivatable>().Skip(1).ToArray();
        }

        public void Activate()
        {
            foreach (var spawner in _activators)
            {
                spawner.Activate();
            }

            IsActive = true;
        }

        public void Deactivate()
        {
            foreach (var spawner in _activators)
            {
                spawner.Deactivate();
            }

            IsActive = false;
        }
    }
}
