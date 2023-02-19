using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    #region Fields
    [SerializeField] private NotifyMusicRequestCollection _musicRequestCollection;
    [SerializeField] private float _musicOffset = 5f;
    [SerializeField] private uint _songBPM = 0;
    [SerializeField] private float _currentSongPos = 0;
    [SerializeField] private float _dspSongTime = 0;
    [SerializeField] private float _beatPerSec = 0;
    [SerializeField] private float _currentBeatPos = 0;
    private bool _countdownEnded = false;
    private AudioVisualization _audioVisualization;
    private RateSpawner[] _rateSpawners= null;
    private GameObject _musicManagerObj;
    private MusicManager _musicManager;
    #endregion

    #region Properties
    public float CurrentBeatPos => _currentBeatPos;
    public float CurrentSongPos => _currentSongPos;

    public float MusicOffset { get => _musicOffset; }
    public float BeatPerSec { get => _beatPerSec; }
    #endregion

    #region Unity
    private void Awake()
    {
        GameManager.Instance.Conductor = this;
        _musicManager = FindObjectOfType<MusicManager>();
        _musicManagerObj = _musicManager.gameObject;
        _audioVisualization = FindObjectOfType<AudioVisualization>();
        _rateSpawners = FindObjectsOfType<RateSpawner>();
    }

    private void Start()
    {
        _songBPM = GameManager.Instance.ActiveLevel.Bpm;
        _beatPerSec = 60f / _songBPM;
        _dspSongTime = (float)AudioSettings.dspTime;
        StartCoroutine(MusicCountdown());
    }

    private void Update()
    {
        if (!_countdownEnded) return;

        _currentSongPos = (float) (AudioSettings.dspTime - _dspSongTime - _musicOffset - (_musicManager.LastCreatedMusicObject.ExtraDelay * 0.001f));
        _currentBeatPos = _currentSongPos / _beatPerSec;
    }

    #endregion

    #region Methods
    private void StartLevelMusic()
    {
        _countdownEnded = true;
        EMusicTypes types = (EMusicTypes)System.Enum.Parse(typeof(EMusicTypes), GameManager.Instance.ActiveLevel.name, true);

        _musicRequestCollection.Add(EntityMusicRequest.Request(ESources.LEVEL, types, _musicManagerObj.transform));
        for (int i = 0; i < _rateSpawners.Length; i++)
        {
            _rateSpawners[i].StartSpawning(); 
        }
    }

    public void StopConductor()
    {
        _countdownEnded = false;
    }

    public void StartConductor()
    {
        _countdownEnded = true;
    }

    #endregion

    #region Enumerators
    private IEnumerator MusicCountdown()
    {
        yield return new WaitForSeconds(MusicOffset);
        StartLevelMusic();
        if (_audioVisualization != null)
            _audioVisualization.GetMusic();
    } 
    #endregion
}
