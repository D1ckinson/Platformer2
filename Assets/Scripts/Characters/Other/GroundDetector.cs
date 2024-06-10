using UnityEngine;

public class GroundDetector
{
    private Rigidbody2D _rigidbody;
    private LayerMask _ground;
    private float _groundDistance = 0.6f;

    public bool OnGround => Physics2D.Raycast(_rigidbody.transform.position, Vector2.down, _groundDistance, _ground);

    public GroundDetector(Rigidbody2D rigidbody, LayerMask ground)
    {
        _rigidbody = rigidbody;
        _ground = ground;
    }
}
