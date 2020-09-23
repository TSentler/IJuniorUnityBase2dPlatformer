using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Tools;
using Player;

namespace Spawner
{
    public class PlayerSpawner : SerializedMonoBehaviour
    {
        [SerializeField] private SimplePhysicsMovement _playerPrefab;
        [SerializeField] private PositionConstraint _cameraConstraint;

        private void OnValidate()
        {
            _playerPrefab.VerifyNotNull<SimplePhysicsMovement>(nameof(_playerPrefab));
            _cameraConstraint.VerifyNotNull<PositionConstraint>(nameof(_cameraConstraint));
        }

        private void Awake()
        {
            _playerPrefab.gameObject.SetActive(false);
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var coreInstance = Instantiate(_playerPrefab, transform);
            SetCameraConstraint(coreInstance);

            coreInstance.gameObject.SetActive(true);
        }

        private void SetCameraConstraint(SimplePhysicsMovement coreInstance)
        {
            var sourceCount = _cameraConstraint.sourceCount;
            for (int i = 0; i < sourceCount; i++)
            {
                _cameraConstraint.RemoveSource(0);
            }
            ConstraintSource coreSource = new ConstraintSource();
            coreSource.sourceTransform = coreInstance.transform;
            coreSource.weight = 1;
            _cameraConstraint.AddSource(coreSource);
        }
    }
}
