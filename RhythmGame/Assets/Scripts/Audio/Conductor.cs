using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private NotifyMusicRequestCollection _musicRequestCollection;
    [SerializeField] private float _musicOffset = 5f;
    [SerializeField] private float _songBPM = 0;
    [SerializeField] private float _currentSongPos = 0;
    [SerializeField] private float _dspSongTime = 0;
    [SerializeField] private float _beatPerSec = 0;
    [SerializeField] private float _currentBeatPos = 0;
    private bool IsSongStarted = false;

    public float CurrentBeatPos => _currentBeatPos;

    private void Start()
    {
        _beatPerSec = 60f / _songBPM;
        _dspSongTime = (float)AudioSettings.dspTime;
        StartCoroutine(MusicCountdown());
    }

    private void Update()
    {
        if (!IsSongStarted)
            return;

        _currentSongPos = (float) (AudioSettings.dspTime - _dspSongTime);
        _currentBeatPos = _currentSongPos / _beatPerSec;
    }

    private void StartLevelMusic()
    {
        IsSongStarted = true;
        _musicRequestCollection.Add(EntityMusicRequest.Request(ESources.LEVEL, EMusicTypes.INGAMEMUSIC, Camera.main.transform));
    }

    private IEnumerator MusicCountdown()
    {
        yield return new WaitForSeconds(_musicOffset);
        StartLevelMusic();
    }
}
