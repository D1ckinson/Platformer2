using UnityEngine;

public class PatrolmanNavigator : MonoBehaviour, ITellDirection
{
    [SerializeField] private Transform _patrolPointsParent;

    private Transform[] _patrolPoints;
    private int _index;

    public float Direction => Mathf.Sign(_patrolPoints[_index].position.x - transform.position.x);
    public bool IsJump => false;

    void Start()
    {
        _patrolPoints = new Transform[_patrolPointsParent.childCount];

        for (int i = 0; i < _patrolPoints.Length - 1; i++)
            _patrolPoints[i] = _patrolPointsParent.GetChild(i);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == _patrolPoints[_index])
            _index = (_index + 1) % _patrolPoints.Length;
    }
}
