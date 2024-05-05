using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private Coin _coin;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 _spawnForce;

    private readonly int SpawnTrigger = Animator.StringToHash("Spawn");//порядок

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

    private void Spawn()
    {
        Coin coin = Instantiate(_coin, transform.position, Quaternion.identity);

        if (coin.TryGetComponent(out Rigidbody2D rigidbody))
            rigidbody.velocity = GetSpawnForce();
    }

    private Vector2 GetSpawnForce() =>
        new()
        {
            x = Random.Range(-_spawnForce.x, _spawnForce.x),
            y = Random.Range(0, _spawnForce.y)
        };
}
