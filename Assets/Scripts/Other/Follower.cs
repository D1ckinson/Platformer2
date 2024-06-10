using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] protected Leader _target;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        if (_target != null)
            transform.position = _target.transform.position + _offset;
    }

    public virtual void SetTarget(Leader target) =>
        _target = target;

    protected virtual void SetTargetNull() =>
        _target = null;
}
