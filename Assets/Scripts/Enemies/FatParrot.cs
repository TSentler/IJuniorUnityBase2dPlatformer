using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using Switchers;

public class FatParrot : SerializedMonoBehaviour, IActivatable
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _speedBack = 13f;
    [SerializeField] private IActivatable _wayPointMovment;

    private Vector3 _startPoint;
    private Quaternion _startRotation;

    public bool IsActive => _wayPointMovment.IsActive;

    private void OnValidate()
    {
        _wayPointMovment.VerifyNotNull<IActivatable>(nameof(_wayPointMovment));
    }

    private void Awake()
    {
        _startPoint = transform.position;
        _startRotation = transform.rotation;
    }

    public void Activate()
    {
        _wayPointMovment.Activate();
    }

    public void Deactivate()
    {
        _wayPointMovment.Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if (_wayPointMovment.IsActive == false && transform.position == _startPoint)
            return;

        Rotate();
        ReturnBack();
    }

    private void Rotate()
    {
        var rightTurn = Quaternion.Euler(0f, 0f, _rotationSpeed * Time.deltaTime);
        transform.rotation *= rightTurn;
    }

    private void ReturnBack()
    {
        if (_wayPointMovment.IsActive == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPoint, _speedBack * Time.deltaTime);
            
            if (transform.position == _startPoint)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        transform.position = _startPoint;
        transform.rotation = _startRotation;
    }
}
