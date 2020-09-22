using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmZone : MonoBehaviour, IAlarmZone
{
    public event UnityAction<bool> Alarmed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayerCore playerCore))
        {
            Alarmed?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayerCore playerCore))
        {
            Alarmed?.Invoke(false);
        }
    }
}
