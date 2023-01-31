using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private NotifyMusicRequestCollection _requestCollection;
    //private MusicManager _musicManager;

    private void Awake()
    {
        //_musicManager = FindObjectOfType<MusicManager>();
        //StartCoroutine(PlayMenuMusic());
    }

    //private IEnumerator PlayMenuMusic()
    //{
    //    while (true)
    //    {
    //        _requestCollection.Add(EntityMusicRequest.Request(ESources.MENU, EMusicTypes.MENUMUSIC, Camera.main.transform));
    //        yield return new WaitForSeconds(_musicManager.LastCreatedMusicObject.Source.clip.length + 2);
    //    }
    //}
}
