using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initilaizer : MonoBehaviour
{
    [SerializeField] private float _logoTime = 5f;
    private IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(_logoTime);
        GameManager.Instance.ChangeGamestate<GameStateMenu>();
        // TODO: Load Menu scene
    }

    private void Awake()
    {
        GameManager.Instance.IsInAllScenes = true;
        GameManager.Instance.ChangeGamestate<GameStateLogo>();
        StartCoroutine(ShowLogo());
    }
}
