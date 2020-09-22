using Sirenix.OdinInspector;
using Switchers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAlarmZone
{
    event UnityAction<bool> Alarmed;
}

public class HouseAlarm : SerializedMonoBehaviour, IActivatable
{
    [SerializeField] private IActivatable[] _activators;
    [SerializeField] private IAlarmZone[] _alarmZones;

    public bool IsActive { get; private set; }

    private void OnValidate()
    {
        if (_activators == null)
        {
            throw new ArgumentNullException(nameof(_activators));
        }
    }

    [Button]
    private void GetAlarmZonesInChildren()
    {
        _alarmZones = GetComponentsInChildren<IAlarmZone>();
    }

    private void Awake()
    {
        foreach (var zone in _alarmZones)
        {
            zone.Alarmed += Alarmed;
        }
    }

    private void OnDestroy()
    {
        foreach (var zone in _alarmZones)
        {
            zone.Alarmed -= Alarmed;
        }
    }

    private void Alarmed(bool isAlarm)
    {
        if (isAlarm)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        if (IsActive)
            return;
            
        IsActive = true;

        foreach (var activator in _activators)
        {
            activator.Activate();
        }
    }

    public void Deactivate()
    {
        if (IsActive == false)
            return;

        IsActive = false;

        foreach (var activator in _activators)
        {
            activator.Deactivate();
        }
    }
}
