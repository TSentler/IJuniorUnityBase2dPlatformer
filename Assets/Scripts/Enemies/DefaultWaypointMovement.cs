using Switchers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWaypointMovement : MonoBehaviour, IActivatable
{
    [SerializeField] private Transform _pathParent;
    [SerializeField] private float _speed = 13f;

    private Vector3[] _points;
    private int _currentPoint;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        _points = new Vector3[_pathParent.childCount];
        for (int i = 0; i < _pathParent.childCount; i++)
        {
            _points[i] = _pathParent.GetChild(i).position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_points.Length == 0 || IsActive == false)
            return;

        var target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (IsActive && transform.position == target)
        {
            _currentPoint++;
            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        _currentPoint = 0;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
