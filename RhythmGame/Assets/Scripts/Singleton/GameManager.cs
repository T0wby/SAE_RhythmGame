using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    [SerializeField] private List<GameState> _gameStates = null;
    [SerializeField] private GameState _activeState = null;
    #endregion


    #region Methods

    #region Gamestates

    public void ChangeGamestate<T>() where T : GameState
    {
        foreach (GameState state in _gameStates)
        {
            if(state.GetType() == typeof(T))
            {
                _activeState = state;
            }
        }
    }

    #endregion

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = true;
        base.Awake();

    }
    #endregion

    #endregion
}
