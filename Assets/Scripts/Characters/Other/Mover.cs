using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _ground;

    private ITellDirection _motionNavigator;
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = new(_rigidbody, _ground);

        if (TryGetComponent(out ITellDirection direction))
            _motionNavigator = direction;
        else
            Debug.Log("Отсутствует ITellDirection");
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move() =>
        _rigidbody.velocity = new(_motionNavigator.Direction * _speed, _rigidbody.velocity.y);

    private void Jump()
    {
        if (_motionNavigator.IsJump && _groundDetector.OnGround)
            _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpForce);
    }
}
