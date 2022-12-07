using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManaging : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    private void LoadLevel()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(LoadLevel);
    }
}
