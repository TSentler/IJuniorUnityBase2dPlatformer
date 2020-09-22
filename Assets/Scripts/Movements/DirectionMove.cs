using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMove : MonoBehaviour
{
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed;


    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
