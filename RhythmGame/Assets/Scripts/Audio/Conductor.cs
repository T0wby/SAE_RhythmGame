using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Conductor : MonoBehaviour
{
    #region Fields
    [SerializeField] private NotifyMusicRequestCollection _musicRequestCollection;
    [SerializeField] private float _musicOffset = 5f;
    [SerializeField] private float _songBPM = 0;
    [SerializeField] private float _currentSongPos = 0;
    [SerializeField] private float _dspSongTime = 0;
    [SerializeField] private float _beatPerSec = 0;
    [SerializeField] private float _currentBeatPos = 0;
    private bool _isSongStarted = false;
    #endregion

    #region Properties
    public float CurrentBeatPos => _currentBeatPos;
    public float CurrentSongPos => _currentSongPos;

    public float MusicOffset { get => _musicOffset; }
    #endregion

    #region Unity
    private void Awake()
    {
        GameManager.Instance.Conductor = this;
        GameObject[] sfxManager = GameObject.FindGameObjectsWithTag("SFXManager");
        if (sfxManager[0])
        {
            if (sfxManager[0].GetComponent<SFXManager>())
                sfxManager[0].GetComponent<SFXManager>().InitPool();
        }
    }

    private void Start()
    {
        _beatPerSec = 60f / _songBPM;
        _dspSongTime = (float)AudioSettings.dspTime;
        //_currentSongPos = -_musicOffset;
        StartCoroutine(MusicCountdown());
    }

    private void Update()
    {
        if (!_isSongStarted)
            return;

        _currentSongPos = (float) (AudioSettings.dspTime - _dspSongTime - _musicOffset);
        _currentBeatPos = _currentSongPos / _beatPerSec;
    }

    #endregion

    #region Methods
    private void StartLevelMusic()
    {
        _isSongStarted = true;
        EMusicTypes types = (EMusicTypes)System.Enum.Parse(typeof(EMusicTypes), GameManager.Instance.ActiveLevel.name, true);

        _musicRequestCollection.Add(EntityMusicRequest.Request(ESources.LEVEL, types, Camera.main.transform));
    }

    public void StopConductor()
    {
        _isSongStarted = false;
    }

    #endregion

    #region Enumerators
    private IEnumerator MusicCountdown()
    {
        yield return new WaitForSeconds(MusicOffset);
        StartLevelMusic();
    } 
    #endregion
}
