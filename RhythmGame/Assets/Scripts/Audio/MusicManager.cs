using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private string m_tag = "MusicManager";

    [SerializeField] private NotifyMusicRequestCollection m_entityRequests;

    [SerializeField] private AudioMixerGroup m_mixingGroup;

    [SerializeField] private int m_poolSize;
    [SerializeField] private GameObject m_audioObjectPrefab;

    [SerializeField] private List<EntityMusicCollection> m_entityMusicCollections = new List<EntityMusicCollection>();


    private Dictionary<ESources, Dictionary<EMusicTypes, ClipLibrary<EMusicTypes>>> m_entityMusicCollectionDictionary = new Dictionary<ESources, Dictionary<EMusicTypes, ClipLibrary<EMusicTypes>>>();

    private ObjectPool<AudioObject> m_pool;
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

    private void OnEntitySound(EntityMusicRequest _request)
    {
        AudioObject tmp = m_pool.GetItem();
        AudioSource tmpSource = tmp.Source;
        ClipLibrary<EMusicTypes> library = m_entityMusicCollectionDictionary[_request.Source][_request.Type];
        tmp.name = $"{_request.Source} {_request.Type}-Sound";

        tmp.transform.parent = _request.Parent;
        tmp.transform.position = _request.Position;

        //Beim Object init schon setzen, falls nur ein SFX output
        tmpSource.outputAudioMixerGroup = m_mixingGroup;
        //FileList[0], wenn nicht ein random sound gewählt werden soll
        tmpSource.clip = library.FileList[0];
        tmpSource.volume = library.Volume;

        tmpSource.Play();
        tmp.SetCountdown((int)(tmpSource.clip.length * 1000));

        m_entityRequests.Remove(_request);
    }
}
