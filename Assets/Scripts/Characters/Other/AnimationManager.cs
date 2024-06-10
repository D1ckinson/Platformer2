using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;

    private readonly int YVelocity = Animator.StringToHash("YVelocity");
    private readonly int OnGround = Animator.StringToHash("OnGround");
    private readonly int IsMove = Animator.StringToHash("IsMove");

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = new(_rigidbody, _ground);
    }

    private void Update()
    {
        _animator.SetBool(IsMove, _rigidbody.velocity.x != 0);
        _animator.SetBool(OnGround, _groundDetector.OnGround);
        _animator.SetFloat(YVelocity, _rigidbody.velocity.y);
    }
}
