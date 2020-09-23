using Audio;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Player;

public class DoorAlarm : SerializedMonoBehaviour, IAlarmZone
{
    [SerializeField] private IVolumable _volumeSmooth;
    [MinMaxSlider(0f, 1f, true)]
    [SerializeField] private Vector2 _minMaxVolume = new Vector2(0, 0.5f);
    [SerializeField] private bool _isLeftExit;

    public event UnityAction Alarmed;
    public event UnityAction Calmed;

    private float MinVolume => _minMaxVolume.x;
    private float MaxVolume => _minMaxVolume.y;
    private int ExitDirection => _isLeftExit ? -1 : 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMovement player))
        {
            AlarmOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IMovement player) 
            && ExitDirection * (transform.position.x - player.Position.x) < 0f )
        {
            AlarmOff();
        }
    }

    private void AlarmOn()
    {
        _volumeSmooth.SetVolume(MaxVolume);
        Alarmed?.Invoke();
    }

    private void AlarmOff()
    {
        _volumeSmooth.SetVolume(MinVolume);
        Calmed?.Invoke();
    }
}
