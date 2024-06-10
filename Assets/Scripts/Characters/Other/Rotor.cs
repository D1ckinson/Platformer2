using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rotor : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _rotationDegrees = 180;
    private float _rotateVelocity = 0.5f;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    private void Update() =>
        Rotate();

    private void Rotate()
    {
        if (_rigidbody.velocity.x > _rotateVelocity)
            transform.rotation = Quaternion.identity;
        else if (_rigidbody.velocity.x < -_rotateVelocity)
            transform.rotation = Quaternion.Euler(0, _rotationDegrees, 0);
    }
}
