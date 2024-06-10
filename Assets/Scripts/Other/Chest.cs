using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Coin _coin;
    [SerializeField] private Ball _ball;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 _spawnForce;
    [SerializeField] private int _BallsQuantity = 2;
    [SerializeField] private Scoreboard _scoreboard;

    private readonly int SpawnTrigger = Animator.StringToHash("Spawn");

    private Pool<Ball> _pool;

    private void Awake() =>
        _pool = new(PreloadFunc, GetAction, ReturnAction, _BallsQuantity);

    private void Start() =>
        StartCoroutine(StartSpawnCycle());

    private IEnumerator StartSpawnCycle()
    {
        WaitForSeconds wait = new(_spawnTime);

        while (true)
        {
            animator.SetTrigger(SpawnTrigger);

            yield return wait;
        }
    }

    private void Spawn() =>
        _pool.Get();

    private Vector2 GetSpawnForce() =>
        new()
        {
            x = Random.Range(-_spawnForce.x, _spawnForce.x),
            y = Random.Range(0, _spawnForce.y)
        };

    private Ball PreloadFunc()
    {
        Ball ball = Instantiate(_ball);
        ball.SetDisableAction(() => _pool.Return(ball));

        return ball;
    }

    private void GetAction(Ball ball)
    {
        ball.gameObject.SetActive(true);
        ball.transform.position = transform.position;

        Coin coin = Instantiate(_coin);
        coin.SetAddScore(_scoreboard.AddScore);
        coin.SetTarget(ball);

        ball.GetForce(GetSpawnForce());
    }

    private void ReturnAction(Ball ball)
    {
        ball.gameObject.SetActive(false);
    }
}
