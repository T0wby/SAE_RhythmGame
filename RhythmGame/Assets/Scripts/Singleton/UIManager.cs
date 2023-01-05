using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _endscreen;

    #region Unity
    protected override void Awake()
    {
        _isInAllScenes = false;
        base.Awake();

    }
    #endregion

    #region Methods

    public void OpenEndscreen()
    {
        _endscreen.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void GoBackToMenu()
    {
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene("LevelSelection");
    }

    #endregion
}
