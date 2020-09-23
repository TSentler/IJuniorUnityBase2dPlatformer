using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmZone : MonoBehaviour, IAlarmZone
{
    public event UnityAction Alarmed;
    public event UnityAction Calmed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMovement player))
        {
            Alarmed?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMovement player))
        {
            Calmed?.Invoke();
        }
    }
}
