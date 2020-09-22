using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using Switchers;

namespace Spawner
{
    public class SpawnerRepeat : MonoBehaviour, IActivatable
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _repeatRate = 2f;

        private float _elapsed;

        public bool IsActive => enabled;

        private void OnValidate()
        {
            _prefab.VerifyNotNull<GameObject>(nameof(_prefab));
        }

        private void Awake()
        {
            _prefab.SetActive(false);
        }

        private void Update()
        {
            if (_elapsed <= 0f)
            {
                _elapsed = _repeatRate;
                Spawn();
            }
            _elapsed -= Time.deltaTime;
        }

        private void Spawn()
        {
            var platform = Instantiate(_prefab, transform);
            platform.SetActive(true);
        }

        public void Activate()
        {
            enabled = true;
        }

        public void Deactivate()
        {
            enabled = false;
        }
    }
}
