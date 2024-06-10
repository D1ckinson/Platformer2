using System;
using UnityEngine;
using System.Collections.Generic;

public class Pool<T>
{
    private Func<T> _preloadFunc;
    private Action<T> _getAction;
    private Action<T> _returnAction;

    private Queue<T> _pool = new();

    public Pool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        if (_preloadFunc == null)
        {
            Debug.Log("Preload Func is null");
            return;
        }

        for (int i = 0; i < preloadCount; i++)
            Return(_preloadFunc.Invoke());
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc.Invoke();

        _getAction.Invoke(item);

        return item;
    }

    public void Return(T item)
    {
        _returnAction.Invoke(item);
        _pool.Enqueue(item);
    }
}
