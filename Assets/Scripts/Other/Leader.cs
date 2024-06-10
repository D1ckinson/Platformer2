using System;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public event Action StopLead;

    protected void CallStopLeadEvent() =>
        StopLead?.Invoke();
}
