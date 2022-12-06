using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private NotifyMusicRequestCollection _musicRequestCollection;
    [SerializeField] private float _musicOffset = 5f;
    [SerializeField] private float _songBPM = 0;
    private float _currentSongPos = 0;
    private float _dspSongTime = 0;
    private float _beatPerSec = 0;
    private float _currentBeat = 0;

    private void Start()
    {
        _beatPerSec = 60f / _songBPM;
        _dspSongTime = (float)AudioSettings.dspTime;

        StartCoroutine(MusicCountdown());
    }

    private void Update()
    {
        _currentSongPos = (float) (AudioSettings.dspTime - _dspSongTime);
        _currentBeat = _currentSongPos / _beatPerSec;
    }

    private void StartLevelMusic()
    {
        _musicRequestCollection.Add(EntityMusicRequest.Request(ESources.LEVEL, EMusicTypes.INGAMEMUSIC, Camera.main.transform));
    }

    private IEnumerator MusicCountdown()
    {
        yield return new WaitForSeconds(_musicOffset);
        StartLevelMusic();
    }
}
