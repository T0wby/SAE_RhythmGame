using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private NotifyMusicRequestCollection _requestCollection;
    [SerializeField] private AudioSource _menuSource;
    //private MusicManager _musicManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _menuSource.Pause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _menuSource.UnPause();
        }
    }
}
