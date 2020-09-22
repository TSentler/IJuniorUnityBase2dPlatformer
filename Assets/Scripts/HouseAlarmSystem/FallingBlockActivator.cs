using Player;
using Switchers;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FallingBlockActivator : MonoBehaviour, IActivatable
{
    [SerializeField] private BoxCollider2D _blockCollider;

    public bool IsActive => _blockCollider.enabled == false;

    private void OnValidate()
    {
        _blockCollider.VerifyNotNull<BoxCollider2D>(nameof(_blockCollider));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayerCore playerCore))
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayerCore playerCore))
        {
            Deactivate();
        }
    }

    private void OnDestroy()
    {
        Deactivate();
    }

    public void Activate()
    {
        _blockCollider.enabled = false;
    }

    public void Deactivate()
    {
        _blockCollider.enabled = true;
    }
}
