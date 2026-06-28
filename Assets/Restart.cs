using System;
using prototype3;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private Vector2 _startLocation;
    private void Awake()
    {
        _startLocation = transform.position;
    }

    private void OnEnable()
    {
        EventManager.Reset += ResetObject;
    }

    private void OnDisable()
    {
        EventManager.Reset -=  ResetObject;
    }

    private void ResetObject()
    {
        transform.position = _startLocation;
    }
}
