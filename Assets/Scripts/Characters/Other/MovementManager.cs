using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputManager))]
public class MovementManager : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _ground;

    private Rigidbody2D _rigidbody;
    private InputManager _inputManager;
    private float _jumpDistance = 0.6f;

    private bool OnGround => Physics2D.Raycast(transform.position, Vector2.down, _jumpDistance, _ground);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        Move(_inputManager.Direction);
        Jump(_inputManager.IsJump);
    }

    private void Move(float direction)
    {
        _rigidbody.velocity = new(direction * _speed, _rigidbody.velocity.y);
    }

    private void Jump(bool isJump)
    {
        if (isJump == false)
        {
            return;
        }

        if (OnGround)
        {
            _rigidbody.velocity = new(0, _jumpForce);
        }
    }
}
