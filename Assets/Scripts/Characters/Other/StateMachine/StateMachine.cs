using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public void AddState(IState state)
    {
        if (_states.ContainsKey(state.GetType()))
            return;

        _states.Add(state.GetType(), state);
    }

    public void SetState(Type stateType)
    {
        if (_states.TryGetValue(stateType, out IState state) == false)
            return;

        if (_currentState == state)
            return;

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
