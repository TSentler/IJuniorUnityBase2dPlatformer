using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Tools;
using Player;
using Control;

namespace Spawner
{
    public class PlayerSpawner : SerializedMonoBehaviour
    {
        [SerializeField] private DefaultCore _corePrefab;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private PositionConstraint _cameraConstraint;
        [SerializeField] private IInputHandler _inputHandler;

        public IPlayerCore PlayerCore { get; private set; }

        private void OnValidate()
        {
            _corePrefab.VerifyNotNull<DefaultCore>(nameof(_corePrefab));
            _cameraConstraint.VerifyNotNull<PositionConstraint>(nameof(_cameraConstraint));
            _inputHandler.VerifyNotNull<PlayerInputHandler>(nameof(_inputHandler));
        }

        private void Awake()
        {
            _corePrefab.gameObject.SetActive(false);
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var coreInstance = Instantiate(_corePrefab, transform);
            Initialize(coreInstance);
            SetCameraConstraint(coreInstance);

            coreInstance.gameObject.SetActive(true);
            
            PlayerCore = coreInstance;
        }

        private void SetCameraConstraint(DefaultCore coreInstance)
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

        private void Initialize(DefaultCore coreInstance)
        {
            var animator = coreInstance.gameObject.GetComponent<Animator>();
            var rigidbody = coreInstance.gameObject.GetComponent<Rigidbody2D>();

            

            coreInstance.Initialize(_inputHandler, rigidbody, animator);
        }
    }
}
