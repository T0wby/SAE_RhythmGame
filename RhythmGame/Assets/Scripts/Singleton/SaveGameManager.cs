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


    public LevelInfo[] LevelCollection { get => _levelCollection;}

    protected override void Awake()
    {
        _levelCollectionSerializable = new LevelSerializable[_levelCollection.Length];

        _filePath = $"{Application.dataPath}/levelsettings.bin";
        _isInAllScenes = true;
        base.Awake();
        _levelCollection = LoadLevelInformation(_levelCollection);
    }

    public void SaveLevelInformation(LevelInfo[] levelCollection)
    {
        UpdateInfo(levelCollection);
        using (Stream writeStream = File.OpenWrite(_filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, _levelCollectionSerializable);
        }
    }

    public LevelInfo[] LoadLevelInformation(LevelInfo[] levelCollection) 
    {
        if (!File.Exists(_filePath))
            return levelCollection;

        using (Stream readStream = File.Open(_filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            object deserializedObject = formatter.Deserialize(readStream);

            _levelCollectionSerializable = (LevelSerializable[])deserializedObject;
        }
        return LoadInfo(levelCollection);
    }

    /// <summary>
    /// Take info from the scriptableobjects and fill our Serializable classes
    /// </summary>
    private void UpdateInfo(LevelInfo[] levelCollection)
    {
        for (int i = 0; i < _levelCollectionSerializable.Length; i++)
        {
            LevelInfo tmp = levelCollection[i];
            List<ScoreSerializable> scoreCollection = new List<ScoreSerializable>();
            List<ScoreInfo> scoreInfo = levelCollection[i].ScoreCollection;

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
    private LevelInfo[] LoadInfo(LevelInfo[] levelCollection)
    {
        for (int i = 0; i < _levelCollectionSerializable.Length; i++)
        {
            LevelSerializable tmp = _levelCollectionSerializable[i];
            List<ScoreInfo> scoreCollection = new List<ScoreInfo>();
            List<ScoreSerializable> scoreInfos = _levelCollectionSerializable[i].ScoreCollection;

            for (int j = 0; j < scoreInfos.Count; j++)
            {
                ScoreInfo tmpScoreInfo = (ScoreInfo)ScriptableObject.CreateInstance("ScoreInfo");
                tmpScoreInfo.Init(scoreInfos[j].Placement, scoreInfos[j].PlayerName, scoreInfos[j].Accuracy, scoreInfos[j].Score);
                scoreCollection.Add(tmpScoreInfo);
            }

            levelCollection[i].Init(tmp.SongName, tmp.ArtistName, tmp.IsUnlocked, tmp.Price, tmp.LevelDifficulty, scoreCollection);
        }

        return levelCollection;
    }
}
