using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
[RequireComponent(typeof(MovementManager))]
[RequireComponent(typeof(DamageReceiver))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(DamageSours))]
[RequireComponent(typeof(Collector))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rotor))]
public class Player : MonoBehaviour
{
    private AnimationManager _animationManager;
    private MovementManager _movementManager;
    private DamageReceiver _damageReceiver;
    private InputManager _inputManager;
    private DamageSours _damageSours;
    private Collector _collector;
    private Health _health;
    private Rotor _rotor;

    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _movementManager = GetComponent<MovementManager>();
        _damageReceiver = GetComponent<DamageReceiver>();
        _inputManager = GetComponent<InputManager>();
        _damageSours = GetComponent<DamageSours>();
        _collector = GetComponent<Collector>();
        _health = GetComponent<Health>();
        _rotor = GetComponent<Rotor>();
    }

    private void Start()
    {

    }
}
