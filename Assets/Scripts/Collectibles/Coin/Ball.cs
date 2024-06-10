using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : Leader
{
    private Rigidbody2D _rigidbody;
    private Action _disable;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (_rigidbody.velocity == Vector2.zero)
            CallStopLeadEvent();
    }

    private void OnEnable() =>
        StopLead += _disable;

    private void OnDisable() =>
        StopLead -= _disable;

    public void SetDisableAction(Action disable) =>
        _disable = disable;

    public void GetForce(Vector2 force) =>
        _rigidbody.velocity = force;
}
