using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : ObserverListener
{
    private GameManager _gameManager = null;

    [SerializeField] private UnityEvent _initResponse = null;
    [SerializeField] private UnityEvent _updateResponse = null;
    [SerializeField] private UnityEvent _finResponse = null;

    protected virtual void OnInit() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFinalize() { }

    public void InvokeInitialize() => _initResponse?.Invoke();
    public void InvokeUpdate() => _updateResponse?.Invoke();
    public void InvokeFinalize() => _finResponse?.Invoke();

    protected new void Awake()
    {
        base.Awake();
        _gameManager ??= GameManager.Instance;
        _initResponse ??= new UnityEvent();
        _updateResponse ??= new UnityEvent();
        _finResponse ??= new UnityEvent();

    }

    protected new void OnEnable()
    {
        base.OnEnable();
        _initResponse?.AddListener(OnInit);
        _updateResponse?.AddListener(OnUpdate);
        _finResponse?.AddListener(OnFinalize);
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        _finResponse?.RemoveListener(OnFinalize);
        _updateResponse?.RemoveListener(OnUpdate);
        _initResponse?.RemoveListener(OnInit);
    }
}
