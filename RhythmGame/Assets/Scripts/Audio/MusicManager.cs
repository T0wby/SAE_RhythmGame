using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    #region Fields
    private string m_tag = "MusicManager";

    [SerializeField] private NotifyMusicRequestCollection m_entityRequests;

    [SerializeField] private AudioMixerGroup m_mixingGroup;

    [SerializeField] private int m_poolSize;
    [SerializeField] private GameObject m_audioObjectPrefab;

    [SerializeField] private List<EntityMusicCollection> m_entityMusicCollections = new List<EntityMusicCollection>();


    private Dictionary<ESources, Dictionary<EMusicTypes, ClipLibrary<EMusicTypes>>> m_entityMusicCollectionDictionary = new Dictionary<ESources, Dictionary<EMusicTypes, ClipLibrary<EMusicTypes>>>();

    private ObjectPool<AudioObject> m_pool;

    private AudioObject _lastCreatedMusicObject = null;


    #endregion

    #region Properties
    public AudioObject LastCreatedMusicObject { get => _lastCreatedMusicObject; }

    #endregion

    #region Unity
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(m_tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        m_pool = new ObjectPool<AudioObject>(m_audioObjectPrefab, m_poolSize, transform);

        foreach (EntityMusicCollection MusicCollection in m_entityMusicCollections)
        {
            foreach (ESources Source in System.Enum.GetValues(typeof(ESources)))
            {
                if (MusicCollection.Source == Source)
                {
                    m_entityMusicCollectionDictionary.Add(Source, MusicCollection.Collection);
                    break;
                }
            }
        }

        m_entityRequests.OnAdd += OnEntitySound;
    }

    private void Update()
    {
        if (GameManager.Instance.MusicPaused && _lastCreatedMusicObject != null)
        {
            _lastCreatedMusicObject.ExtraDelay += Time.unscaledDeltaTime * 1000;
        }
    }
    #endregion

    #region Methods
    private void OnEntitySound(EntityMusicRequest _request)
    {
        _lastCreatedMusicObject = m_pool.GetItem();
        AudioSource tmpSource = _lastCreatedMusicObject.Source;
        ClipLibrary<EMusicTypes> library = m_entityMusicCollectionDictionary[_request.Source][_request.Type];
        _lastCreatedMusicObject.name = $"{_request.Source} {_request.Type}-Sound";

        _lastCreatedMusicObject.transform.parent = _request.Parent;
        _lastCreatedMusicObject.transform.position = _request.Position;

        tmpSource.outputAudioMixerGroup = m_mixingGroup;
        tmpSource.clip = library.FileList[0];
        tmpSource.volume = library.Volume;

        tmpSource.Play();
        _lastCreatedMusicObject.SetCountdown((int)(tmpSource.clip.length * 1000), GameManager.Instance.EndGame);

        m_entityRequests.Remove(_request);
    } 

    public void PauseMusic()
    {
        if (_lastCreatedMusicObject != null)
        {
            _lastCreatedMusicObject.PauseSound();
        }
    }

    public void ResumeMusic()
    {
        if (_lastCreatedMusicObject != null)
        {
            _lastCreatedMusicObject.ResumeSound();
        }
    }
    #endregion
}
