using System;
using System.Collections;
using UnityEngine;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    private float _health = 100;
    private float _damage = 20;
    private float _speed = 10;
    private float _jumpHeight = 5;
    private KeyCode _jumpKey = KeyCode.Space;
    private float _onGroundDistance = 0.6f;
    [SerializeField] private LayerMask _ground;

    private float _rotationDegrees = 180;
    private float _rotateVelocity = 0.5f;

    private float _moveDirection;
    private bool _isJumping;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    //private Func<bool> _condition;
    private bool _isMovementLock = false;
    private bool _isStateLock = false;
    private bool _a = true;

    private string _awakeStateName = "Appearing";
    private string _idleStateName = "Idle";
    private string _moveStateName = "Run";
    private string _jumpStateName = "Jump";
    private string _fallStateName = "Fall";
    private string _takeDamageStateName = "Hit";
    private string _dieStateName = "Disappearing";

    private bool OnGround => Physics2D.Raycast(transform.position, Vector2.down, _onGroundDistance, _ground);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //SetAwakeState();
    }

    private void Start()
    {
        //SetAwakeState();
    }

    private void OnEnable()
    {
        SetAwakeState();
    }

    private void Update()
    {
        if (_a)
        {
            SetAwakeState();
            _a = false;
        }

        ReadInputs();
        Rotate();
        Debug.Log(_isStateLock);
        DeterminateState();
    }

    private void FixedUpdate()
    {
        if (_isMovementLock == true)
            return;

        Move();
        Jump();
    }

    private void ReadInputs()
    {
        _moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(_jumpKey) && OnGround)
            _isJumping = true;
    }

    private void Move()
    {
        _rigidbody.velocity = new(_moveDirection * _speed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_isJumping)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpHeight);
            _isJumping = false;
        }
    }

    private void Rotate()
    {
        if (_rigidbody.velocity.x > _rotateVelocity)
            transform.rotation = Quaternion.identity;
        else if (_rigidbody.velocity.x < -_rotateVelocity)
            transform.rotation = Quaternion.Euler(0, _rotationDegrees, 0);
    }

    private void DeterminateState()
    {
        if (_isStateLock)
        {
            Debug.Log("ÇÀËÎ×ÅÍÎ!");
            return;
        }

        if (OnGround)
        {
            if (_rigidbody.velocity.x != 0)//moveState
            {
                SetMoveState();
            }

            if (_rigidbody.velocity.x == 0)//idleState
            {
                SetIdleState();
            }
        }
        else
        {
            if (_rigidbody.velocity.y > 0)//jumpState
            {
                SetJumpState();
            }

            if (_rigidbody.velocity.y < 0)//fallState
            {
                SetFallState();
            }
        }

        if (false)//takeDamageState
        {
            SetTakeDamageState();
        }

        if (false)//dieState
        {
            SetDieState();
        }
    }

    private IEnumerator LockBool(Func<bool> lockCondition, bool value)
    {
        value = true;

        while (lockCondition.Invoke())
        {
            yield return null;
        }

        value = false;
    }

    private IEnumerator LockState(Func<bool> condition)
    {
        _isStateLock = true;
        Debug.Log("1");
        Debug.Log(_isStateLock);

        while (condition.Invoke())
        {
            yield return null;
        }

        _isStateLock = false;
    }

    private IEnumerator LockMovement(Func<bool> condition)
    {
        _isMovementLock = true;

        while (condition.Invoke())
        {
            yield return null;
        }

        _isMovementLock = false;
    }

    private void SetAwakeState()
    {
        _isStateLock = true;
        _animator.Play(_awakeStateName);
        Func<bool> condition = () => _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToString() == _awakeStateName;//ëîêàëüíàÿ ôóíêöèÿ?

        StartCoroutine(LockState(condition));
        StartCoroutine(LockMovement(condition));

        //Debug.Log(_awakeStateName);
        //Debug.Log(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToString());
        //Debug.Log(_isStateLock);
    }

    private void SetIdleState()
    {
        _animator.Play(_idleStateName);
    }

    private void SetMoveState()
    {
        _animator.Play(_moveStateName);
    }

    private void SetJumpState()
    {
        _animator.Play(_jumpStateName);

        Func<bool> condition = () => _rigidbody.velocity.y > 0;

        LockState(condition);
    }

    private void SetFallState()
    {
        _animator.Play(_fallStateName);

        Func<bool> condition = () => _rigidbody.velocity.y < 0;

        LockState(condition);
    }

    private void SetTakeDamageState()
    {
        _animator.Play(_takeDamageStateName);

        //
    }

    private void SetDieState()
    {
        _animator.Play(_dieStateName);

        //
    }
}
