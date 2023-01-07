using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
    [Header("Collection of available Levels")]
    [SerializeField] private LevelInfo[] _levelCollection = null;
    private LevelSerializable[] _levelCollectionSerializable = null;
    private string _filePath;

    private LevelInfo _tmpLevel;
    private ScoreInfo _tmpScoreInfo;

    public LevelInfo[] LevelCollection { get => _levelCollection;}

    protected override void Awake()
    {
        _levelCollectionSerializable = new LevelSerializable[_levelCollection.Length];
        _tmpLevel = new LevelInfo();
        _tmpScoreInfo = new ScoreInfo();

        _filePath = $"{Application.dataPath}/levelsettings.bin";
        _isInAllScenes = true;
        base.Awake();
        LoadLevelInformation();
    }

    public void SaveLevelInformation()
    {
        UpdateInfo();
        using (Stream writeStream = File.OpenWrite(_filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, _levelCollectionSerializable);
        }
    }

    public void LoadLevelInformation() 
    {
        if (!File.Exists(_filePath))
            return;

        using (Stream readStream = File.Open(_filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            object deserializedObject = formatter.Deserialize(readStream);

            _levelCollectionSerializable = (LevelSerializable[])deserializedObject;
        }
        LoadInfo();
    }

    /// <summary>
    /// Take info from the scriptableobjects and fill our Serializable classes
    /// </summary>
    private void UpdateInfo()
    {
        for (int i = 0; i < _levelCollectionSerializable.Length; i++)
        {
            LevelInfo tmp = _levelCollection[i];
            List<ScoreSerializable> scoreCollection = new List<ScoreSerializable>();
            List<ScoreInfo> scoreInfo = _levelCollection[i].ScoreCollection;

            for (int j = 0; j < scoreInfo.Count; j++)
            {
                scoreCollection.Add(new ScoreSerializable(scoreInfo[j].Placement, scoreInfo[j].PlayerName, scoreInfo[j].Accuracy, scoreInfo[j].Score));
            }

            _levelCollectionSerializable[i] = new LevelSerializable(tmp.SongName, tmp.ArtistName, tmp.IsUnlocked, tmp.Price, tmp.LevelDifficulty, scoreCollection);
        }
    }

    /// <summary>
    /// Take info from our Serializable classes and fill the scriptableobjects
    /// </summary>
    private void LoadInfo()
    {
        for (int i = 0; i < _levelCollectionSerializable.Length; i++)
        {
            LevelSerializable tmp = _levelCollectionSerializable[i];
            List<ScoreInfo> scoreCollection = new List<ScoreInfo>();
            List<ScoreSerializable> scoreInfos = _levelCollectionSerializable[i].ScoreCollection;

            for (int j = 0; j < scoreInfos.Count; j++)
            {
                _tmpScoreInfo.Init(scoreInfos[j].Placement, scoreInfos[j].PlayerName, scoreInfos[j].Accuracy, scoreInfos[j].Score);
                scoreCollection.Add(_tmpScoreInfo);
            }

            _tmpLevel.Init(tmp.SongName, tmp.ArtistName, tmp.IsUnlocked, tmp.Price, tmp.LevelDifficulty, scoreCollection);
            _levelCollection[i] = _tmpLevel;
        }
    }
}
