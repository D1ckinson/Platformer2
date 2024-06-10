using System;

public class Coin : Follower, ICollectible
{
    private Action<int> _addScore;
    private int _score = 5;

    public override void SetTarget(Leader target)
    {
        base.SetTarget(target);
        target.StopLead += SetTargetNull;
    }

    protected override void SetTargetNull()
    {
        _target.StopLead -= SetTargetNull;

        base.SetTargetNull();
    }

    public void Collect()
    {
        _addScore?.Invoke(_score);
        Destroy(gameObject);
    }

    public void SetAddScore(Action<int> addScore) =>
        _addScore = addScore;
}
