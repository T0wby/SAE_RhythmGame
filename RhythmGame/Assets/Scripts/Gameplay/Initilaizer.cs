using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initilaizer : MonoBehaviour
{
    [SerializeField] private float _logoTime = 5f;
    
    private void Awake()
    {
        StartCoroutine(ShowLogo());
    }
    private IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(_logoTime);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
