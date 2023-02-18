using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;
using System.Globalization;
using TMPro;
using System.Runtime.CompilerServices;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private GameObject _continueText;
    private TextAsset xmlRawFile;
    private Char[] _trimCharacters;
    private List<float> _spawnerOne = new List<float>();
    private List<float> _spawnerTwo = new List<float>();
    private List<float> _spawnerThree = new List<float>();
    private List<float> _spawnerFour = new List<float>();
    private static string[] LOADINGCOLLECTION = new string[]
    {
            "LOADING.",
            "LOADING..",
            "LOADING..."
    };

    private void Start()
    {
        _trimCharacters = new Char[] {'"'};
        xmlRawFile = (TextAsset)Resources.Load($"XML/{GameManager.Instance.ActiveLevel.name}");
        ParseXMLFile(xmlRawFile.text);
        SetSpawnArrays();
        StartCoroutine(nameof(LoadingScreenStart));
    }

    private IEnumerator LoadingScreenStart()
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("Level");
        loadOp.allowSceneActivation = false;
        Coroutine nameChange = StartCoroutine(nameof(TextChange));

        while (!loadOp.isDone)
        {
            _loadingBar.fillAmount = loadOp.progress;
            
            if (loadOp.progress >= 0.9f)
            {
                _loadingBar.fillAmount = 1f;
                _continueText.SetActive(true);
                StopCoroutine(nameChange);
                if (Input.GetKeyDown(KeyCode.Space))
                    loadOp.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.UnloadSceneAsync("LoadingScreen");
    }

    private IEnumerator TextChange()
    {
        int count = 0;
        int length = LOADINGCOLLECTION.Length;
        while (true)
        {
            _loadingText.text = LOADINGCOLLECTION[count % length];
            count++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ParseXMLFile(string xmlFile)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlFile));

        XmlNodeList elemList = xmlDoc.GetElementsByTagName("line");

        for (int i = 0; i < elemList.Count; i++)
        {
            //XmlNodeList temp = elemList[i].SelectNodes("descendant::starttime");
            XmlNodeList temp = elemList[i].SelectNodes("descendant::time");
            foreach (XmlNode xmlNode in temp)
            {
                switch (i)
                {
                    case 0:
                        _spawnerOne.Add(float.Parse(xmlNode.InnerText.Trim(_trimCharacters), CultureInfo.InvariantCulture));
                        break;
                    case 1:
                        _spawnerTwo.Add(float.Parse(xmlNode.InnerText.Trim(_trimCharacters), CultureInfo.InvariantCulture));
                        break;
                    case 2:
                        _spawnerThree.Add(float.Parse(xmlNode.InnerText.Trim(_trimCharacters), CultureInfo.InvariantCulture));
                        break;
                    case 3:
                        _spawnerFour.Add(float.Parse(xmlNode.InnerText.Trim(_trimCharacters), CultureInfo.InvariantCulture));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void SetSpawnArrays()
    {
        GameManager gameManager = GameManager.Instance;

        switch (gameManager.CurrentLevelDifficulty)
        {
            case ELevelDifficulty.EASY:
                gameManager.SpawnerTwo = _spawnerTwo;
                gameManager.SpawnerThree = _spawnerThree;
                break;
            case ELevelDifficulty.NORMAL:
                gameManager.SpawnerOne = _spawnerOne;
                gameManager.SpawnerTwo = _spawnerTwo;
                gameManager.SpawnerThree = _spawnerThree;
                break;
            case ELevelDifficulty.HARD:
                gameManager.SpawnerOne = _spawnerOne;
                gameManager.SpawnerTwo = _spawnerTwo;
                gameManager.SpawnerThree = _spawnerThree;
                gameManager.SpawnerFour = _spawnerFour;
                break;
            default:
                gameManager.SpawnerOne = _spawnerOne;
                gameManager.SpawnerTwo = _spawnerTwo;
                gameManager.SpawnerThree = _spawnerThree;
                gameManager.SpawnerFour = _spawnerFour;
                break;
        }
    }
}
