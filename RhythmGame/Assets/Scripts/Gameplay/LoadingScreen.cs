using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;
    [SerializeField] private GameObject _continueText;
    private TextAsset xmlRawFile;


    private void Start()
    {
        xmlRawFile = (TextAsset)Resources.Load($"XML/{GameManager.Instance.ActiveLevel}");
        parseXMLFile(xmlRawFile.text);
        StartCoroutine(nameof(LoadingScreenStart));
    }

    private IEnumerator LoadingScreenStart()
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("Level");
        loadOp.allowSceneActivation = false;

        while (!loadOp.isDone)
        {
            _loadingBar.fillAmount = loadOp.progress;

            if (loadOp.progress >= 0.9f)
            {
                _loadingBar.fillAmount = 1f;
                _continueText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                    loadOp.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.UnloadSceneAsync("LoadingScreen");
    }

    private void parseXMLFile(string xmlFile)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlFile));

        XmlNodeList elemList = xmlDoc.GetElementsByTagName("line");


        for (int i = 0; i < elemList.Count; i++)
        {
            XmlNodeList temp = elemList[i].SelectNodes("descendant::starttime");
            List<float> list0 = new List<float>();
            List<float> list1 = new List<float>();
            List<float> list2 = new List<float>();
            List<float> list3 = new List<float>();
            foreach (XmlNode xmlNode in temp)
            {
                Debug.Log($"Parse: {int.Parse(xmlNode.InnerText)}" +
                    $"\n None: {xmlNode.InnerText}");

                //switch (i)
                //{
                //    case 0:
                //        list0.Add(float.Parse(xmlNode.InnerText));
                //        break;
                //    case 1:
                //        list1.Add(float.Parse(xmlNode.InnerText));
                //        break;
                //    case 2:
                //        list2.Add(float.Parse(xmlNode.InnerText));
                //        break;
                //    case 3:
                //        list3.Add(float.Parse(xmlNode.InnerText));
                //        break;
                //    default:
                //        break;
                //}
            }

        }
    }
}
